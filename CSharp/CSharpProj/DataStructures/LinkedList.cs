using DataStructures.Nodes;
using System.Reflection;
using System.Text;

namespace DataStructures;

/// <summary>
/// A simple singly linked list implementation.
/// </summary>
public class LinkedList<T> : IEnumerable<T>
{
    private Knot<T>? TopKnot = null; // The Top/Root of the List
    private Knot<T>? LastKnot = null; // The last item

    private int Length = 0; // The count of items in the list
    public bool IsEmpty { get { return _IsEmpty(); } } // return true is the list is empty
    public LinkedList() { Clear(); } // class constructor

    /// <summary>
    /// clears the list
    /// </summary>
    public void Clear()
    {
        TopKnot = null;
        LastKnot = null;
        Length = 0;
    }


    #region Inserters

    /// <summary>
    /// Add a T value at the end of the list
    /// </summary>
    /// <param name="value"> value to be added </param>
    public void Add(T value)
    {
        Knot<T> knotToAdd = new(value);

        if (IsEmpty)
        {
            TopKnot = knotToAdd;
            LastKnot = knotToAdd;
            Length++;
            return;
        }

        LastKnot!.NextKnot = knotToAdd;
        LastKnot = knotToAdd;
        Length++;
    }

    /// <summary>
    /// insert a value at the chosen index
    /// </summary>
    /// <param name="value"> value to be added </param>
    /// <param name="index"> the index </param>
    /// <exception cref="IndexOutOfRangeException"> if index goes outside the list </exception>
    public void Insert(T value, int index)
    {
        if (index < 0)
            index = Length + index;

        if (index < 0 || Length <= index)
            throw new IndexOutOfRangeException();

        if (index == 0)
        {
            TopKnot = new Knot<T>(value, TopKnot);
            Length++;
            return;
        }

        var prev = TopKnot;
        var current = TopKnot!.NextKnot;

        for (int i = 0; i < index; i++)
        {
            prev = current;
            current = current!.NextKnot;
        }
        prev!.NextKnot = new Knot<T>(value, current);
        Length++;
    }
    #endregion






    #region Removers

    /// <summary>
    /// Remove the first occurrence of a value in the list
    /// </summary>
    /// <param name="value"> value to remove </param>
    /// <returns> return true if an item was removed </returns>
    public bool Remove(T value)
    {
        return Remove(item => EqualityComparer<T>.Default.Equals(item, value));
    }

    /// <summary>
    /// Remove the first occurrence of a value in the list that matches the condition
    /// </summary>
    /// <param name="condition"> condition to check </param>
    /// <returns> return true if an item was removed </returns>
    public bool Remove(Func<T, bool> condition)
    {

        if (TopKnot is null)
            return false;

        if (condition(TopKnot.Value))
        {
            TopKnot = TopKnot.NextKnot;
            Length--;
            return true;
        }

        var prev = TopKnot;
        var current = TopKnot.NextKnot;

        while (current is not null)
        {
            if (condition(current.Value))
            {
                prev.NextKnot = current.NextKnot;
                Length--;
                return true;
            }

            prev = current;
            current = current.NextKnot;

        }

        return false;
    }

    /// <summary>
    /// Remove all occurrences of a value in the list
    /// </summary>
    /// <param name="value"> value to remove </param>
    /// <returns> return true if at least one item was removed </returns>
    public bool RemoveAll(T value)
    {
        return RemoveAll(item => EqualityComparer<T>.Default.Equals(item, value));
    }

    /// <summary>
    /// Remove all occurrences of values in the list that match the condition
    /// </summary>
    /// <param name="condition"> condition to check </param>
    /// <returns> return true if at least one item was removed </returns>
    public bool RemoveAll(Func<T, bool> condition)
    {
        bool removed = false;

        if (IsEmpty)
            return false;

        while (TopKnot is not null && condition(TopKnot.Value))
        {
            TopKnot = TopKnot.NextKnot;
            Length--;
            removed = true;
        }

        if (IsEmpty)
            return removed;

        Knot<T> prev = TopKnot!;
        Knot<T>? current = TopKnot!.NextKnot;

        while (current is not null)
        {
            if (condition(current.Value))
            {
                prev.NextKnot = current.NextKnot;
                current = current.NextKnot;
                Length--;
                removed = true;
            }
            else
            {
                prev = current;
                current = current.NextKnot;
            }
        }

        return removed;
    }

