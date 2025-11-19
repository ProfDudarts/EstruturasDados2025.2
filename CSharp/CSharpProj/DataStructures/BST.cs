using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using DataStructures.Nodes;

namespace DataStructures;

/// <summary>
/// A basic binary search tree implementation
/// </summary>
public class MySearchTree<T> where T : IComparable<T>
{
    private Node<T>? root = null; // The root of the Tree
    int Count = 0; // Count
    bool IsEmpty => Count == 0; // Check if the Tree is empty

    private readonly IComparer<T> _comparer = Comparer<T>.Default; // comparer

    public MySearchTree() { } // basic constructor

    /// <summary> Clears the tree </summary>
    public void Clear()
    {
        root = null;
        Count = 0;
    }

    #region Basic Operations

    /// <summary>
    /// Inserts a value into the tree
    /// </summary>
    public void Insert(T value)
    {
        if (IsEmpty)
            InsertRoot(value);
        else
            Inserter(value, root!);
    }

    /// <summary>
    /// Return a Value in the tree that corresponds to the value given <br/>
    /// return default if not found
    /// </summary>
    public T? Search(T value)
    {
        var node = Searcher(root, value);
        return node is null ? default : node.Value;
    }

    /// <summary>
    /// Deletes a node from the tree and returns its value
    /// Returns default if not found
    /// </summary>
    public T? Delete(T value)
    {
        if (IsEmpty) return default;

        (var parent, var child, var isLeft) = SearchNodePosition(value);

        if (child is null) return default;

        return Unlink(parent, isLeft);
    }

    #endregion






    #region Getters

    /// <summary>
    /// Returns the outmost Right value <br/>
    /// The outmost Right value should be the max <br/>
    /// if is empty return null
    /// </summary>
    public T? Max()
    {
        if (IsEmpty) return default;
        return Max(root!).Value;
    }

    /// <summary>
    /// Returns the outmost Left value <br/>
    /// The outmost Left value should be the min <br/>
    /// if is empty return null
    /// </summary>
    public T? Min()
    {
        if (IsEmpty) return default;
        return Min(root!).Value;
    }


    /// <summary>
    /// Return the Height of the tree <br/>
    /// if is empty return -1
    /// </summary>
    public int GetHeight()
    {
        if (IsEmpty)
            return -1;
        return CalculateHeight(root!);
    }

    #endregion






    #region Helpers

    /// <summary>
    /// true if bigger greater to smaller <br/>
    /// false if smaller greater or equal to bigger <br/>
    /// use bigger for value and smaller for node's <br/>
    /// same as (bigger > smaller) 
    /// </summary>
    private bool Compare(T bigger, T smaller) => _comparer.Compare(bigger, smaller) > 0;

    /// <summary>
    /// return true if A is equivalent to B <br/>
    /// A needs to be equal to B in the eyes of the comparator
    /// </summary>
    private bool CompareEquality(T A, T B) => _comparer.Compare(A, B) == 0;

    /// <summary> Used to Instantiate Nodes </summary>
    private static Node<T> CreateNode(T value) => new Node<T>(value);

    // return (int)Math.Log2(Count);
    /// <summary>
    /// Recursive method to get the height of the tree
    /// </summary>
    private static int CalculateHeight(Node<T> node)
    {
        if (node.Left is null && node.Right is null)
            return 0;

        int leftValue = 0;
        int rightValue = 0;

        if (node.Left is not null)
            leftValue = CalculateHeight(node.Left);

        if (node.Right is not null)
            rightValue = CalculateHeight(node.Right);

        if (rightValue > leftValue)
            return rightValue + 1;
        else
            return leftValue + 1;
    }

    /// <summary>
    /// return the outmost Right node from the origin
    /// </summary>
    private static Node<T> Max(Node<T> origin)
    {
        while (origin.Right is not null)
            origin = origin.Right;
        return origin;
    }

