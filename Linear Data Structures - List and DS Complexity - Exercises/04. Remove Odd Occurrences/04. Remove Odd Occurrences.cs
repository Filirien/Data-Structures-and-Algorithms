namespace _04.Remove_Odd_Occurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
                    dic[list[i]] = 0;
                }
                dic[list[i]]++;
            }
            var invalidNums = dic.Where(a=>a.Value%2!=0).Select(a=>a.Key);
            var result = new List<int>();
            for (int i = 0; i < list.Length; i++)
            {
                if (!invalidNums.Contains(list[i]))
                {
                    result.Add(list[i]);
                }
            }
            Console.WriteLine(string.Join(" ",result));
        }
    }
}

