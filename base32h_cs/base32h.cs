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
            ArrayList output = new ArrayList();

            var overflow = input.Length % 5;
            if (overflow != 0)
            {
                //input = Array.ConstrainedCopy(input, input.Length + 5-overflow);
                //input.CopyTo(input, input.Length + 5 - overflow);
                input = moveZeroesToLeft(input);

            }
            for(int i = 0; i < input.Length; i+=5)
            {
                int[] segment = new int[5];
                Array.Copy(input, 0, segment, 0, 4);
                long segInt = bytesToUint40(segment);
                output.AddRange(encode(segInt));
                pad(output);
                printArrayList(output);

            }

            return output;
        }

        private long bytesToUint40(int[] input)
        {

            return input[0] * (long)Math.Pow(2, 32) + input[1] * (long)Math.Pow(2, 24) + input[2] * (long)Math.Pow(2, 16) + input[3] * (long)Math.Pow(2, 8) + input[4];
        }

        private ArrayList pad(ArrayList input)
        {

            int o = input.Count % 8;
            if (o != 0)
            {
                for (int i = 0; i < 8 - o; i++)
                {
                    input.Insert(i, "0");
                }
                return input;
            }

            return input;
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

        public ArrayList decodeBin(String input)
        {
            ArrayList output = new ArrayList();
            ArrayList padded = new ArrayList();
            padded.AddRange(input.ToCharArray());
            var n = padded.Count;

            for(int i = 0; i < n; i +=8)
            {
                String segment ="";
                long val = decode(segment);
                output.AddRange(uint40ToBytes(val));
            }

            return output;
        }

        private int[] uint40ToBytes(long input)
        {
            int[] bytes = { 0, 0, 0, 0, 0 };
            for (int idx = bytes.Length - 1; idx >= 0; idx--)
            {
                float myByte = input & 0xff;
                bytes[idx] = (int)myByte;
                input = (input - (long)myByte) / 256;
                if (input == 0)
                {
                    break;
                }
            }
            return bytes;
        }


        static void Main(string[] args) 
        {
            base32h myBase = new base32h();

            ArrayList padd = new ArrayList();
            //String test = "Corrin";
            //padd.AddRange(test.ToCharArray());
           // myBase.printArrayList(padd);
            //int[] test = { 255, 255, 255, 255, 255};
            // myBase.encodeBin(test);
            //System.Console.WriteLine();
            //var y = myBase.decode("howdy");
            //System.Console.WriteLine(y);
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
