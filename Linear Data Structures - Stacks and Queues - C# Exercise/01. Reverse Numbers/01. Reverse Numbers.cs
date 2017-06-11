namespace _01.Reverse_Numbers
{
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public class Reverse_Numbers_with_a_Stack
    {
        public static void Main()
        {
            var stack = new Stack<int>();
            var nums = Console.ReadLine().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            foreach (var num in nums)
            {
                stack.Push(num);
            }
            while (stack.Count != 0)
            {
                Console.Write($"{stack.Pop()} ");
            }
            Console.WriteLine();
        }
    }
}
