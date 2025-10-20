using DataStructures.Nodes;
using System.Reflection;
using System.Text;

namespace DataStructures;

/// <summary>
/// A simple doubly-linked list implementation.
/// </summary>
public class BiLinkedList<T> : IEnumerable<T>
{
    private DiKnot<T>? Head = null; // The Top/Root of the List
    private DiKnot<T>? Tail = null; // The last item

    private int Length = 0; // The count of items in the list

    public bool IsEmpty { get { return Length == 0; } } // return true is the list is empty

    public BiLinkedList() { Clear(); } // class constructor

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
    /// Add a T value at the end of the list <br/>
    /// This is an alias for AddEnd(T value).
    /// </summary>
    /// <param name="value"> value to be added </param>
    public void Add(T value) => AddEnd(value);

    /// <summary>
    /// Add a T value at the end of the list
    /// </summary>
    /// <param name="value"> value to be added </param>
    public void AddEnd(T value)
    {
        var toAdd = CreateNode(value);

        if (IsEmpty)
        {
            StartList(toAdd);
            return;
        }

        LinkAlt(toAdd, Tail!);
    }

    /// <summary>
    /// Add a T value at the Front of the list
    /// </summary>
    /// <param name="value"> value to be added </param>
    public void AddFront(T value)
    {
        var toAdd = CreateNode(value);

        if (IsEmpty)
        {
            StartList(toAdd);
            return;
        }

        Link(toAdd, Head!);
    }

