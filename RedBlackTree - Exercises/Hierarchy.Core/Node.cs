using System;
using System.Collections.Generic;


public class Node<T> where T : IComparable
{
    public T Value;
    public Node<T> Parent;
    public List<Node<T>> Children;


    public Node(T value, Node<T> parent = null)
    {
        this.Children = new List<Node<T>>();
        this.Value = value;
        this.Parent = parent;
    }
    public T getValue()
    {
        return this.Value;
    }
    public Node<T> getParent()
    {
        return this.Parent;
    }
    public void setParent(Node<T> parent)
    {
        this.Parent = parent;
    }
    public List<Node<T>> getChildren()
    {
        return this.Children;
    }
    public void addChild(Node<T> child)
    {
        this.Children.Add(child);
    }
    public String toString()
    {
        return this.Value + "";
    }
}

