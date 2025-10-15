namespace DataStructures.Knots;

public class DiKnot<T>
{
    private DiKnot<T>? _nextKnot = null;
    private DiKnot<T>? _previousKnot = null;

    private T _value;

    public DiKnot<T>? NextKnot { get { return _nextKnot; } set { _nextKnot = value; } }
    public DiKnot<T>? PreviousKnot { get { return _previousKnot; } set { _previousKnot = value; } }
    public T Value { get { return _value; } set { _value = value; } }

    public bool HasNext { get { return _nextKnot is DiKnot<T>; } }

    public DiKnot(T value)
    {
        _value = value;
        _nextKnot = null;
        _previousKnot = null;
    }

    public DiKnot(T value, DiKnot<T> nextKnot, DiKnot<T> preKnot)
    {
        _value = value;
        _nextKnot = nextKnot;
        _previousKnot = preKnot;
    }
  
}
