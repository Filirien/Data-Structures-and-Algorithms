namespace _05.Count_of_Occurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class Program
    {
        public static void Main()
        {
            var list = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var dic = new Dictionary<int, int>();
            for (int i = 0; i < list.Length; i++)
            {
                if (!dic.ContainsKey(list[i]))
                {
                    dic[list[i]] = 1;
                }
                else
                {
                    dic[list[i]]++;
                }
            }
            foreach (var item in dic.OrderBy(a => a.Key))
            {
                Console.WriteLine($"{item.Key} -> {item.Value} times");
            }
        }
    }
}
