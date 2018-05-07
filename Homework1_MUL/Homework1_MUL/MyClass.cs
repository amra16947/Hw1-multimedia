using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1_MUL
{
    public class MyClass
    {
        public byte[] FileByte;
        public int byteNum = 0;
        public int bitNum = 0;
        public int currentBits = 0;
        public byte currentByte = 0;
        public string FileNameBin;

        public ulong ReadBit()
        {
            byte current = FileByte[byteNum];

            ulong bit = Convert.ToUInt64((current & (1 << (7 - bitNum))) != 0);

            bitNum++;
            if (bitNum == 8)
            {
                byteNum++;
                bitNum = 0;
            }

            return bit;
        }

        public ulong ReadBits(byte numBits)
        {
            ulong value = 0;
            for (byte i=1; i<=numBits; i++)
            {
               
                value = value << 1;
                value += ReadBit();
                
            }
            return value;

        }


        public void WriteBits(ulong data, byte numBits)
        {

            for(int i = numBits - 1; i>=0; i--)
            {
                ulong position = Convert.ToUInt64(1 << i);
                int and = Convert.ToInt32((data & position) != 0);
                byte andShift = Convert.ToByte( and << (7 - currentBits));

                currentByte = Convert.ToByte(Convert.ToInt32(currentByte) | Convert.ToInt32(andShift));

                currentBits++;

                if(currentBits == 8)
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(FileNameBin, FileMode.Append)))
                    {
                        writer.Write(currentByte);
                        writer.Close();
                       
                    }
                    currentBits = 0;
                    currentByte = 0;
                }
            }


        }


        public string ulongToString(ulong data, byte numBits)
        {
            string dataString = "";
 
            const int mask = 1;
            
            while (data > 0)
            {
                
                dataString = (data & mask) + dataString;
                data = data >> 1;
            }

            while(dataString.Length < numBits)
            {
                dataString = "0" + dataString;
            }

            return dataString;
 
        }


        public ulong StringToulong(string bitStr)
        {
            ulong bitUlong = 0;
            char zero = '0';
            for(int i = 0; i < bitStr.Length; i++)
            {
                ulong some = Convert.ToUInt64(bitStr[i] - zero);
                bitUlong = (bitUlong << 1) + some;
            }

            
            return bitUlong;
        }
    }
}
