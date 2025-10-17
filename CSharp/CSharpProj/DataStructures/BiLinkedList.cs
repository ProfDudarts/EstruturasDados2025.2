using DataStructures.Nodes;
using System.Reflection;
using System.Text;

namespace DataStructures;

/// <summary>
/// A simple singly linked list implementation.
/// </summary>
public class DiLinkedList<T> : IEnumerable<T>
{
    private DiKnot<T>? Head = null; // The Top/Root of the List
    private DiKnot<T>? Tail = null; // The last item

    private int Length = 0; // The count of items in the list

    public bool IsEmpty { get { return _IsEmpty(); } } // return true is the list is empty

    public DiLinkedList() { Clear(); } // class constructor

    /// <summary>
    /// Clears the list
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
    /// This is an alias for AddEnd(T value).
    /// </summary>
    /// <param name="value"> value to be added </param>
    public void Add(T value) => Add_Tail(value);

    /// <summary>
    /// Add a T value at the end of the list
    /// </summary>
    /// <param name="value"> value to be added </param>
    public void Add_Tail(T value)
    {
        var toAdd = new DiKnot<T>(value);

        if (IsEmpty)
        {
            Tail = toAdd;
            Head = toAdd;
            Length++;
            return;
        }

        Tail!.NextKnot = toAdd;
        toAdd.PreviousKnot = Tail;
        Tail = toAdd;
        Length++;
    }

    /// <summary>
    /// Add a T value at the Front of the list
    /// </summary>
    /// <param name="value"> value to be added </param>
    public void Add_Head(T value)
    {
        var toAdd = new DiKnot<T>(value);

        if (IsEmpty)
        {
            Tail = toAdd;
            Head = toAdd;
            Length++;
            return;
        }

        Head!.PreviousKnot = toAdd;
        toAdd.NextKnot = Head;
        Head = toAdd;
        Length++;
    }

    /// <summary>
    /// insert a value at the chosen index.
    /// Decides if it's better to traverse from the head or tail
    /// </summary>
    /// <param name="value"> value to be added </param>
    /// <param name="index"> the index </param>
    /// <exception cref="IndexOutOfRangeException"> if index goes outside the list </exception>
    public void Insert(T value, int index)
    {
        if ((Length/2) < ((index < 0)? Length - index : index))
        { Insert_Tail(value, index); }
        else
        { InsertHead(value, index); } 
    }

    /// <summary>
    /// insert a value at the chosen index, traversing from the Head
    /// </summary>
    /// <param name="value"> value to be added </param>
    /// <param name="index"> the index </param>
    /// <exception cref="IndexOutOfRangeException"> if index goes outside the list </exception>
    public void InsertHead(T value, int index)
    {
        if (index < 0)
            index = Length + index;

        if (index < 0 || Length < index)
            throw new IndexOutOfRangeException();

        if (index == 0)
            { Add_Head(value); return; }

        if (index == Length)
            { Add_Tail(value); return; }

        var toAdd = new DiKnot<T>(value);

        var current = Head!.NextKnot;
        for (int i = 1; i < index; i++)
            current = current!.NextKnot;

        toAdd.NextKnot = current;
        toAdd.PreviousKnot = current!.PreviousKnot;
        current.PreviousKnot!.NextKnot = toAdd;
        current.PreviousKnot = toAdd;
        Length++;
    }

