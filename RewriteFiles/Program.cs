using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionsTree;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace RewriteFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            

            string pathWebUI = ConfigurationManager.AppSettings["pathWebUI"];

            Rewriter writer = new Rewriter( pathWebUI );

            FuncTreeAccessObj dao = new FuncTreeAccessObj();

            writer.RunRewrite( dao );

           /* 
            #region Debugging FunctionFixer

            string pathStr = @"C:\Users\roysh_000\Desktop\TestDir\FunctionFixTest.txt";
            string testFuncStr = File.ReadAllText(pathStr);
            string funcName = Regex.Match(testFuncStr, "function (.*?\\(.*?\\))", RegexOptions.Singleline).Groups[1].Value;

            FunctionData testFunc = new FunctionData(40027, testFuncStr, null, pathStr, funcName, 40012, "True");

            FunctionFixer.FixFunction(testFunc, dao);

            Console.WriteLine(testFunc.Fixed);
            Console.ReadKey();

            #endregion
            */

            Console.ReadKey();

        }

    }
}
