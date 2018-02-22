using System;
using System.Collections.Generic;
using System.Linq;

namespace ClaimMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal cost = 31.00M;
            decimal maxCost = 31.10M;

            do
            {
                var coins = FindCoins(cost);
                var four = coins.Count(x => x == .04M);
                var five = coins.Count(x => x == .05M);
                var seven = coins.Count(x => x == .07M);

                Console.WriteLine($"Coint total amount: {coins.Sum()}: {seven} seven cent coins, {five} five cent coins, {four} four cent coins");
                cost += .01M;
            } while (cost <= maxCost);
            Console.Read();
        }

        public static List<decimal> FindCoins(decimal cost)
        {
            int theIntCost = decimal.ToInt32(cost * 100);
            var coins = new List<decimal>();
            int remainder = 0;

            while (theIntCost > 0)
            {
                switch (theIntCost % 7)
                {
                    case 0:
                        coins.Add(.07M);
                        theIntCost -= 7;
                        break;
                    case 5:
                        coins.Add(.05M);
                        theIntCost -= 5;
                        break;
                    case 4:
                        coins.Add(.04M);
                        theIntCost -= 4;
                        break;
                    default:
                        remainder += theIntCost%7;
                        if (remainder == 4 || remainder == 5 || remainder == 7)
                        {
                            coins.Add(remainder*.01M);
                            theIntCost -= remainder;
                            remainder = 0;
                        }
                        if (remainder > 7)
                        {
                            var remainderCoins = FindCoins(remainder * .01M);
                            coins.AddRange(remainderCoins);
                            theIntCost -= decimal.ToInt32(remainderCoins.Sum() * 100);
                            ;
                            remainder = 0;
                        }
                        break;
                }
            }
            return coins;
        }
    }
}
