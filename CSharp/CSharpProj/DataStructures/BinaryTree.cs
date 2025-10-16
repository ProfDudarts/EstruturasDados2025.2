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
            node = nodeList.Pop()!;

            if (node.Left is null)
            {
                node.Left = new(value);
                break;
            }
            else
            {
                nodeList.Add(node.Left);
            }

            if (node.Right is null)
            {
                node.Right = new(value);
                break;
            }
            else
            {
                nodeList.Add(node.Right);
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
            node = nodeList.Pop()!;
            yield return node.Value!;

            if (node.Left is not null)
            {
                nodeList.Add(node.Left);
            }
            if (node.Right is not null)
            {
                nodeList.Add(node.Right);
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
        string str = "[";
        foreach (T i in this)
        {
            str += i?.ToString();
            str += ", ";
        }
        str += "]";
        return str;

        // Old code
        // {


        //     //3             [abcd]             (4i+13e*2+2d)
        //     //2           /        \           (8i+11e*2+2d)
        //     //2     [abcd]          [abcd]     (4i+5e*2+2d)*2 
        //     //1       /\              /\       (0i+7e*2+2d)*2  
        //     //1 [abcd]  [abcd]  [abcd]  [abcd] (4i+e*2+2d)*4   

        //     // e = (2e°+p(n))
        //     // p(n) = n/2 +1

        //     // e = numero de espaços externos de valores
        //     // e° = "e" inicial
        //     // n = characters em valor
        //     // v = seta
        //     // ve = espaços externos das setas | vi = espaço interno das setas

        //     // 2vi = 2(e-1) = tvi
        //     // 2ve = 2(n+2+e) = tve

        //     // // prepping
        //     // StringBuilder graph = new();
        //     // Queue<string> values = new();
        //     // int levels = (int)Math.Log2(Count)+1;
        //     // int maxChar = 0;
        //     // int levelSpaces;

        //     // static int vi(int e) => e - 1;
        //     // static int ve(int n, int e) => e + n + 2;
        //     // static int SetLevelSpace(int n, int lv)
        //     // {
        //     //     int x = 1;
        //     //     for (int i = 0; i < lv; i++)
        //     //         x = (2 * x) + (n / 2) + 1;
        //     //     return x;
        //     // }

        //     // //getting variables
        //     // foreach (T value in this)
        //     // {
        //     //     int valueLength;
        //     //     if (value is null)
        //     //     {
        //     //         values.Add(string.Empty);
        //     //         valueLength = 0;
        //     //     }
        //     //     else
        //     //     {
        //     //         string strValue = value.ToString() ?? string.Empty;
        //     //         valueLength = strValue.Length;
        //     //         values.Add(strValue);
        //     //     }
        //     //     if (valueLength > maxChar)
        //     //     {
        //     //         maxChar = valueLength;
        //     //     }
        //     // }
        //     // maxChar = maxChar % 2 == 0 ? maxChar : maxChar + 1;

        //     // // Final Construction
        //     // for (int level = levels; level != 0; level--)
        //     // {
        //     //     int altLevel = levels - level;
        //     //     levelSpaces = SetLevelSpace(maxChar, level - 1);

        //     //     for (int arrowStep = 0; arrowStep < Math.Pow(2, altLevel); arrowStep++)
        //     //     {
        //     //         if (level < levels)
        //     //         {
        //     //             if (arrowStep % 2 == 0)
        //     //             {
        //     //                 graph.Append(new string(' ', ve(maxChar, levelSpaces)));
        //     //                 graph.Append('/');
        //     //                 graph.Append(new string(' ', vi(levelSpaces)));
        //     //             }
        //     //             else
        //     //             {
        //     //                 graph.Append(new string(' ', vi(levelSpaces)));
        //     //                 graph.Append('\\');
        //     //                 graph.Append(new string(' ', ve(maxChar, levelSpaces)));
        //     //             }
        //     //         }
        //     //     }
        //     //     graph.Append('\n');
        //     //     for (int nodeStep = 0; nodeStep < Math.Pow(2, altLevel); nodeStep++)
        //     //     {
        //     //         string bit;
        //     //         try
        //     //         {
        //     //             bit = values.Pop();
        //     //         }
        //     //         catch
        //     //         {
        //     //             bit = string.Empty;
        //     //         }
        //     //         graph.Append(new string(' ', levelSpaces));
        //     //         graph.Append($"[{bit.PadLeft(maxChar)}]");
        //     //         graph.Append(new string(' ', levelSpaces));
        //     //     }
        //     //     graph.Append('\n');

        //     // }
        //     // graph.Remove(0, 1);
        //     // graph.Remove(graph.Length - 1, 1);
        //     // return graph.ToString();
        // }

    }
}

















