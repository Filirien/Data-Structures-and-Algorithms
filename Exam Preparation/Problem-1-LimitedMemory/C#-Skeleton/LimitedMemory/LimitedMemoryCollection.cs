using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace LimitedMemory
{
    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        private Dictionary<K, LinkedListNode<Pair<K, V>>> dic;
        private LinkedList<Pair<K, V>> linkedList;
        public LimitedMemoryCollection(int capacity)
        {
            this.Capacity = capacity;
            this.Count = 0;
            this.dic = new Dictionary<K, LinkedListNode<Pair<K, V>>>();
            this.linkedList = new LinkedList<Pair<K, V>>();

        }

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            var currentElement = linkedList.Last;
            while (currentElement != null)
            {
                yield return currentElement.Value;
            currentElement = currentElement.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get; private set; }

        public int Count { get; private set; }

        public void Set(K key, V value)
        {
            if (!dic.ContainsKey(key))
            {
                var newNode = new LinkedListNode<Pair<K, V>>(new Pair<K, V>(key, value));
                if (Count == Capacity)
                {
                    var firstElement = linkedList.First;

                    linkedList.RemoveFirst();
                    dic.Remove(firstElement.Value.Key);
                    this.Count--;
                }
                linkedList.AddLast(newNode);
                dic[key] = newNode;
                Count++;
            }
            else
            {
                var elementToRemove = dic[key];
                linkedList.Remove(elementToRemove);
                elementToRemove.Value = new Pair<K, V>(key, value);
                linkedList.AddLast(elementToRemove);
            }
        }

        public V Get(K key)
        {
            if (!dic.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }
            var elementToRemove = dic[key];
            linkedList.Remove(elementToRemove);
            linkedList.AddLast(elementToRemove);
            return elementToRemove.Value.Value;
        }
    }
}
