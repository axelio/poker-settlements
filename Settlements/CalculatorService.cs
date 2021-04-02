using System;
using System.Collections.Generic;
using System.Linq;

namespace Settlements
{
    internal static class CalculatorService
    {
        public static void SettlePayments(IEnumerable<Person> persons, ICollection<string> payments)
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
                        payments.Add($"{loser.Name} owes {winner.Name}: {loserCurrentBalanceAbsoluteValue}");
                        winner.CurrentBalance -= loserCurrentBalanceAbsoluteValue;
                        loser.CurrentBalance = 0;
                        break;
                    }

                    if (winner.CurrentBalance < loserCurrentBalanceAbsoluteValue)
                    {
                        payments.Add($"{loser.Name} owes {winner.Name}: {winner.CurrentBalance}");
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