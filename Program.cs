using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXFparser
{
    class Program
    {
        private static string sourceFilePath = @"D:/distancesXYZ(5).txt";

        static void Main(string[] args)
        {
            ParseToDXF parser = new ParseToDXF();
            parser.generateDXF(sourceFilePath);
            Console.ReadLine();
        }
    }
}
