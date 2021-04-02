using System;
using System.Collections.Generic;

namespace Settlements
{
    internal static class PrinterService
    {
        public static void PrintPaymentsInfo(IEnumerable<string> payments)
        {
            foreach (var payment in payments) Console.WriteLine(payment);
        }

        public static void PrintBalanceInfo(IEnumerable<Person> persons)
        {
            foreach (var p in persons)
                Console.WriteLine($"Name: {p.Name} \t Total in: {p.TotalIn} \t Total out: {p.TotalOut} \t TotalBalance: {p.TotalBalance}");
        }
    }
}