    /// <summary>
    /// insert a value at the chosen index, traversing from the Tail
    /// </summary>
    /// <param name="value"> value to be added </param>
    /// <param name="index"> the index </param>
    /// <exception cref="IndexOutOfRangeException"> if index goes outside the list </exception>
    public void Insert_Tail(T value, int index)
    {
        if (index < 0)
            index = Length + index;

        if (index < 0 || Length < index)
            throw new IndexOutOfRangeException();

        if (index == 0)
            { Add_Head(value); return; }

        if (index == Length)
            { Add_Tail(value); return; }

        var toAdd = new DiKnot<T>(value);

        var current = Tail!.PreviousKnot;
        for (int i = Length - 2; i > index; i--)
            current = current!.PreviousKnot;

        toAdd.PreviousKnot = current;
        toAdd.NextKnot = current!.NextKnot;
        current.NextKnot!.PreviousKnot = toAdd;
        current.NextKnot = toAdd;
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
        foreach (var node in Nodes())
        {
            if (condition(node.Value))
            {
                Unlink(node);
                return true;
            }
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
        var removed = false;
        foreach (var node in Nodes().ToList())
        {
            if (condition(node.Value))
            {
                Unlink(node);
                removed = true;
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
    public T Pop(int? index = null)
    {
        if (IsEmpty)
            throw new InvalidOperationException("Can't pop an empty list");

        int i = NormalizeIndex(index ?? -1);
        var target = GetNodeAt(i);

        Unlink(target);

        return target.Value;
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
        return Length == 0;
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
        var current = Head;
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






    #region Helpers

    /// <summary>
    /// Normalize an index, converting negative indexes to positive ones
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"> throws if index is out of range </exception>
    private int NormalizeIndex(int index)
    {
        if (index < 0)
            index = Length + index;
        if (index < 0 || index >= Length)
            throw new IndexOutOfRangeException();
        return index;
    }

    /// <summary>
    /// Get the node at a specific index, decides if it's better to traverse from Head or Tail
    /// </summary>
    private DiKnot<T> GetNodeAt(int index)
    {
        if ((Length / 2) > ((index < 0) ? Length - index : index))
        { return GetNodeAt_Head(index); }
        else
        { return GetNodeAt_Tail(index); }

    }

    /// <summary>
    /// Get the node at a specific index, traverses from Head
    /// </summary>
    /// <param name="index"> the index can not be out of range </param>
    private DiKnot<T> GetNodeAt_Head(int index)
    {
        if (index < 0 || index >= Length)
            throw new IndexOutOfRangeException();

        var current = Head!;
        for (int i = 0; i < index; i++)
            current = current.NextKnot!;
        return current;
    }

    /// <summary>
    /// Get the node at a specific index, traverses from Tail
    /// </summary>
    /// <param name="index"> the index can not be out of range </param>
    private DiKnot<T> GetNodeAt_Tail(int index)
    {
        if (index < 0 || index >= Length)
            throw new IndexOutOfRangeException();

        var current = Tail!;
        for (int i = Length - 1 ; i > index; i--)
            current = current.PreviousKnot!;
        return current;
    }

    /// <summary>
    /// Unlink a node from the list
    /// </summary>
    private void Unlink(DiKnot<T> node)
    {

        var prev = node.PreviousKnot;
        var next = node.NextKnot;

        if (prev is not null)
            prev.NextKnot = next;
        else
            Head = next;

        if (next is not null)
            next.PreviousKnot = prev;
        else
            Tail = prev;

        Length--;
    }

    private IEnumerable<DiKnot<T>> Nodes() => Nodes_Head();

    private IEnumerable<DiKnot<T>> Nodes_Head()
    {
        var current = Head;
        while (current is not null)
        {
            yield return current;
            current = current.NextKnot;
        }
    }
    private IEnumerable<DiKnot<T>> Nodes_Tail()
    {
        var current = Tail;
        while (current is not null)
        {
            yield return current;
            current = current.PreviousKnot;
        }
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
            if (IsEmpty)
                throw new InvalidOperationException();

            var i = NormalizeIndex(index);

            DiKnot<T> target = GetNodeAt(i);

            return target.Value;
        }
        set
        {
            if (IsEmpty)
                throw new InvalidOperationException();

            var i = NormalizeIndex(index);

            DiKnot<T> target = GetNodeAt(i);

            target.Value = value;
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
    /// gives enumerator functions to the class but from the back
    /// </summary> 
    public IEnumerator<T> ReadTail()
    {
        var knot = Tail;
        while (knot is not null)
        {
            yield return knot.Value;
            knot = knot.PreviousKnot;
        }
    }

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