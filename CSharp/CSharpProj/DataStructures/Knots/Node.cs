namespace DataStructures.Knots;

public class Node<T>
{
    public Node<T>? Left = null;
    public Node<T>? Right = null;

    public T Value;

    public Node(T value)
    {
        Value = value;
    }

    public Node(T value, Node<T> left, Node<T> right)
    {
        Value = value;
        Right = right;
        Left = left;
    }

    public override string ToString()
    {
        if (Value is not null)
        {
            return Value.ToString()!;
        }
        else
        {
            return string.Empty;
        }

    }
}
