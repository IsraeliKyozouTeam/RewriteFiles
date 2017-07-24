using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionsTree;
using System.Text.RegularExpressions;
using System.IO;

namespace RewriteFiles
{
    class Rewriter
    {

        public string pathToWrite { get; protected set; }

        public Rewriter(string pathToWriteTo)
        {
            pathToWrite = pathToWriteTo;
        }
        

        public void RunRewrite( FuncTreeAccessObj dao )
        {
           
            FunctionsDS.FunctionsTreeDataTable table = new FunctionsDS.FunctionsTreeDataTable();

            List<FunctionData> funcList = new List<FunctionData>();
            List<FunctionData> funcsToFixManual = new List<FunctionData>();
            

            // Currently assuming that level 6 is the final level
            for (int i = 1; i <= 6; i++)
            {
                table = dao.GetFunctionsByLevel(i);

                foreach (FunctionsDS.FunctionsTreeRow row in table)
                {
                    string funcName = Regex.Match(row.FuncCode, "function (?<FuncName>.*?\\(.*?\\))", RegexOptions.Singleline).Groups["FuncName"].Value;
                    FunctionData currFunc = new FunctionData(row.Id, row.FuncCode, row.FixedFunction, Path.Combine(pathToWrite, row.FileName), funcName, row.CalledFuncId, row.CanBeReWriten);
                    funcList.Add(currFunc);

                    Console.WriteLine(currFunc.Ident + ", " + i);

                    // Check if the function has not been fixed
                    if (string.IsNullOrWhiteSpace( currFunc.Fixed ))
                    {
                        if (FunctionFixer.IsFuncFixable(currFunc))
                        {
                            // If the function has not been fixed and it can be fixed, fix it. 
                            FunctionFixer.FixFunction(currFunc, dao);
                        }
                        else
                        {
                            Console.WriteLine("Function {0}, {1} cannot be fixed!", currFunc.Ident, currFunc.Name);
                            // If it cannot be fixed then add it to the list of funcs that
                            // need a manual fix and dont rewrite the function (a.k.a wait for the func to be fixed before writing it).
                            funcsToFixManual.Add(currFunc);
                            continue;
                        }
                    }
                    
                    // Once we have made sure the function has a fixed version, go ahead
                    // and overwrite the function to file.
                    OverwriteFunc(currFunc);
                }
            }


            // Once all the functions have been passed through then we can go ahead and
            // run all the functions through the manual fix call.
            CallForManualFix(funcsToFixManual);

        }


        public void OverwriteFunc(FunctionData func)
        {
            string output = "";

            // if need to change directory of files for rewriting - do here
            try
            {

                output = File.ReadAllText(func.Path);
                string replaceExpression = "(?<Func>function " + func.RegexName + @".*?{.*?})(?<NextFuncStart>.*?function)";
                output = Regex.Replace(output, replaceExpression, (m) => ( func.Fixed + m.Groups["NextFuncStart"].Value), RegexOptions.Singleline);

                File.WriteAllText(func.Path, output);
                
            }
            catch
            {
                Console.WriteLine("Path {0} does not exist!", func.Path);
            }
        }


        public void CallForManualFix(List<FunctionData> funcList)
        {

            if (funcList.Count == 0)
                return;

            string path = Path.Combine(pathToWrite, "ManualFix.txt");

            StreamWriter writer = new StreamWriter(path);

            foreach (FunctionData func in funcList)
            {
                writer.WriteLine(func.Ident);
                writer.WriteLine(func.Name);
                writer.WriteLine(func.Path);
                writer.WriteLine();
            }

            writer.WriteLine(funcList.Count);

            writer.Close();
            
        }




        /*
        public void RunRewrite(FunctionsTreeAccess DA)
        {
            FunctionsDS.FunctionsTree1DataTable Data = DA.GetFixedAndOriginalFunctions();

            foreach (FunctionsDS.FunctionsTree1Row Row in Data)
            {
                string text = "";
                string path = "";

                // if need to change directory of files for rewriting - do here
                try
                {

                    path = Path.Combine(pathToWrite, Row.filename);

                    text = File.ReadAllText(Row.filename);

                    string fixedCode = Row.FixedFunction;
                    string funcName = Regex.Match(fixedCode, "function (.+?)\\(.+?\\).+?{.+?}.+?function", RegexOptions.Singleline).Groups[1].Value;

                    string replaceExpression = "(function " + funcName + "\\(.+?\\).+?{.+?}).+?function";

                    text = Regex.Replace( text, replaceExpression, 
                                                                    delegate (Match m) {
                                                                        return m.Groups[1].Value.Replace(m.Groups[1].Value, fixedCode);
                                                                    }, RegexOptions.Singleline );

                    File.WriteAllText(Row.filename, text);


                }
                catch
                {
                    Console.WriteLine("Path {0} does not exist!", Row.filename);
                }


            }
        }
    }*/
    }
}
