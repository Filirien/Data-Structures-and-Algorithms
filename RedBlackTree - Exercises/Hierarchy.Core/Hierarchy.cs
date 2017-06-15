using System;
using System.Collections.Generic;
using System.Collections;

public class Hierarchy<T> : IHierarchy<T> where T : IComparable
{
    private Node<T> root;
    Dictionary<T, Node<T>> nodesDic = new Dictionary<T, Node<T>>();

    public Hierarchy(T root)
    {
        this.root = new Node<T>(root);
        nodesDic[root] = this.root;
    }

    public int Count
    {
        get
        {
            return this.nodesDic.Count;
        }
    }

    public void Add(T parent, T child)
    {
        if (!this.nodesDic.ContainsKey(parent))
        {
            throw new ArgumentException();
        }
        if (this.nodesDic.ContainsKey(child))
        {
            throw new ArgumentException();
        }
        Node<T> parentNode = this.nodesDic[parent];
        Node<T> childNode = new Node<T>(child);
        childNode.Parent = parentNode;
        parentNode.Children.Add(childNode);
        nodesDic[child] = childNode;
    }

    public void Remove(T element)
    {
        if (!this.nodesDic.ContainsKey(element))
        {
            throw new ArgumentException();
        }
        Node<T> toBeRemoved = this.nodesDic[element];
        if (toBeRemoved.Parent == null)
        {
            throw new InvalidOperationException();
        }
        Node<T> parentNode = nodesDic[toBeRemoved.Parent.Value];

        foreach (var child in toBeRemoved.Children)
        {
            child.Parent = parentNode;
            parentNode.Children.Add(child);
        }
        parentNode.Children.Remove(toBeRemoved);
        nodesDic.Remove(toBeRemoved.Value);
    }

    public IEnumerable<T> GetChildren(T item)
    {
        if (!this.Contains(item))
        {
            throw new ArgumentException();
        }
        Node<T> node = this.nodesDic[item];
        List<T> children = new List<T>();
        foreach (Node<T> childNode in node.getChildren())
        {
            children.Add(childNode.getValue());
        }
        return children;
    }

    public T GetParent(T item)
    {
        if (!this.Contains(item))
        {
            throw new ArgumentException();
        }
        Node<T> parent = this.nodesDic[item].getParent();
        if (parent == null)
        {
            return default(T);
        }
        return parent.getValue();
    }

    public bool Contains(T value)
    {
        return nodesDic.ContainsKey(value);
    }

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
    {
        var nodes = this.nodesDic.Values;
        var commonElements = new List<T>();
        foreach (var node in nodes)
        {
            if (other.Contains(node.getValue()))
            {
                commonElements.Add(node.getValue());
            }
        }
        return commonElements;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var queue = new Queue<Node<T>>();
        var currentElement = this.root;
        while (true)
        {
            yield return currentElement.Value;
            foreach (var child in currentElement.Children)
            {
                queue.Enqueue(child);
            }
            if (queue.Count == 0)
            {
                yield break;
            }
            currentElement = queue.Dequeue();

        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
