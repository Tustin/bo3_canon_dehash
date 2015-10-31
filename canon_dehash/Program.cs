//#define test
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace canon_dehash
{

    class Program
    {
        //requires canon.dll to be inside the executable directory
       [DllImport("canon.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint HashCanonicalString(string fname);

        static List<string> output = new List<string>();
        static void Main(string[] args)
        {
            string filename = "bo2_dvars.txt"; //args[0];

#if test
            Console.Write(filename);
            Console.ReadKey();
            return;
#endif
            int count = 1;
            string[] input;
            input = File.ReadAllLines(filename);

            Console.Write("\rLoaded {0} strings... Beginning hash sequence\n", input.Length);
            output.Add("//Canonical mass dehasher by Tustin");
            foreach (string line in input)
            {
                uint hash = HashCanonicalString(line);

                if (args.Length == 1 && args[1] == "-p")
                    output.Add("0x" + hash.ToString("x8"));
                else
                    output.Add("0x" + hash.ToString("x8") + " - " + line);

                Console.Write("\rHashed string {0} - {1}/{2}\n", line, count, input.Length);
                count++;
            }
            File.WriteAllLines("done.txt", output.ToArray());
            Console.Write("\rFinished hashing.");
            Console.ReadKey();
        }
    }
}
