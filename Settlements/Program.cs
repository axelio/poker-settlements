using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Settlements
{
    internal class Program
    {
        private static void Main()
        {
            var persons = new List<Person>
            {
                new("A", 30.00m, 40.20m),
                new("B", 20.00m, 49.80m),
                new("C", 20.00m, 5.00m),
                new("D", 25.00m, 0.00m),
                new("E", 20.00m, 20.00m)
            };

            if (persons.Sum(p => p.TotalIn) != persons.Sum(p => p.TotalOut))
                throw new ArgumentException("Invalid total in and total out of all people balance.");

            ShowBalanceInfo(persons);
            WriteLine();
            ShowPayments(persons);

            ReadKey();
        }

        public static void ShowBalanceInfo(IEnumerable<Person> persons)
        {
            foreach (var p in persons)
            {
                WriteLine($"Name: {p.Name} \t Total in: {p.TotalIn} \t Total out: {p.TotalOut} \t TotalBalance: {p.TotalBalance}");
            }
        }

        public static void ShowPayments(IEnumerable<Person> persons)
        {
            var (positiveBalancePersons, negativeBalancePersons) = GetGroupedPersons(persons);

            foreach (var loser in negativeBalancePersons)
            {
                foreach (var winner in positiveBalancePersons)
                {
                    if (winner.IsSettled) continue;

                    var loserCurrentBalanceAbsoluteValue = Math.Abs(loser.CurrentBalance);

                    if (winner.CurrentBalance >= loserCurrentBalanceAbsoluteValue)
                    {
                        WriteLine($"{loser.Name} owes {winner.Name}: {loserCurrentBalanceAbsoluteValue}");
                        winner.CurrentBalance -= loserCurrentBalanceAbsoluteValue;
                        loser.CurrentBalance = 0;
                        break;
                    }

                    if (winner.CurrentBalance < loserCurrentBalanceAbsoluteValue)
                    {
                        WriteLine($"{loser.Name} owes {winner.Name}: {winner.CurrentBalance}");
                        loser.CurrentBalance += winner.CurrentBalance;
                        winner.CurrentBalance = 0;
                    }
                }
            }
        }

        private static (List<Person> positiveBalancePersons, List<Person> negativeBalancePersons) GetGroupedPersons(IEnumerable<Person> persons)
        {
            var sortedPersons = persons.OrderByDescending(p => p.TotalBalance);

            var positiveBalancePersons = new List<Person>();
            var negativeBalancePersons = new List<Person>();

            foreach (var p in sortedPersons)
            {
                if (p.TotalBalance > 0) positiveBalancePersons.Add(p);
                else if (p.TotalBalance < 0) negativeBalancePersons.Add(p);
            }

            return (positiveBalancePersons, negativeBalancePersons);
        }
    }
}