    /// <summary>
    /// insert a value at the chosen index.
    /// Decides if it's better to traverse from the head or tail
    /// </summary>
    /// <param name="value"> value to be added </param>
    /// <param name="index"> the index </param>
    /// <exception cref="IndexOutOfRangeException"> if index goes outside the list </exception>
    public void InsertAt(T value, int index, bool alt = false)
    {
        NormalizeIndex(ref index);

        if (index == 0)
        { AddFront(value); return; }
        if (index == Length)
        { AddEnd(value); return; }

        CheckIndex(index);

        var toAdd = CreateNode(value);
        var target = GetNodeAt(index);

        if (alt)
            LinkAlt(toAdd, target);
        else
            Link(toAdd, target);
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
    public bool RemoveAll(T value) => RemoveAll(item => EqualityComparer<T>.Default.Equals(item, value));

    /// <summary>
    /// Remove all occurrences of values in the list that match the condition
    /// </summary>
    /// <param name="condition"> condition to check </param>
    /// <returns> return true if at least one item was removed </returns>
    public bool RemoveAll(Func<T, bool> condition)
    {
        var removed = false;
        foreach (var node in Nodes())
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
    /// <param name="index"> the index of the item
    /// <returns> the removed value </returns>
    /// <exception cref="InvalidOperationException"> if the list is empty </exception>
    public T Pop(int index)
    {
        if (IsEmpty)
            throw new InvalidOperationException("Can't pop an empty list");

        NormalizeIndex(ref index);
        CheckIndex(index);

        var target = GetNodeAt(index);

        Unlink(target);

        return target.Value;
    }

    /// <summary>
    /// Remove and returns the end of the list
    /// </summary>
    public T Pop() => Pop(-1);

    #endregion






    #region Getters

    /// <summary>
    /// returns the Length of the list
    /// </summary>
    public int Count() => Length;

    /// <summary>
    /// Counts how many of a value are in the list
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int Count(T value) => Count(item => EqualityComparer<T>.Default.Equals(item, value));

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

    /// <summary>
    /// Get the index of the first occurrence of a value in the list
    /// </summary>
    /// <param name="value"> value to look for </param>
    /// <returns> returns the index of the item </returns>
    public int? GetIndex(T value) => GetIndex(item => EqualityComparer<T>.Default.Equals(item, value));

    /// <summary>
    /// Get the index of the first occurrence of a value in the list that matches the condition
    /// </summary>
    /// <param name="condition"> condition to check </param>
    /// <returns> returns the index of the item, or null if not found </returns>
    public int? GetIndex(Func<T, bool> condition)
    {
        var current = Head;
        for (int index = 0; current is not null; index++)
        {
            if (condition(current.Value))
                return index;

            current = current.NextKnot;
        }
        return null;
    }

    #endregion






    #region Helpers

    /// <summary>
    /// Normalize an index, converting negative indexes to positive ones
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"> throws if index is out of range </exception>
    private void NormalizeIndex(ref int index)
    {
        if (index < 0)
            index = Length + index;
    }

    /// <summary>
    /// throws if index is out of range
    /// </summary>
    private void CheckIndex(int index)
    {
        if (index < 0 || index >= Length)
            throw new IndexOutOfRangeException();
    }

    /// <summary>
    /// Get the node at a specific index, decides if it's better to traverse from Head or Tail. Uses a Raw index
    /// </summary>
    private DiKnot<T> GetNodeAt(int index)
    {
        if (CheckBestPath(index))
            return GetNodeAt_Head(index);
        else
            return GetNodeAt_Tail(index);
    }

    /// <summary>
    /// if true from the head is better <br/>
    /// if false from the tail is better
    /// </summary>
    private bool CheckBestPath(int index) => (Length / 2) > ((index < 0) ? Length + index : index);

    /// <summary>
    /// Get the node at a specific index, traverses from Head
    /// </summary>
    /// <param name="index"> the index is not normalized and can not be out of range </param>
    private DiKnot<T> GetNodeAt_Head(int index)
    {
        CheckIndex(index);

        var current = Head!;
        for (int i = 0; i < index; i++)
            current = current.NextKnot!;
        return current;
    }

    /// <summary>
    /// Get the node at a specific index, traverses from Tail
    /// </summary>
    /// <param name="index"> the index is not normalized and can not be out of range </param>
    private DiKnot<T> GetNodeAt_Tail(int index)
    {
        CheckIndex(index);

        var current = Tail!;
        for (int i = Length - 1 ; i > index; i--)
            current = current.PreviousKnot!;
        return current;
    }

    /// <summary>
    /// Alias of Nodes_Head() <br/>
    /// Enumerate nodes from Head to Tail
    /// </summary>
    private IEnumerable<DiKnot<T>> Nodes() => Nodes_Head();

    /// <summary>
    /// Enumerate nodes from Head to Tail
    /// </summary>
    private IEnumerable<DiKnot<T>> Nodes_Head()
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
    /// Enumerate nodes from Tail to Head
    /// </summary>
    private IEnumerable<DiKnot<T>> Nodes_Tail()
    {
        var current = Tail;
        while (current is not null)
        {
            var next = current.PreviousKnot;
            yield return current;
            current = next;
        }
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

    /// <summary>
    /// Link a node before the target node
    /// </summary>
    private void Link(DiKnot<T> toAdd, DiKnot<T> target)
    {
        toAdd.NextKnot = target;
        toAdd.PreviousKnot = target.PreviousKnot;

        if (target.PreviousKnot is not null)
            target.PreviousKnot.NextKnot = toAdd;
        else
            Head = toAdd;

        target.PreviousKnot = toAdd;
        Length++;
    }

    /// <summary>
    /// Link a node after the target node
    /// </summary>
    private void LinkAlt(DiKnot<T> toAdd, DiKnot<T> target)
    {
        toAdd.PreviousKnot = target;
        toAdd.NextKnot = target.NextKnot;

        if (target.NextKnot is not null)
            target.NextKnot.PreviousKnot = toAdd;
        else
            Tail = toAdd;

        target.NextKnot = toAdd;
        Length++;
    }

    /// <summary>
    /// Starts the List with a value
    /// </summary>
    /// <param name="toAdd"></param>
    /// <exception cref="InvalidOperationException"> Can't start if list is not empty </exception>
    private void StartList(DiKnot<T> toAdd)
    {
        if (!IsEmpty) throw new InvalidOperationException("Can't Start a non empty list");

        Tail = toAdd;
        Head = toAdd;
        Length++;
    }

    /// <summary>
    /// Creates a new node
    /// </summary>
    private DiKnot<T> CreateNode(T value) => new DiKnot<T>(value);

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

            NormalizeIndex(ref index);
            CheckIndex(index);

            var target = GetNodeAt(index);

            return target.Value;
        }
        set
        {
            if (IsEmpty)
                throw new InvalidOperationException();

            NormalizeIndex(ref index);
            CheckIndex(index);

            var target = GetNodeAt(index);

            target.Value = value;
        }
    }

    /// <summary>
    /// gives enumerator functions to the class
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        var current = Head;
        while (current is not null)
        {
            var next = current.NextKnot;
            yield return current.Value;
            current = next;
        }
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// gives enumerator functions to the class but from the back
    /// </summary> 
    public IEnumerable<T> ReadTail()
    {
        var current = Tail;
        while (current is not null)
        {
            var prev = current.PreviousKnot;
            yield return current.Value;
            current = prev;
        }
    }

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