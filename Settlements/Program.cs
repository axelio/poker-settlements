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
            var paymentsInfo = new List<string>();
            
            var persons = new List<Person>
            {
                new("A", 30.00m, 40.20m),
                new("B", 20.00m, 49.80m),
                new("C", 20.00m, 5.00m),
                new("D", 25.00m, 0.00m),
                new("E", 20.00m, 20.00m)
            };

            PrinterService.PrintBalanceInfo(persons);
            WriteLine();
            ValidateTotals(persons);
            CalculatorService.SettlePayments(persons, paymentsInfo);
            PrinterService.PrintPaymentsInfo(paymentsInfo);

            ReadKey();
        }

        private static void ValidateTotals(List<Person> persons)
        {
            if (persons.Sum(p => p.TotalIn) != persons.Sum(p => p.TotalOut))
                throw new ArgumentException("Invalid total in and total out of all people balance.");
        }
    }
}