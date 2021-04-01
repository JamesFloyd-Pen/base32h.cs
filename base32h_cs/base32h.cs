using System;
using System.Collections;

namespace base32h_cs
{
    class base32h
    {
        public string[] digits = {
            "0Oo",
            "1Ii",
            "2",
            "3",
            "4",
            "5Ss",
            "6",
            "7",
            "8",
            "9",
            "Aa",
            "Bb",
            "Cc",
            "Dd",
            "Ee",
            "Ff",
            "Gg",
            "Hh",
            "Jj",
            "Kk",
            "Ll",
            "Mn",
            "Nn",
            "Pp",
            "Qq",
            "Rr",
            "Tt",
            "VvUu",
            "Ww",
            "Xx",
            "Yy",
            "Zz"
        };

        public ArrayList encode(long input){

            ArrayList output = new ArrayList();


            if (input == 0)
            {
                output.Add("0");
                return output;
            }

            while(input > 0)
            {
                long temp = input % 32;
                int indexPointer = (int)temp;
                output.Insert(0, digits[indexPointer % 32].Substring(0, 1));
                input = input/32;
            }

            return output;
        }


        static void Main(string[] args)
        {
            base32h myBase = new base32h();
            //System.Console.WriteLine("Hello World");
            var x = myBase.encode(17854910);
            myBase.printArrayList(x);

        }

        void printArrayList(ArrayList x)
        {
            foreach (Object obj in x)
                Console.Write(" {0}", obj);
            Console.WriteLine();
        }

    }
}
