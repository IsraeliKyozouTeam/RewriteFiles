using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewriteFiles
{
    class FuncTreeAccessObj
    {

        FunctionsDSTableAdapters.FunctionsTreeTableAdapter tableAdapter;

        public FuncTreeAccessObj()
        {
            tableAdapter = new FunctionsDSTableAdapters.FunctionsTreeTableAdapter();
        }
        
        public FunctionsDS.FunctionsTreeDataTable GetFunctionsByLevel(int level)
        {
            return tableAdapter.GetDataByLevel( level );
        }

        public FunctionsDS.FunctionsTreeDataTable GetFunctionTreeByID(int id)
        {
            return tableAdapter.GetDataByID(id);
        }

        public string GetFunctionNameByID(int id)
        {
            return tableAdapter.GetFuncNamePerId(id).ToString();
        }
        
    }
}
