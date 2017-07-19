using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewriteFiles
{
    class FunctionData
    {

        public string Original { get; protected set; }
        public string Fixed { get; protected set; }
        public string Path { get; protected set; }
        public int Ident { get; protected set; }
        public string Name { get; protected set; }


        public FunctionData(int id, string origin, string fix, string path, string name)
        {
            Ident = id;
            Original = origin;
            Fixed = fix;
            Path = path;
            Name = name;
        }





    }
}
