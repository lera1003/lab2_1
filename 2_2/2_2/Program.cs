using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2_2
{
    class Program
    {
        static int counterOverFlow = 0;
        static bool flag;
        static void Main(string[] args)
        {
            List<byte> a = new List<byte>() { 1, 1, 0, 1, 1, 0, 1, 0 };
            List<byte> b = new List<byte>() { 0, 1, 0, 1, 0, 1, 0, 1 };

            float m = Convert.ToSingle(2.11);
            float k = Convert.ToSingle(5.21);
            Console.WriteLine("\nDivision\n");
            Result res = Divide(a, b);
            Console.WriteLine($"RESULT:::: quotient:{ListToString(res.Quotient)} remain:{ListToString(res.Remain)}");
        }
        static Result Divide(List<byte> numb1, List<byte> numb2)
        {
            Result result = new Result();
            result.Quotient = new List<byte>() { 0 };
            result.Remain = new List<byte>();

            List<byte> numb1InBits = numb1;
            List<byte> numb2InBits = numb2;

            Console.WriteLine($"divided: {ListToString(numb1InBits)} *** divisor: {ListToString(numb2InBits)} *** quotient: {ListToString(result.Quotient)}");

            int counter = 0;
            List<byte> portionOfDivided = new List<byte>();
            for (int i = 0; i < numb2InBits.Count; i++)
            {
                portionOfDivided.Add(numb1InBits[i]);
                counter++;
            }

            do
            {
                if (NumberFromBinaryList(portionOfDivided) >= NumberFromBinaryList(numb2InBits))
                {
                    result.Quotient.Add(1);
                    List<byte> helper = new List<byte>();
                    helper.AddRange(portionOfDivided);
                    portionOfDivided.Clear();
                    portionOfDivided.AddRange(Substract(helper, numb2InBits));
                    if (counter >= numb1InBits.Count)
                    {
                        result.Remain.AddRange(portionOfDivided);
                        break;
                    }
                    portionOfDivided.Add(numb1InBits[counter]);
                    counter++;
                }
                else
                {
                    result.Quotient.Add(0);
                    if (counter >= numb1InBits.Count)
                    {
                        result.Remain.AddRange(portionOfDivided);
                        break;
                    }
                    portionOfDivided.Add(numb1InBits[counter]);
                    counter++;
                }
                RightShift(numb2InBits);

                Console.WriteLine($"divided: {ListToString(numb1InBits)} *** divisor: {ListToString(numb2InBits)} *** quotient: {ListToString(result.Quotient)}");
            }
            while (NumberFromBinaryList(numb1InBits) >= NumberFromBinaryList(numb2InBits));

            return result;
        }
        static List<int> GetProduct(List<int> first, List<int> second)
        {
            List<int> result = new List<int>();
            bool flag = false;

            List<int> help = new List<int>() { 1 };
            help.AddRange(first);
            first.Clear();
            first.AddRange(help);
            help.Clear();
            help.Add(1);
            help.AddRange(second);
            second.Clear();
            second.AddRange(help);

            List<int> helper1 = new List<int>();
            List<int> helper2 = new List<int>();

            if (second[second.Count - 1] == 0)
            {
                helper1.AddRange(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            }
            else
            {
                helper1.AddRange(first);
            }

            List<int> helper = new List<int>();
            int shift = 1;
            for (int i = second.Count - 2; i >= 0; i--)
            {
                helper2.Clear();
                if (second[i] == 0)
                {
                    helper2.AddRange(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                }
                else
                {
                    helper2.AddRange(first);
                }
                helper.AddRange(helper1);
                helper1.Clear();
                shift = helper.Count - helper2.Count + 1;
                helper1.AddRange(AddInBinary(helper, helper2, shift));
                Console.WriteLine($"first term: {ListToString(helper)} *** second term: {ListToString(helper2)} *** sum: {ListToString(helper1)}");
                helper.Clear();
            }

            for (int i = 0; i < helper1.Count; i++)
            {
                while (helper1[0] == 0)
                {
                    helper1.RemoveAt(0);
                }
                helper1.RemoveAt(0);
                break;
            }
            List<int> helperForHelp = new List<int>();
            helperForHelp.AddRange(helper1);
            helper1.Clear();
            for (int i = 0; i < 23; i++)
            {
                helper1.Add(helperForHelp[i]);
            }
            Console.WriteLine($"both mantisa product: {ListToString(helper1)}");

            return helper1;
        }
        static void RightShift(List<byte> divisor)
        {
            List<byte> helper = new List<byte>() { 0 };
            helper.AddRange(divisor);
            divisor.Clear();
            divisor.AddRange(helper);
        }
        static List<byte> Substract(List<byte> portionOfDivided, List<byte> divisor)
        {
            List<byte> result = new List<byte>();

            List<byte> helper;
            while (divisor[0] == 0)
            {
                divisor.RemoveAt(0);
            }
            while (divisor.Count < portionOfDivided.Count)
            {
                helper = new List<byte>() { 0 };
                helper.AddRange(divisor);
                divisor.Clear();
                divisor.AddRange(helper);
                helper.Clear();
            }

            int i = divisor.Count - 1;
            while (i >= 0)
            {
                if (portionOfDivided[i] == 0 && divisor[i] == 0)
                {
                    result.Add(0);
                }
                else if (portionOfDivided[i] == 1 && divisor[i] == 1)
                {
                    result.Add(0);
                }
                else if (portionOfDivided[i] == 1 && divisor[i] == 0)
                {
                    result.Add(1);
                }
                else
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (portionOfDivided[j] == 1)
                        {
                            for (int k = j + 1; k <= i; k++)
                            {
                                if (portionOfDivided[k] == 0)
                                    portionOfDivided[k] = 1;
                            }
                            portionOfDivided[j] = 0;
                            result.Add(1);
                            break;
                        }
                    }
                }
                i--;
            }

            result.Reverse();
            while (result.Count != 1 && result[0] == 0)
            {
                result.RemoveAt(0);
            }
            return result;
        }

        static List<int> Subtract(List<int> exponent, List<int> numb)
        {
            List<int> result = new List<int>();

            for (int i = exponent.Count - 1; i >= 0; i--)
            {
                if (exponent[i] == 0 && numb[i] == 0)
                {
                    result.Add(0);
                }
                else if (exponent[i] == 1 && numb[i] == 1)
                {
                    result.Add(0);
                }
                else if (exponent[i] == 1 && numb[i] == 0)
                {
                    result.Add(1);
                }
                else
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (exponent[j] == 1)
                        {
                            for (int k = j + 1; k <= i; k++)
                            {
                                if (exponent[k] == 0)
                                    exponent[k] = 1;
                            }
                            exponent[j] = 0;
                            result.Add(1);
                            break;
                        }
                    }
                }
            }

            result.Reverse();
            return result;
        }
        static string ListToString(List<byte> list)
        {
            string result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += list[i];
            }
            return result;
        }
        static string ListToString(List<int> list)
        {
            string result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += list[i];
            }
            return result;
        }


        static List<int> AddInBinary(List<int> first, List<int> second, int shift)
        {
            List<int> result = new List<int>();

            if (shift > 0 && !Program.flag)
            {
                List<int> help = new List<int>() { 0 };
                help.AddRange(first);
                first.Clear();
                first.AddRange(help);
            }
            Program.flag = false;

            while (shift > 0)
            {
                second.Add(0);
                shift--;
            }

            int mind = 0;
            for (int i = first.Count - 1; i >= 0; i--)
            {
                if (first[i] == 0 && second[i] == 0 && mind == 0)
                {
                    result.Add(0);
                }
                else if ((first[i] == 0 && second[i] == 0 && mind == 1) ||
                        (first[i] == 0 && second[i] == 1 && mind == 0) ||
                        (first[i] == 1 && second[i] == 0 && mind == 0))
                {
                    result.Add(1);
                    mind = 0;
                }
                else if ((first[i] == 0 && second[i] == 1 && mind == 1) ||
                        (first[i] == 1 && second[i] == 0 && mind == 1) ||
                        (first[i] == 1 && second[i] == 1 && mind == 0))
                {
                    result.Add(0);
                    mind = 1;
                }
                else
                {
                    result.Add(1);
                    mind = 1;
                }
            }
            if (mind == 1)
            {
                result.Add(1);
                Program.flag = true;
                Program.counterOverFlow++;
            }

            result.Reverse();
            return result;
        }

        static int NumberFromBinaryList(List<byte> list)
        {
            int result = 0;
            int counter = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == 1)
                {
                    result += (int)Math.Pow(2, counter);
                }
                counter++;
            }
            return result;
        }
    }
    class Result
    {
        public List<byte> Quotient { get; set; }
        public List<byte> Remain { get; set; }
    }
    class FloatPointNumb
    {
        public List<int> Exponent { get; set; }
        public List<int> Mantisa { get; set; }
        public int Sign { get; set; }

    }
}