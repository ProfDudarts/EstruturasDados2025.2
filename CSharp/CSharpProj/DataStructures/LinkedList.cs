using DataStructures.Nodes;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace DataStructures;

/// <summary>
/// A simple singly linked list implementation.
/// </summary>
public class LinkedList<T> : IEnumerable<T>
{
    private Knot<T>? Head = null; // The Top/Root of the List
    private Knot<T>? Tail = null; // The last item
    private int Length = 0; // The count of items in the list
    
    public bool IsEmpty { get { return Length == 0; } } // return true is the list is empty
    public LinkedList() { Clear(); } // class constructor

    /// <summary>
    /// clears the list
    /// </summary>
    public void Clear()
    {
        Head = null;
        Tail = null;
        Length = 0;
    }

    #region Inserters


    /// <summary>
    /// Add a T value at the end of the list
    /// </summary>
    /// <param name="value"> value to be added </param>
    public void Add(T value) => AddTail(value);

    /// <summary>
    /// Add a T value at the end of the list
    /// </summary>
    /// <param name="value"> value to be added </param>
    public void AddTail(T value)
    {
        Knot<T> toAdd = new(value);

        if (IsEmpty)
        {
            StartList(toAdd);
            return;
        }

        Link(Tail, toAdd);
    }

    /// <summary>
    /// Add a T value at the Head of the list
    /// </summary>
    /// <param name="value"></param>
    public void AddHead(T value)
    {
        Knot<T> toAdd = new(value);

        if (IsEmpty)
        {
            StartList(toAdd);
            return;
        }

        Link(null, toAdd);
    }

    /// <summary>
    /// insert a value at the chosen index
    /// </summary>
    /// <param name="value"> value to be added </param>
    /// <param name="index"> the index </param>
    /// <exception cref="IndexOutOfRangeException"> if index goes outside the list </exception>
    public void InsertAt(T value, int index)
    {
        NormalizeIndex(ref index);

        if (index == 0)
        { AddHead(value); return; }
        if (index == Length)
        { AddTail(value); return; }

        CheckIndex(index);

        var toAdd = new Knot<T>(value);

        var prev = GetPreviousTo(index);

        Link(prev, toAdd);
    }
    #endregion






    #region Removers

    /// <summary>
    /// Remove the first occurrence of a value in the list
    /// </summary>
    /// <param name="value"> value to remove </param>
    /// <returns> return true if an item was removed </returns>
    public bool Remove(T value) => Remove(item => EqualityComparer<T>.Default.Equals(item, value));

    /// <summary>
    /// Remove the first occurrence of a value in the list that matches the condition
    /// </summary>
    /// <param name="condition"> condition to check </param>
    /// <returns> return true if an item was removed </returns>
    public bool Remove(Func<T, bool> condition)
    {
        Knot<T>? prev = null;
        foreach (var i in knots())
        {
            if (condition(i.Value))
            {
                Unlink(prev); //Unlink(prev, i)
                return true;
            }
            prev = i;
        }
        return false;
    }

    /// <summary>
    /// Remove all occurrences of a value in the list
    /// </summary>
    /// <param name="value"> value to remove </param>
    /// <returns> return true if at least one item was removed </returns>
    public bool RemoveAll(T value) => RemoveAll(item => EqualityComparer<T>.Default.Equals(item, value));

    /// <summary>
    /// Remove all occurrences of values in the list that match the condition
    /// </summary>
    /// <param name="condition"> condition to check </param>
    /// <returns> return true if at least one item was removed </returns>
    public bool RemoveAll(Func<T, bool> condition)
    {
        bool constRemoved = false;
        bool Removed = false;
        Knot<T>? prev = null;
        foreach (var i in knots())
        {
            if (condition(i.Value))
            {
                Unlink(prev); // Unlink(prev, i)
                Removed = true;
                constRemoved = true;
            }
            if (Removed)
                Removed = false;
            else
                prev = i;
        }
        return constRemoved;
    }

    /// <summary>
    /// Remove and return a value at the chosen index
    /// </summary>
    public T Pop(int index)
    {
        NormalizeIndex(ref index);
        CheckIndex(index);

        var pointer = GetPreviousTo(index);

        T popped = pointer is null ? Head!.Value : pointer.NextKnot!.Value;

        Unlink(pointer);

        return popped;
    }

