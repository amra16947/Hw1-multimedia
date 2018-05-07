using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1_MUL
{
    class Program
    {
        static void Main(string[] args)
        {

            string FileName = args[1].ToUpper();
            string ReadOrWrite = args[0];
              MyClass newClass = new MyClass();


               if (ReadOrWrite == "-R")
               {

                   using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                   {
                       newClass.FileByte = File.ReadAllBytes(FileName);
                       int length = args.Length;
                    while(newClass.byteNum < newClass.FileByte.Length)
                       for (int i = 2; i < length; i++)
                       {
                           byte numBits = Convert.ToByte(args[i].Substring(3, args[i].Length - 3));
                           ulong bits = newClass.ReadBits(numBits);
                           Console.WriteLine("BIT" + numBits + " " + newClass.ulongToString(bits, numBits));

                       }
                   }




               }
               else if (ReadOrWrite == "-W")
               {

                
                using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    string[] data = File.ReadAllLines(FileName);
                    newClass.FileNameBin = args[2];
                    BinaryWriter writer = new BinaryWriter(File.Open(newClass.FileNameBin, FileMode.Create));
                    writer.Close();
                    
                        foreach (string s in data)
                        {
                            string StrBits = s.Substring(s.IndexOf(" ") + 1, s.Length - s.IndexOf(" ") - 1);
                            ulong bits = newClass.StringToulong(StrBits);
                            newClass.WriteBits(bits, Convert.ToByte(StrBits.Length));
                        }
                    
                }
                
              }   
                else
                {
                }

        }
    }
}
