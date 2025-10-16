using DataStructures.Nodes;
using System.Text;

namespace DataStructures;

public class Stack<T> : IEnumerable<T>
{
    private int _count = 0;
    private Knot<T>? TopKnot = null;

    public bool IsEmpty { get { return _count == 0; } }

    public Stack() { Clear(); }


    public void Clear()
    {
        _count = 0;
        TopKnot = null;
    }


    public void Add(T value)
    {
        Knot<T> knotToAdd = new(value, TopKnot);
        TopKnot = knotToAdd;
        _count += 1;
    }

    public T Pop()
    {
        if (IsEmpty)
        { throw new IndexOutOfRangeException("Can't Remove from Empty Stack"); }

        T value = TopKnot!.Value;

        TopKnot = TopKnot.NextKnot;
        _count -= 1;

        return value;
    }


    public int Count()
    { return _count; }

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

    public T Peek()
    {
        if (TopKnot is not null)
        { return TopKnot.Value; }
        else
        { throw new InvalidOperationException("Can't peek at an empty stack"); }
    }

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

    public T CheckNextValue()
    {
        if (TopKnot is not null && TopKnot.HasNext)
        { return TopKnot.NextKnot!.Value; }
        else
        { throw new InvalidOperationException("There's no value to check"); }
    }

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
    public int GetIndex(T value)
    {
        int count = 0;
        foreach (T item in this)
        {
            if (EqualityComparer<T>.Default.Equals(item, value))
            { return count; }
            count++;
        }
        return -1;
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
        StringBuilder builder = new();

        builder.Append('[');

        foreach (T i in this)
        {
            if (i is null)
            {
                builder.Append("");
            }
            else
            {
                builder.Append(i.ToString());
            }
            builder.Append(", ");
        }
        builder.Remove(builder.Length - 2, 2);
        builder.Append(']');

        return builder.ToString();
    }
}