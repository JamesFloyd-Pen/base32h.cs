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
        //EncodeBin
        public ArrayList encodeBin(int[] input)
        {
            ArrayList output;

            var overflow = input.Length % 5;
            if (overflow != 0)
            {
                //input = Array.ConstrainedCopy(input, input.Length + 5-overflow);
                input.CopyTo(input, input.Length + 5 - overflow);
                input = moveZeroesToLeft(input);

            }
            for(int i = 0; i < input.Length; i+=5)
            {
                var segment = new ArraySegment<int>(input,i,i+5);
                //var segInt = bytesToUint40(segment);
                //output.add(encode(segInt);
                //pad(output);
            }

            return output;
        }

        private long bytesToUint40(int[] input)
        {

            return input[0] * (long)Math.Pow(2, 32) + input[1] * (long)Math.Pow(2, 24) + input[2] * (long)Math.Pow(2, 16) + input[3] * (long)Math.Pow(2, 8) + input[4];
        }

        private int[] moveZeroesToLeft(int[] n)
        {

            int count = 0;
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] == 0)
                {
                    n[i] = n[count];
                    n[count++] = 0;
                }
            }

            return n;
        }

        public int decodeDigit(Char input)
        {
            int index = -1;
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i].Contains(input))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public long decode(String input) 
        {
            Char[] rem = input.ToCharArray();
            int temp = rem.Length, i = rem.Length;
            long answer = 0, exp = 0;

            while (temp > 0)
            {
                long digit = decodeDigit(rem[i-1]);
                i -= 1;
                temp -= 1;

                if (digit < 0)
                {
                    continue;
                }

                answer += digit * (long)Math.Pow(32, exp);
                exp++;
            }

            return answer;
        }

        static void Main(string[] args) 
        {
            base32h myBase = new base32h();
            var y = myBase.decode("howdy");
            System.Console.WriteLine(y);
            //var x = myBase.encode(17854910);
            //myBase.printArrayList(x);

        }

        void printArrayList(ArrayList x)
        {
            foreach (Object obj in x)
                Console.Write(" {0}", obj);
            Console.WriteLine();
        }

    }
}
