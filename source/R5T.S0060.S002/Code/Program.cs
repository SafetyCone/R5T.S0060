using System;

using R5T.F0000;


namespace R5T.S0060.S002
{
    class Program
    {
        static void Main()
        {
            var writer = Console.Out;

            //InputArgumentOperator.Instance.DescribeTo(writer);
            InputArgumentOperator.Instance.DescribeTo_IncludingZeroth(writer);
        }
    }
}
