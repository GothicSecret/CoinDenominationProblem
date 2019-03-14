using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinDenominationProblem
{
    public class CoinPurse
    {
        public int Coins7 { get; set; }

        public int Coins5 { get; set; }

        public int Coins4 { get; set; }

        public decimal GetTotal()
        {
            return Coins7 * 7 + Coins5 * 5 + Coins4 * 4;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            decimal cost = .07M;
            decimal maxCost = .31M;

            do
            {
                var coins = FindCoins(cost);
                Console.WriteLine($"Coint total amount: {coins.GetTotal()}: {coins.Coins7} seven cent coins, {coins.Coins5} five cent coins, {coins.Coins4} four cent coins");
                cost += .01M;
            } while (cost <= maxCost);
            Console.Read();
        }

        public static CoinPurse FindCoins(decimal cost)
        {
            int theIntCost = decimal.ToInt32(cost * 100);
            var coinPurse = new CoinPurse()
            {
                Coins7 = theIntCost / 7
            };
            switch (theIntCost - coinPurse.Coins7 * 7)
            {
                case 1:// -7 so remains 1 + 7 = 8. Add 4 + 4
                    coinPurse.Coins7--;
                    coinPurse.Coins4 += 2;
                    break;

                case 2:// -7 so remains 2 + 7 = 9. Add 4 + 5
                    coinPurse.Coins7--;
                    coinPurse.Coins5++;
                    coinPurse.Coins4++;
                    break;

                case 3:// -7 so remains 3 + 7 = 10. Add 5 + 5
                    coinPurse.Coins7--;
                    coinPurse.Coins5 += 2;
                    break;

                case 4:// just add 4
                    coinPurse.Coins4++;
                    break;

                case 5:// just add 5
                    coinPurse.Coins5++;
                    break;

                case 6:// -7 so remains 6 + 7 = 13. Add 4 + 4 + 5
                    coinPurse.Coins7--;
                    coinPurse.Coins4 += 2;
                    coinPurse.Coins5++;
                    break;
            }
            return coinPurse;
        }
    }
}
