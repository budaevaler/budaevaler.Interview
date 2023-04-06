namespace ClosedList;

public class Node<T>
{
    public Node(T? data)
    {
        Data = data;
        Previous = this!;
        Next = this!;
    }

    public Node(T? data, Node<T?> previous, Node<T?> next)
    {
        Data = data;
        Previous = previous;
        Next = next;
    }

    public Node<T?> Previous { get; set; }
    public Node<T?> Next { get; set; }
    public T? Data { get; set; }

    public static Node<T?> Empty()
    {
        return new Node<T?>(default);
    }
}