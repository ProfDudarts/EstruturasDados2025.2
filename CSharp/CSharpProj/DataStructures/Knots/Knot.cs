namespace DataStructures.Knots;

public class Knot<T>
{
    private Knot<T>? _nextKnot = null;
    private T _value;

    public Knot<T>? NextKnot { get { return _nextKnot; } set { _nextKnot = value; } }
    public T Value { get { return _value; } set { _value = value; } }

    public bool HasNext { get { return _nextKnot is Knot<T>; } }

    public Knot(T value)
    {
        _value = value;
        _nextKnot = null;
    }

    public Knot(T value, Knot<T>? knot)
    {
        _value = value;
        _nextKnot = knot;
    }


    public void Tie(Knot<T>? knot)
    {
        NextKnot = knot;
    }
  
}
