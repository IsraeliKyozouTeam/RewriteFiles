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
            
            FunctionsTreeAccess DA = new FunctionsTreeAccess();

            string pathWebUI = ConfigurationManager.AppSettings["pathWebUI"];

            Rewriter writer = new Rewriter( pathWebUI );

            writer.RunRewrite(  );
            
                
        }

    }
}
