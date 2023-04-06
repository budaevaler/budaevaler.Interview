using System.Collections;
using ClosedList.Interfaces;

namespace ClosedList;

internal class LinkedClosedList<T> : IClosedList<T?>
{
    private Node<T?> _head;
    private Node<T?> _current;

    public LinkedClosedList()
    {
        _head = Node<T?>.Empty();
        _current = Node<T?>.Empty();
    }

    public LinkedClosedList(IEnumerable<T> elements) : this()
    {
        foreach (var element in elements)
        {
            Add(element);
        }
    }

    public T? this[int index]
    {
        get
        {
            if (index >= Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            return GetNodeAt(index).Data;
        }
        set
        {
            if (index >= Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            GetNodeAt(index).Data = value;
        }
    }

    public T? Head => IsEmpty ? default : _head.Data;

    public T? Current => IsEmpty ? default : _current.Data;

    public T? Previous => IsEmpty ? default : _current.Previous.Data;

    public T? Next => IsEmpty ? default : _current.Next.Data;

    public int Count { get; private set; }

    public bool IsReadOnly => false;

    public event EventHandler<T?>? HeadReached;

    public void Add(T? item)
    {
        if (IsEmpty)
            AddFirst(item);
        else
            AddBetween(item, _head.Previous, _head);
    }

    public void Clear()
    {
        _head = Node<T>.Empty();
        _current = Node<T>.Empty();
        Count = 0;
    }

    public bool Contains(T? item)
    {
        if (IsEmpty)
            return false;
        var current = _head;
        do
        {
            if (current.Data != null && current.Data.Equals(item))
                return true;
            current = current.Next;
        } while (current != _head);

        return false;
    }

    public void CopyTo(T?[] array, int arrayIndex)
    {
        if (IsEmpty)
            return;
        var current = _head;
        do
        {
            array[arrayIndex++] = current.Data;
            current = current.Next;
        } while (current != _head);
    }

    public IEnumerator<T?> GetEnumerator()
    {
        if (IsEmpty)
            yield break;
        var current = _head;
        do
        {
            yield return current.Data;
            current = current.Next;
        } while (current != _head);
    }

    public int IndexOf(T? item)
    {
        var index = 0;
        var current = _head;
        if (IsEmpty)
            return -1;
        do
        {
            if (current.Data != null && current.Data.Equals(item))
                return index;
            index++;
            current = current.Next;
        } while (current != _head);

        return -1;
    }

    public void Insert(int index, T? item)
    {
        if (index > Count || index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (IsEmpty)
            AddFirst(item);
        else
        {
            var node = GetNodeAt(index);
            AddBetween(item, node.Previous, node);
        }
    }

    public void MoveBack(int step = 1)
    {
        if (step >= 0)
            MoveBackward(step);
        else
            MoveForward(Math.Abs(step));
    }

    public void MoveNext(int step = 1)
    {
        if (step >= 0)
            MoveForward(step);
        else
            MoveBackward(Math.Abs(step));
    }

    public bool Remove(T? item)
    {
        if (IsEmpty)
            return false;
        var current = _head;
        if (Count == 1)
        {
            if (_head.Data is not null && !_head.Data.Equals(item) ||
                _head.Data is null && item is not null)
                return false;
            Clear();
            return true;
        }

        do
        {
            if (_head.Data is not null && !_head.Data.Equals(item) ||
                _head.Data is null && item is not null)
                continue;
            RemoveBetween(current.Previous, current.Next);
            return true;
        } while (current != _head);

        return false;
    }

    public void RemoveAt(int index)
    {
        if (index >= Count || index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));
        if (Count == 1)
        {
            Clear();
            return;
        }

        var node = GetNodeAt(index);
        RemoveBetween(node.Previous, node.Next);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)this).GetEnumerator();
    }

    private bool IsEmpty => Count == 0;

    private void AddFirst(T? item)
    {
        var node = new Node<T?>(item);
        _head = node;
        _current = node;
        Count++;
    }

    private void AddBetween(T? item, Node<T?> first, Node<T?> second)
    {
        var node = new Node<T?>(item, first, second);
        first.Next = node;
        second.Previous = node;
        Count++;
    }

    private void RemoveBetween(Node<T?> first, Node<T?> second)
    {
        first.Next = second;
        second.Previous = first;
        Count--;
    }

    private Node<T?> GetNodeAt(int index)
    {
        var current = _head;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        return current;
    }

    private void MoveForward(int step)
    {
        for (int i = 0; i < step; i++)
        {
            _current = _current.Next;
            if (_current == _head)
                HeadReached?.Invoke(this, _head.Data);
        }
    }

    private void MoveBackward(int step)
    {
        for (int i = 0; i < step; i++)
        {
            _current = _current.Previous;
            if (_current == _head.Previous)
                HeadReached?.Invoke(this, _head.Data);
        }
    }
}