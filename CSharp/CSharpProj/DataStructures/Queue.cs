using DataStructures.Nodes;
using System.Text;

namespace DataStructures;

/// <summary>
/// A simple linked-list FIFO queue implementation.
/// </summary>
public class Queue<T> : IEnumerable<T>
{
    private int _count = 0; // The count of items in the queue
    private Knot<T>? TopKnot = null; // The Top/Root of the Queue
    private Knot<T>? LastKnot = null; // The last item

    public bool IsEmpty { get { return _count == 0; } } // return true is the list is empty

    public Queue() { Clear(); } // The class constructor

    /// <summary>
    /// Remove all items from the queue and reset internal state.
    /// </summary>
    public void Clear()
    {
        _count = 0;
        TopKnot = null;
        LastKnot = null;
    }

    #region Operations

    /// <summary>
    /// Enqueue a new value at the tail of the queue.
    /// </summary>
    /// <param name="value">Value to add to the queue</param>
    public void Add(T value) => Enqueue(value);

    /// <summary>
    /// Enqueue a new value at the tail of the queue.
    /// </summary>
    /// <param name="value">Value to add to the queue</param>
    public void Enqueue(T value)
    {
        Knot<T> ToAdd = new(value);

        if (IsEmpty)
        {
            TopKnot = ToAdd;
            LastKnot = ToAdd;
        }
        else
        {
            LastKnot!.NextKnot = ToAdd;
            LastKnot = ToAdd;
        }

        _count += 1;
    }

    /// <summary>
    /// Dequeue and return the value at the front of the queue.
    /// Throws InvalidOperationException if queue is empty.
    /// </summary>
    /// <returns> Dequeued value </returns>
    public T Pop() => Dequeue();

    /// <summary>
    /// Dequeue and return the value at the front of the queue.
    /// Throws InvalidOperationException if queue is empty.
    /// </summary>
    /// <returns> Dequeued value </returns>
    public T Dequeue()
    {
        if (IsEmpty)
        { throw new InvalidOperationException("Can't Remove from Empty Queue"); }

        T value = TopKnot!.Value;

        TopKnot = TopKnot.NextKnot;
        _count -= 1;

        if (TopKnot is null)
        {
            LastKnot = null;
        }

        return value;
    }
    #endregion






    #region Getters

    /// <summary>
    /// Return the number of items currently in the queue.
    /// </summary>
    public int Count() => _count;

    /// <summary>
    /// Counts how many of a value are in the queue
    /// </summary>
    public int Count(T value) => Count(item => EqualityComparer<T>.Default.Equals(item, value));

    /// <summary>
    /// Count items that match the provided predicate.
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
    /// Peek at the front value without removing it.
    /// Throws InvalidOperationException if queue is empty.
    /// </summary>
    public T Peek()
    {
        if (TopKnot is not null)
        { return TopKnot.Value; }
        else
        { throw new InvalidOperationException("Can't peek at an empty queue"); }
    }

    /// <summary>
    /// Try to peek at the front value. Returns true if a value was available.
    /// Does not throw.
    /// </summary>
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
    /// Return the value immediately after the front element (second element).
    /// Throws if there is no next value.
    /// </summary>
    public T CheckNextValue()
    {
        if (TopKnot is not null && TopKnot.HasNext)
        { return TopKnot.NextKnot!.Value; }
        else
        { throw new InvalidOperationException("There's no value to check"); }
    }

    /// <summary>
    /// Try to get the second element value. Returns false if not available.
    /// </summary>
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
    /// Return the index of the first item matching the predicate.
    /// Returns null if not found.
    /// </summary>
    public int? GetIndex(Func<T, bool> condition)
    {
        int count = 0;
        foreach (T item in this)
        {
            if (condition(item)) { return count; }
            count++;
        }
        return null;
    }

    /// <summary>
    /// Return the zero-based index of the first item equal to the provided value.
    /// Return null if not found
    /// </summary>
    public int? GetIndex(T value)
    {
        return GetIndex(item => EqualityComparer<T>.Default.Equals(item, value));
    }
    #endregion
    
    
    
    
    
    
    #region Utils

    /// <summary>
    /// Give Enumerator functions to the class
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        var knot = TopKnot;
        while (knot is not null)
        {
            yield return knot.Value!;
            knot = knot.NextKnot;
        }
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Return a string representation like [item1, item2, ..., ]
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
    #endregion
}