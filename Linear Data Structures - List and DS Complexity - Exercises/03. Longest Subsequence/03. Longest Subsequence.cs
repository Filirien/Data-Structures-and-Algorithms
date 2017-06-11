namespace _03.Longest_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class Program
    {
        public static void Main()
        {
            var nums = Console.ReadLine().Split().Select(int.Parse).ToList();
            var dict = EqualNumbers(nums);
            foreach (var item in dict.OrderByDescending(a=>a.Value))
            {
                var result = Convert.ToChar(item.Key);
                var print = item.Value;
                Console.WriteLine(new string(result,print));
                break;
            }
        }

        private static Dictionary<int,int> EqualNumbers(List<int> nums)
        {
            var list = new List<int>(nums[0]);
            var dic = new Dictionary<int, int>();
            for (int i = 1; i < nums.Count; i++)
            {
                var first = nums[i - 1];
                var second = nums[i];
                if (!dic.ContainsKey(first))
                {
                    dic[first] = 1;
                }
                if (first == second)
                {
                    dic[first]++;
                }
            }
            return dic;
        }
    }
}