    /// <summary>
    /// Removes and returns last value in list
    /// </summary>
    /// <returns></returns>
    public T Pop() => Pop(-1);

    #endregion






    #region Getters

    /// <summary>
    /// returns the Length of the list
    /// </summary>
    public int Count() => Length;

    /// <summary>
    /// Counts how many of the same value there is in the list
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int Count(T value) => Count(item => EqualityComparer<T>.Default.Equals(item, value));

    /// <summary>
    /// Counts items that match the provided condition
    /// </summary>
    /// <param name="condition"> condition to match items against </param>
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
    /// Get the index of the first occurrence of a value in the list
    /// </summary>
    /// <param name="value"> value to look for </param>
    /// <returns> returns the index of the item, or null if not found </returns>
    public int? GetIndex(T value) => GetIndex(item => EqualityComparer<T>.Default.Equals(item, value));

    /// <summary>
    /// Get the index of the first occurrence of a value in the list that matches the condition
    /// </summary>
    /// <param name="condition"> condition to check for </param>
    /// <returns> returns the index of the item, or null if not found </returns>
    public int? GetIndex(Func<T, bool> condition)
    {
        var current = Head;
        for (int index = 0; current is not null; index++)
        {
            if (condition(current.Value))
                return index;
            else
                current = current.NextKnot;
        }
        return null;
    }
    #endregion






    #region Helpers

    /// <summary>
    /// Enumerable for every knot in list.
    /// </summary>
    private IEnumerable<Knot<T>> knots()
    {
        var current = Head;
        while (current is not null)
        {
            var next = current.NextKnot;
            yield return current;
            current = next;
        }
    }

    /// <summary>
    /// Unlinks the target Knot from the list. <br/> 
    /// Needs the knot that points to a non null target.<br/>
    /// if pointer is null, unlinks the head
    /// </summary>
    private void Unlink(Knot<T>? previousToTarget)
    {
        if (previousToTarget is not null && previousToTarget.NextKnot is null)
            throw new InvalidOperationException("target can't be null");
        if (IsEmpty)
            throw new InvalidOperationException("Can't Unlink an empty list");

        if (previousToTarget is not null)
            previousToTarget.NextKnot = previousToTarget.NextKnot!.NextKnot;
        else
            Head = Head!.NextKnot;
        Length--;
    }

    /// <summary>
    /// Links a Knot after the target Knot. <br/>
    /// If target is null, links at the start of the list.
    /// </summary>
    private void Link(Knot<T>? previous, Knot<T> toAdd)
    {
        if (previous is not null)
        {
            toAdd.NextKnot = previous.NextKnot;
            previous.NextKnot = toAdd;
        }
        else
        {
            toAdd.NextKnot = Head;
            Head = toAdd;
        }
        Length++;
    }

    private void StartList(Knot<T> toAdd)
    {
            Head = toAdd;
            Tail = toAdd;
            Length++;
            return;
    }

    private Knot<T> GetKnotAt(int index)
    {
        NormalizeIndex(ref index);
        CheckIndex(index);

        var current = Head;
        for (var i = 0; i < index; i++)
        { current = current!.NextKnot; }
        return current!;
    }

    private Knot<T>? GetPreviousTo(int index)
    {
        NormalizeIndex(ref index);
        CheckIndex(index);

        Knot<T>? prev = null;
        var current = Head;
        for (var i = 0; i < index; i++)
        {
            prev = current;
            current = current!.NextKnot;
        }
        return prev;
    }

    private int NormalizeIndex(ref int index)
    {
        return index < 0 ? Length + index : index;
    }

    private void CheckIndex(int index)
    {
        if (index < 0 || index >= Length)
            throw new IndexOutOfRangeException();
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
            NormalizeIndex(ref index);
            CheckIndex(index);

            var pointer = GetKnotAt(index);

            return pointer.Value;

        }
        set
        {
            NormalizeIndex(ref index);
            CheckIndex(index);

            var pointer = GetKnotAt(index);

            pointer.Value = value;
        }
    }

    /// <summary>
    /// gives enumerator functions to the class
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        var knot = Head;
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
        var str = new StringBuilder();
        str.Append('[');
        bool first = true;
        foreach (T item in this)
        {
            if (!first) str.Append(", ");
            str.Append(item is null ? "null" : item.ToString());
            first = false;
        }
        str.Append(']');
        return str.ToString();
    }
    #endregion
}