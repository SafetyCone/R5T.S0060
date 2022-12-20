using System;


namespace R5T.S0060.S001
{
    class Program
    {
        static void Main()
        {
            var writer = Console.Out;

            F0000.EnvironmentOperator.Instance.DescribeTo(writer);
        }
    }
}
