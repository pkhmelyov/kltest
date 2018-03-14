using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Pairs
    {
        public static IEnumerable<IEnumerable<int>> GetPairs(int[] z, int x)
        {
            var result = new List<IEnumerable<int>>();
            bool[] usedFlags = Enumerable.Repeat(false, z.Length).ToArray();
            for (int i = 0; i < z.Length; i++)
            {

                if (usedFlags[i])
                {
                    continue;
                }

                int firstNumber = z[i];
                int secondNumber = x - firstNumber;

                for (int j = i + 1; j < z.Length; j++)
                {
                    if (usedFlags[j])
                    {
                        continue;
                    }

                    if (z[j] == secondNumber)
                    {
                        result.Add(new [] { firstNumber, secondNumber });
                        usedFlags[j] = true;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
