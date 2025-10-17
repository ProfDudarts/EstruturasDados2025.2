using DataStructures.Nodes;
using System.Text;

namespace DataStructures;

/// <summary>
/// Simple singly-linked stack implementation using Knot<T> as node storage.
/// </summary>
public class Stack<T> : IEnumerable<T>
{
    private int _count = 0; // number of elements 
    private Knot<T>? TopKnot = null; // the top/root of the stack

    public bool IsEmpty { get { return _count == 0; } }// return true if empty

    public Stack() { Clear(); } // constructor

    /// <summary>
    /// Clears the stack
    /// </summary>
    public void Clear()
    {
        _count = 0;
        TopKnot = null;
    }

    /// <summary>
    /// Pushes the value<T> onto the top of the stack
    /// </summary>
    public void Add(T value)
    {
        Knot<T> knotToAdd = new(value, TopKnot);
        TopKnot = knotToAdd;
        _count += 1;
    }

    /// <summary>
    /// Removes and returns the top value; throws if the stack is empty
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"> if the stack is empty </exception>
    public T Pop()
    {
        if (IsEmpty)
        { throw new IndexOutOfRangeException("Can't Remove from Empty Stack"); }

        T value = TopKnot!.Value;

        TopKnot = TopKnot.NextKnot;
        _count -= 1;

        return value;
    }

    /// <summary>
    /// Returns the number of elements in the stack
    /// </summary>
    public int Count()
    { return _count; }

    /// <summary>
    /// Returns the number of elements in the stack that satisfy the given condition
    /// </summary>
    public int Count(Func<T, bool> condition)
    {
        int count = 0;
        foreach (T item in this)
        {
            if (condition(item))
            { count++; }
        }
        return count;
    }

    /// <summary>
    /// Returns the top value without removing it; throws if the stack is empty
    /// </summary>
    /// <exception cref="InvalidOperationException"> If the stack is empty </exception>
    public T Peek()
    {
        if (TopKnot is not null)
        { return TopKnot.Value; }
        else
        { throw new InvalidOperationException("Can't peek at an empty stack"); }
    }

    /// <summary>
    /// Tries to return the top value without removing it; returns false if the stack is empty
    /// </summary>
    /// <param name="value"> When this method returns, contains the object at the top of the stack, if the stack is not empty; </param>
    public bool TryPeek(out T? value)
    {
        try
        {
            value = Peek();
            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }

    /// <summary>
    /// Returns the next value without removing it; throws if there is no next value
    /// </summary>
    /// <exception cref="InvalidOperationException"> If there is no next value </exception>
    public T CheckNextValue()
    {
        if (TopKnot is not null && TopKnot.HasNext)
        { return TopKnot.NextKnot!.Value; }
        else
        { throw new InvalidOperationException("There's no value to check"); }
    }

    /// <summary>
    /// Tries to return the next value without removing it; returns false if there is no next value
    /// </summary>
    /// <param name="value">  When this method returns, contains the next object in the stack, if there is a next value; </param>
    public bool TryCheckNextValue(out T? value)
    {
        try
        {
            value = CheckNextValue();
            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }

    /// <summary>
    /// Returns the index of the first element that satisfies the given condition, or -1 if none do
    /// </summary>
    public int GetIndex(Func<T, bool> condition)
    {
        int count = 0;
        foreach (T item in this)
        {
            if (condition(item)) { return count; }
            count++;
        }
        return -1;
    }

    /// <summary>
    /// Returns the index of the first occurrence of the given value, or -1 if not found
    /// </summary>
    public int GetIndex(T value)
    {
        return GetIndex(x => EqualityComparer<T>.Default.Equals(x, value));
    }

    /// <summary>
    /// Gives the class Enumerator functions
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        var knot = TopKnot;
        while (knot is not null)
        {
            yield return knot.Value;
            knot = knot.NextKnot;
        }
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Returns a string representation of the stack
    /// </summary>
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