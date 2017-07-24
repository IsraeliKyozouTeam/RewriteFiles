using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewriteFiles
{
    class FunctionData
    {

        public string CodeOriginal { get; protected set; }
        public string Fixed { get; protected set; }
        public string Path { get; protected set; }
        public int Ident { get; protected set; }
        public string Name { get; protected set; }
        public int calledFuncID { get; protected set; }
        public bool canBeWritten { get; protected set; }


        public FunctionData(int id, string origin, string fix, string path, string name, int calledID, string writeable)
        {
            Ident = id;
            CodeOriginal = origin;
            Fixed = fix;
            Path = path;
            Name = name;
            calledFuncID = calledID;

            if (writeable == "False")
                canBeWritten = false;
            else
                canBeWritten = true;

        }

        public void RegisterFix(string fix)
        {
            Fixed = fix;
        }

        public string RegexName
        {
            get
            {
                string name = Name;

                name = name.Replace("(", @"\(");
                name = name.Replace(")", @"\)");

                return name;
            }
        }





    }
}
