namespace DataStructures.Nodes;

public class Node<T>
{
    public Node<T>? Left = null;
    public Node<T>? Right = null;

    public T Value;

    public Node(T value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }

}
