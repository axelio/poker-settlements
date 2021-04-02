namespace Settlements
{
    internal class Person
    {
        internal Person(string name, decimal totalIn, decimal totalOut)
        {
            Name = name;
            TotalIn = totalIn;
            TotalOut = totalOut;
            TotalBalance = totalOut - totalIn;
            CurrentBalance = TotalOut - totalIn;
        }

        public string Name { get; }
        public decimal TotalIn { get; }
        public decimal TotalOut { get; }
        public decimal TotalBalance { get; }
        public decimal CurrentBalance { get; set; }
        public bool IsSettled => CurrentBalance == 0;
    }
}
