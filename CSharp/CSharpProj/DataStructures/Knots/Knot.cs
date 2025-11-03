namespace DataStructures.Nodes;

public class Knot<T>
{
    public Knot<T>? NextKnot = null;

    public T Value;

    public bool HasNext { get { return NextKnot is Knot<T>; } }

    public Knot(T value)
    {
        Value = value;
        NextKnot = null;
    }

    public Knot(T value, Knot<T>? knot)
    {
        Value = value;
        NextKnot = knot;
    }

    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }

}
