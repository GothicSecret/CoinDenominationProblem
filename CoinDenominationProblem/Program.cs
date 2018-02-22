using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinDenominationProblem
{
    public class CoinPurse
    {
        public CoinPurse()
        {
            Coins = new Dictionary<decimal, int>()
            {
                {.04M, 0},
                {.05M, 0},
                {.07M, 0}
            };
        }
        public Dictionary<decimal, int> Coins { get; private set; }

        public decimal GetTotal()
        {
            decimal result = 0M;
            foreach (var kvp in Coins)
            {
                result += kvp.Key*kvp.Value;
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            decimal cost = .07M;
            decimal maxCost = .31M;

            do
            {
                var coins = FindCoins(cost);
                Console.WriteLine($"Coint total amount: {coins.GetTotal()}: {coins.Coins[.07M]} seven cent coins, {coins.Coins[.05M]} five cent coins, {coins.Coins[.04M]} four cent coins");
                cost += .01M;
            } while (cost <= maxCost);
            Console.Read();
        }

        public static CoinPurse FindCoins(decimal cost)
        {
            int theIntCost = decimal.ToInt32(cost * 100);
            var coinPurse = new CoinPurse();
            int remainder = 0;

            while (theIntCost > 0)
            {
                switch (theIntCost % 7)
                {
                    case 0:
                        int remainingSevens = theIntCost/7;
                        coinPurse.Coins[.07M] += remainingSevens;
                        theIntCost -= remainingSevens * 7;
                        break;
                    case 5:
                        coinPurse.Coins[.05M] += 1;
                        theIntCost -= 5;
                        break;
                    case 4:
                        coinPurse.Coins[.04M] += 1;
                        theIntCost -= 4;
                        break;
                    default:
                        remainder += theIntCost%7;
                        if (remainder == 4 || remainder == 5 || remainder == 7)
                        {
                            coinPurse.Coins[remainder*.01M] += 1;
                            theIntCost -= remainder;
                            remainder = 0;
                        }
                        if (remainder > 7)
                        {
                            var remainderPurse = FindCoins(remainder * .01M);
                            foreach (var denomination in remainderPurse.Coins)
                            {
                                coinPurse.Coins[denomination.Key] += denomination.Value;
                            }
                            theIntCost -= decimal.ToInt32(remainderPurse.GetTotal() * 100);
                            ;
                            remainder = 0;
                        }
                        break;
                }
            }
            return coinPurse;
        }
    }
}
