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
        public static string FixFunction( FunctionData func, FuncTreeAccessObj dao )
        {
            // TODO: Fix the function !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            


            string calledFuncName = dao.GetFunctionNameByID( func.calledFuncID );

            string replaceExpression = @"function .+?(?<ParamStart>\().*?\)";
            string replaceByFuncNameExpression = calledFuncName + @"(\().*?\)";
            string callbackReplace = "(callback, ";

            string fixedFunc = func.CodeOriginal;

            fixedFunc = Regex.Replace(fixedFunc, replaceExpression, (m) => (m.Groups[1].Value.Replace(m.Groups[1].Value, callbackReplace)), RegexOptions.Singleline);
            
            fixedFunc = Regex.Replace(fixedFunc, replaceByFuncNameExpression, (m) => ( m.Groups[1].Value.Replace(m.Groups[1].Value, callbackReplace) ), RegexOptions.Singleline);

            func.RegisterFix(fixedFunc);

            return fixedFunc;
        }

        public static bool IsFuncFixable( FunctionData func )
        {
            // TODO: Check if the function is fixable !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            if (func.canBeWritten == false)
                return false;
            
            if (Regex.Matches(func.CodeOriginal, "ShowPostableModalWindow").Count > 0)
                return false;

            
            return true;
        }












    }
}
