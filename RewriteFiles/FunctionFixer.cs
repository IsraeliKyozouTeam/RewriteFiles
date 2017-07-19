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

            


            string calledFuncName = "";

            string replaceExpression = "function .+?(\\().*?\\)";
            string replaceByFuncNameExpression = "function " + calledFuncName + "(\\().*?\\)";
            string callbackReplace = "(callback, ";

            string fixedFunc = "";

            fixedFunc = Regex.Replace(func.Original, replaceExpression,
                                                                delegate (Match m) {
                                                                    return m.Groups[1].Value.Replace(m.Groups[1].Value, callbackReplace);
                                                                }, RegexOptions.Singleline);


            fixedFunc = Regex.Replace(func.Original, replaceByFuncNameExpression,
                                                                delegate (Match m) {
                                                                    return m.Groups[1].Value.Replace(m.Groups[1].Value, callbackReplace);
                                                                }, RegexOptions.Singleline);

            return fixedFunc;
        }

        public static bool IsFuncFixable( FunctionData func )
        {
            // TODO: Check if the function is fixable !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            return true;
        }












    }
}
