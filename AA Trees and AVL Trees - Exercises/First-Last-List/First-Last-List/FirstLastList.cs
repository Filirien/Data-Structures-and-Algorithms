using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private List<T> byInsertion = new List<T>();
    private OrderedBag<T> byOrder = new OrderedBag<T>();
    private OrderedBag<T> byOrderReversed = new OrderedBag<T>((x,y) => -x.CompareTo(y));
    public int Count
    {
        get
        {
            return byInsertion.Count;
        }
    }

    public void Add(T element)
    {
        byInsertion.Add(element);
        byOrder.Add(element);
        byOrderReversed.Add(element);
    }

    public void Clear()
    {
        byOrder.Clear();
        byInsertion.Clear();
        byOrderReversed.Clear();

    }

    public IEnumerable<T> First(int count)
    {
        ValidateCount(count);
        for (int i = 0; i < count; i++)
        {
            yield return byInsertion[i];
        }
    }

    private void ValidateCount(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    public IEnumerable<T> Last(int count)
    {
        ValidateCount(count);
        for (int i = byInsertion.Count -1; i >= byInsertion.Count-count; i--)
        {
            yield return byInsertion[i];
        }
    }

    public IEnumerable<T> Max(int count)
    {
        ValidateCount(count);
        foreach (var item in byOrderReversed)
        {
            if (count <=0)
            {
                break;
            }
            yield return item;
            count--;
        }
    }

    public IEnumerable<T> Min(int count)
    {
        ValidateCount(count);
        foreach (var item in byOrder)
        {
            if (count <= 0)
            {
                break;
            }
            yield return item;
            count--;
        }
    }

    public int RemoveAll(T element)
    {
        foreach (var item in byOrder.Range(element, true, element, true))
        {
            byInsertion.Remove(item);
        }
        var count = byOrder.RemoveAllCopies(element);
        byOrderReversed.RemoveAllCopies(element);
        return count;
    }
}