    /// <summary>
    /// Remove and return a value at the chosen index
    /// </summary>
    /// <param name="index"> the index to remove the value from
    /// if null, removes the last item </param>
    /// <returns> the removed value </returns>
    /// <exception cref="InvalidOperationException"> if the list is empty </exception>
    /// <exception cref="IndexOutOfRangeException"> if index goes outside the list </exception>
    public T Pop(int? index = null)
    {
        if (IsEmpty)
            throw new InvalidOperationException("Can't Pop an Empty List");
        if (index is null)
            return Pop(-1);
        if (index < 0)
            index = Length + index;
        if (index < 0 || Length <= index)
            throw new IndexOutOfRangeException();

        T popped;

        if (index == 0)
        {
            popped = TopKnot!.Value;
            TopKnot = TopKnot.NextKnot;
            Length--;
            return popped;
        }

        Knot<T> prev = TopKnot!;
        Knot<T>? current = TopKnot!.NextKnot;

        // "current" may be bugged
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

        for (int i = 1; i < index; i++)
        {
            prev = current;
            current = current.NextKnot;
        }

        popped = current.Value;
        prev.NextKnot = current.NextKnot;
        Length--;

#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        return popped;
    }
    #endregion






    #region Getters

    /// <summary>
    /// returns the Length of the list
    /// </summary>
    public int Count()
    {
        return Length;
    }

    /// <summary>
    /// Count items that match the provided predicate.
    /// </summary>
    /// <param name="condition"> Predicate to match items against </param>
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

    private bool _IsEmpty()
    {
        return TopKnot is null;
    }

    /// <summary>
    /// Get the index of the first occurrence of a value in the list
    /// </summary>
    /// <param name="value"> value to look for </param>
    /// <returns> returns the index of the item </returns>
    public int GetIndex(T value)
    {
        return GetIndex(item => EqualityComparer<T>.Default.Equals(item, value));
    }

    /// <summary>
    /// Get the index of the first occurrence of a value in the list that matches the condition
    /// </summary>
    /// <param name="condition"> condition to check </param>
    /// <returns> returns the index of the item, or -1 if not found </returns>
    public int GetIndex(Func<T, bool> condition)
    {
        var current = TopKnot;
        var count = 0;
        while (current is not null)
        {
            if (condition(current.Value))
            {
                return count;
            }
            else
            {
                current = current.NextKnot;
                count++;
            }
        }
        return -1;
    }
    #endregion






    #region Nice

    /// <summary>
    /// for quick get and set
    /// </summary>
    /// <param name="index"> index of the item </param>
    /// <exception cref="IndexOutOfRangeException"> if index goes outside the list or list is empty </exception>
    public T this[int index]
    {
        get
        {
            if (index < 0)
                index = Length + index;

            if (index < 0 || index >= Length || IsEmpty)
                throw new IndexOutOfRangeException();

            Knot<T> pointer = TopKnot!;

            for (int i = 0; i < index; i++)
                pointer = pointer.NextKnot!;

            return pointer.Value;

        }
        set
        {
            if (index < 0)
                index = Length + index;

            if (index < 0 || index >= Length || IsEmpty)
                throw new IndexOutOfRangeException();

            Knot<T> pointer = TopKnot!;

            for (int i = 0; i < index; i++)
                pointer = pointer.NextKnot!;

            pointer.Value = value;
        }
    }

    /// <summary>
    /// gives enumerator functions to the class
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
    /// makes the this into a string
    /// </summary>
    public override string ToString()
    {
        string _base = "[";
        foreach (T i in this)
        {
            _base += i?.ToString();
            _base += ", ";
        }
        return _base += "]";
    }
    #endregion
}