using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Media3D;
using System.Threading;
using System.Globalization;


namespace DXFparser
{
    class ParseToDXF
    {
        public string dxfFilePath = @"D:/distancesDXF.dxf"; // ścieżki do plików postaci @"D:/distancesXYZ.txt"
        public StreamWriter dxf;

        public void generateDXF(string inputFilePath)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            if (!File.Exists(inputFilePath))
            {
                File.Create(inputFilePath);
            }
            StreamReader inputFileReader = null;

            try
            {
                inputFileReader = new StreamReader(inputFilePath);
            } catch (FileNotFoundException e)
            {
                Console.WriteLine("Nie mozna odczytac zawartosci pliku.");
                /////////////////////////////////////////////////////////////////////////////////
                //TU POP-UP MESSAGE!!!!!!!!!!!
            }

            string line;
            int count = 0;
            bool success = true;

            try
            {
                dxf = new StreamWriter(dxfFilePath);
                writeDXFHeader();
                dxf.Write("\n0\nSECTION\n2\nENTITIES");


                int dataLength = 3;
                int startCoord = 0;
                char delimeter = ' ';

                while (true)
                {
                    line = inputFileReader.ReadLine();
                    if (line != null)
                    {
                        string[] currentLine = line.Split(delimeter);
                        if (currentLine.Length > dataLength - 1)
                        {
                            Point3D point = new Point3D(Convert.ToDouble(currentLine[startCoord]),
                                Convert.ToDouble(currentLine[startCoord + 1]),
                                Convert.ToDouble(currentLine[startCoord + 2]));

                            writeDXFPoint(point);
                        }
                        count++;
                    }
                    else break;
                }
                dxf.Write("\n0");
                dxf.Write("\nENDSEC");
                

            } catch (IOException e)
            {
                success = false;
                Console.WriteLine("Wystapil blad");
                ///////////////////////////////////////////////////////////////////////////
                //TUTAJ POP-UP MESSAGE!!
            } finally
            {
                try
                {
                    dxf.Write("\n0");
                    dxf.Write("\nEOF");
                } catch (IOException e)
                {
                    if (e.Source != null)
                        Console.WriteLine("IOException source: {0}", e.Source);
                    throw;
                }
            }

            if(success)
            {
                Console.WriteLine("Sukces!");
                Console.WriteLine("Przekonwertowano {0} punkty!", count);
            }
            dxf.Close();
        }

        private void writeDXFHeader()
        {
            try
            {
                dxf.Write("0\nSECTION");
                dxf.Write("\n2\nHEADER");
                dxf.Write("\n9\n$ACADVER\n1");
                dxf.Write("\nAC1006\n9\n$INSBASE\n10\n0.0\n20\n0.0\n30\n0.0");
                dxf.Write("\n0\nENDSEC");
            } catch (IOException e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
        }

        private void writeDXFPoint(Point3D point)
        {
            try
            {
                dxf.Write("\n  0");
                dxf.Write("\nPOINT");
                dxf.Write("\n8");
                dxf.Write("\nPointCloud");

                //X
                dxf.Write("\n10");
                dxf.Write("\n" + point.X.ToString());
                Console.WriteLine("Wspolrzedna X obecnego punktu: " + point.X.ToString());
                //Y
                dxf.Write("\n20");
                dxf.Write("\n" + point.Y.ToString());
                //Z
                dxf.Write("\n30");
                dxf.Write("\n" + point.Z.ToString());
            } catch (IOException e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
        }
    }
}
