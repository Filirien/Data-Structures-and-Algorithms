namespace _01.Sum_and_Average
{
    using System;
    using System.Linq;
    public class Program
    {
       public static void Main()
        {
            var list = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();
            Console.WriteLine($"Sum={list.Sum()}; Average={list.Average():F2}");
        }
    }
}
