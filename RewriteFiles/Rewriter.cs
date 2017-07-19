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



        public void RunRewrite( /* Get a DataObject from the database */ )
        {

            // Use the DataObject to iterate over all of the level 1 functions and
            // foreach lvl we will create a tree of FunctionData that goes all the way
            // to the final level. Then iterate over that list, and saves the ID's of all 
            // fixed functions so as to not run twice.

            FunctionsDS.FunctionsTreeDataTable table = new FunctionsDS.FunctionsTreeDataTable();
            FuncTreeAccessObj dao = new FuncTreeAccessObj();
            List<FunctionData> funcList = new List<FunctionData>();
            List<FunctionData> funcsToFixManual = new List<FunctionData>();


            // Currently assuming that level 6 is the final level
            for (int i = 1; i <= 6; i++)
            {
                table = dao.GetFunctionsByLevel(i);

                foreach (FunctionsDS.FunctionsTreeRow row in table)
                {
                    string funcName = Regex.Match(row.FuncCode, "function (.+?\\(.*?\\))", RegexOptions.Singleline).Groups[1].Value;
                    FunctionData currFunc = new FunctionData(row.CalledFuncId, row.FuncCode, row.FixedFunction, Path.Combine( pathToWrite, row.FileName), funcName );
                    funcList.Add( currFunc );

                    
                    // Check if the function has not been fixed
                    if ((currFunc.Fixed == "" || currFunc.Fixed == null))
                    {
                        if (FunctionFixer.IsFuncFixable(currFunc) && i > 2)
                        {
                            // If the function has not been fixed and it can be fixed, fix it. 
                            FunctionFixer.FixFunction(currFunc, dao);
                        }
                        else
                        {
                            // If it cannot be fixed then call for a manual fix
                            funcsToFixManual.Add(currFunc);
                            continue;
                        }
                    }
                    
                    // Once we have made sure the function has a fixed version, go ahead
                    // and overwrite the function to file.
                    OverwriteFunc(currFunc);
                    
                }
            }

            CallForManualFix(funcsToFixManual);

        }

        public void OverwriteFunc(FunctionData func)
        {
            string text = "";

            // if need to change directory of files for rewriting - do here
            try
            {

                text = File.ReadAllText(func.Path);
                
                string replaceExpression = "(function " + func.Name + "\\(.+?\\).+?{.+?}).+?function";

                text = Regex.Replace(text, replaceExpression,
                                                                delegate (Match m) {
                                                                    return m.Groups[1].Value.Replace(m.Groups[1].Value, func.Fixed);
                                                                }, RegexOptions.Singleline);

                File.WriteAllText(func.Path, text);


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
                writer.WriteLine(func.Name);
                writer.WriteLine(func.Path);
                writer.WriteLine();
            }

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
