namespace _02.Sort_Words
{
    using System;
    using System.Linq;
    public class Program
    {
        public static void Main()
        {
            var list = Console.ReadLine()
                .Split()
                .ToList();
            Console.WriteLine(string.Join(" ",list.OrderBy(a=>a)));
        }
    }
}
