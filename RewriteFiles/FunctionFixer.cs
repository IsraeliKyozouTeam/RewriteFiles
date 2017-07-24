using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RewriteFiles
{
    static class FunctionFixer
    {
        public static string FixFunction(FunctionData func, FuncTreeAccessObj dao)
        {



















            return "";

            /*
            // TODO: Fix the function !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            string calledFuncName = dao.GetFunctionNameByID(func.calledFuncID);
            string fixedFunc = func.CodeOriginal;

            string replaceExpression = @"(?<Func>function)(?<FuncName>.+?)(?<ParamStart>\()(?<Params>.*?\))";
            string replaceByFuncNameExpression = calledFuncName + @"(?<ParamStart>\()(?<Params>.*?\))";
            string callbackMultiParam = "(callback, ";
            string callbackSingleParam = "(callback";

            string callbackReplace;


            // Check if the function is a single paramater (or if it isnt) function and give the 
            // replace string the corresponding replacement string            
            if (isSingleParamFunc(Regex.Match(fixedFunc, replaceExpression)))
            callbackReplace = callbackSingleParam;
            else
                callbackReplace = callbackMultiParam;
            
            fixedFunc = Regex.Replace(fixedFunc, replaceExpression, (m) => (m.Groups["Func"].Value + m.Groups["FuncName"].Value + callbackReplace + m.Groups["Params"].Value), RegexOptions.Singleline);

            /*
            // Check if the called function is a single paramater (or if it isnt) function and give the 
            // replace string the corresponding replacement string
            if (isSingleParamFunc(Regex.Match(fixedFunc, replaceByFuncNameExpression)))
                callbackReplace = callbackSingleParam;
            else
                callbackReplace = callbackMultiParam;

            fixedFunc = Regex.Replace(fixedFunc, replaceByFuncNameExpression, (m) => (calledFuncName + callbackReplace + m.Groups["Params"].Value), RegexOptions.Singleline);

            
            dao.UpdateFixedFuncByID(func.Ident, func.Fixed);
            
            func.RegisterFix(fixedFunc);

            return fixedFunc;
            */
        }

        public static bool IsFuncFixable( FunctionData func )
        {
            // TODO: Add more things to check if the function is fixable, maybe multiple function calls etc.
            

            if (func.canBeWritten == false)
                return false;
            
            

            
            return true;
        }

        static bool isSingleParamFunc(Match m)
        {
            if (m.Groups["Params"].Value == ")")
                return true;
            else
                return false;
        }












    }
}
