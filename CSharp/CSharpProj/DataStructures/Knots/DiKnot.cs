namespace DataStructures.Nodes;

public class DiKnot<T>
{
    public DiKnot<T>? NextKnot = null;
    public DiKnot<T>? PreviousKnot = null;

    public T Value;

    public bool HasNext { get { return NextKnot is DiKnot<T>; } }
    public bool HasPrevious { get { return PreviousKnot is DiKnot<T>; } }

    public DiKnot(T value)
    {
        Value = value;
    }

    public DiKnot(T value, DiKnot<T> previous)
    {
        Value = value;
        PreviousKnot = previous;
    }

    public DiKnot(T value, DiKnot<T> previous, DiKnot<T> next)
    {
        Value = value;
        PreviousKnot = previous;
        NextKnot = next;
    }

    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }

}
