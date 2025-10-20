using DataStructures.Nodes;
using System.ComponentModel;
using System.Text;

namespace DataStructures;

/// <summary>
/// A simple binary tree implementation.
/// </summary>
public class BinaryTree<T> : IEnumerable<T>
{
    private Node<T>? Root; // the root of the tree
    private int Count; // number of elements

    public bool IsEmpty { get { return (Root is null) ? true : false; } } // return true if empty

    public BinaryTree() { Clear(); } // constructor

    /// <summary>
    /// Clears the Tree
    /// </summary>
    public void Clear()
    {
        Root = null;
        Count = 0;
    }

    /// <summary>
    /// Returns the height of the tree
    /// </summary>
    /// <exception cref="InvalidOperationException"> if tree is empty </exception>
    public int GetHeight()
    {
        // return (int)Math.Log2(Count);
        static int _getHeight(Node<T> node)
        {
            if (node.Left is null && node.Right is null)
                return 0;

            int leftValue = 0;
            int rightValue = 0;

            if (node.Left is not null)
                leftValue = _getHeight(node.Left);

            if (node.Right is not null)
                rightValue = _getHeight(node.Right);

            if (rightValue > leftValue)
                return rightValue + 1;
            else
                return leftValue + 1;

        }

        if (Root is null)
            throw new InvalidOperationException("Empty tree doesn't have a height");

        return _getHeight(Root);
    }

    /// <summary>
    /// Pushes a value<T> to the tree
    /// </summary>
    public void Add(T value)
    {
        if (Root is null)
        {
            Root = new(value);
            Count++;
            return;

        }

        Queue<Node<T>> nodeList = [Root];
        Node<T> node;

        while (nodeList.Count() > 0)
        {
            node = nodeList.Dequeue()!;

            if (node.Left is null)
            {
                node.Left = new(value);
                break;
            }
            else
            {
                nodeList.Enqueue(node.Left);
            }

            if (node.Right is null)
            {
                node.Right = new(value);
                break;
            }
            else
            {
                nodeList.Enqueue(node.Right);
            }
            Count++;

        }
    }

    /// <summary>
    /// Gives Enumerator functions to the class
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        if (Root is null) { yield break; }

        Queue<Node<T>> nodeList = [Root];
        Node<T> node;

        while (nodeList.Count() > 0)
        {
            node = nodeList.Dequeue()!;
            yield return node.Value!;

            if (node.Left is not null)
            {
                nodeList.Enqueue(node.Left);
            }
            if (node.Right is not null)
            {
                nodeList.Enqueue(node.Right);
            }
        }
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();


    /// <summary>
    /// return a string representation of the tree
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append('[');
        bool first = true;
        foreach (T item in this)
        {
            if (!first) sb.Append(", ");
            sb.Append(item is null ? "null" : item.ToString());
            first = false;
        }
        sb.Append(']');
        return sb.ToString();

    }
}

