    /// <summary>
    /// return the outmost Left node from the origin
    /// </summary>
    private static Node<T> Min(Node<T> origin)
    {
        while (origin.Left is not null)
            origin = origin.Left;
        return origin;
    }

    /// <summary> Add a value to the tree </summary>
    private void InsertRoot(T value)
    {
        root = CreateNode(value);
        Count++;
    }

    /// <summary> A recursive method to insert a value in the Tree </summary>
    private void Inserter(T value, Node<T> node)
    {
        if (Compare(value, node.Value))
        {
            if (node.Right is null)
            {
                node.Right = CreateNode(value);
                Count++;
            }
            else
            {
                Inserter(value, node.Right);
            }
        }
        else
        {
            if (node.Left is null)
            {
                node.Left = CreateNode(value);
                Count++;
            }
            else
            {
                Inserter(value, node.Left);
            }
        }
    }

    /// <summary>
    /// Recursive method that navigates the tree in search of a value equal to the one given
    /// </summary>
    private Node<T>? Searcher(Node<T>? node, T value)
    {
        if (node is null)
            return null;

        if (CompareEquality(node.Value, value))
            return node;

        return Compare(value, node.Value)
            ? Searcher(node.Right, value)
            : Searcher(node.Left, value);
    }

    /// <summary>
    /// Checks if target exists, else throws InvalidOperationException
    /// </summary>
    private bool ValidateTarget(Node<T>? parent, bool isLeft)
    {
        if (parent is null)
            { if (IsEmpty) return false; }
        else
        {
            if (isLeft) 
                { if (parent.Left is null) return false; }
            else
                { if (parent.Right is null) return false; }
        }
        return true;
    }

    /// <summary>
    /// return the parent, child and if is the left child of the parent <br/>
    /// if parent is null, target is the root
    /// if child is null, target doesn't exist
    /// </summary>
    private (Node<T>? parent, Node<T>? child, bool isLeft) SearchNodePosition(T value)
    {
        Node<T>? parent = null;
        Node<T>? current = root!;
        bool isLeft = false;
        while (current is not null && !CompareEquality(current.Value, value))
        {
            parent = current;
            if (Compare(value, current.Value))
            {
                current = current.Right;
                isLeft = false;
            }
            else
            {
                current = current.Left;
                isLeft = true;
            }
        }
        return (parent, current, isLeft);
    }

    /// <summary>
    /// A performative O(h) method that unlinks a node <br/>
    /// throws if target doesn't exist
    /// </summary>
    private T Unlink(Node<T>? parent, bool isLeft)
    {
        if (!ValidateTarget(parent, isLeft)) 
            throw new InvalidOperationException("Child doesn't exist or is Empty");

        if (parent is null)
        {
            var dummy = CreateNode(root!.Value);
            dummy.Left = root;
            Unlink(dummy, true);
            root = dummy.Left;
            return dummy.Value;
        }

        var target = isLeft ? parent.Left! : parent.Right!;
        var targetValue = target.Value;

        static void SetNode(Node<T> parent, bool isLeft, Node<T>? node)
        {
            if (isLeft) parent.Left = node;
            else parent.Right = node;
        }

        // Target is Leaf
        if (target.Left is null && target.Right is null)
        {
            SetNode(parent, isLeft, null);
            return targetValue;
        }

        // Target has one Child
        if (target.Left is null ^ target.Right is null)
        {
            var existingChild = target.Left ?? target.Right;
            SetNode(parent, isLeft, existingChild);
            return targetValue;
        }

        // Target Has Both Children
        Node<T> prev = target;
        Node<T> min = target.Right!;
        while (min.Left is not null)
        {
            prev = min;
            min = min.Left;
        }

        if (prev == target)
            prev.Right = min.Right; // min is target.Right
        else
            prev.Left = min.Right;

        min.Left = target.Left;   //
        min.Right = target.Right; // links target children to min node 

        SetNode(parent, isLeft, min);

        return targetValue;
    }
    #endregion
}