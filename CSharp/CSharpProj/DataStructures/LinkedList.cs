using DataStructures.Nodes;
using System.Reflection;
using System.Text;

namespace DataStructures;

public class LinkedList<T> : IEnumerable<T>
{
    private Knot<T>? TopKnot = null;
    private Knot<T>? LastKnot = null;

    private int Length = 0;

    public bool IsEmpty { get { return _IsEmpty(); } }

    public LinkedList() { Clear(); }

    public void Clear()
    {
        TopKnot = null;
        LastKnot = null;
        Length = 0;
    }


#region Inserters
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
    public bool Remove(T value)
    {
        return Remove(item => EqualityComparer<T>.Default.Equals(item, value));
    }
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
    
    public bool RemoveAll(T value)
    {
        return RemoveAll(item => EqualityComparer<T>.Default.Equals(item, value));
    }
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
    public int Count()
    {
        return Length;
    }

    private bool _IsEmpty()
    {
        return TopKnot is null;
    }

    public int GetIndex(T value)
    {
        return GetIndex(item => EqualityComparer<T>.Default.Equals(item, value));
    }
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