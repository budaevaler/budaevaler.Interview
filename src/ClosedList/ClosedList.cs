using System.Collections;
using ClosedList.Interfaces;

namespace ClosedList;

internal sealed class ClosedList<T> : IClosedList<T?>
{
    private readonly List<T?> _list;
    private int _position;

    public ClosedList()
    {
        _list = new List<T?>();
    }

    public ClosedList(IEnumerable<T?> elements)
    {
        _list = elements.ToList();
    }

    public T? this[int index]
    {
        get => _list[index];
        set => _list[index] = value; 
    }

    public int Count => _list.Count;

    public bool IsReadOnly => false;

    public T? Head => _list.Count == 0 ? default : _list[0];

    public T? Current => _list.Count == 0 ? default : _list[_position];

    public T? Previous
    {
        get
        {
            if (IsEmpty)
                return default;
            return _position == 0 ? _list[^1] : _list[_position - 1];
        }
    }

    public T? Next
    {
        get
        {
            if (_list.Count == 0)
                return default;
            return _position == _list.Count - 1 ? _list[0] : _list[_position + 1];
        }
    }

    public event EventHandler<T?>? HeadReached;

    public void Add(T? item) => _list.Add(item);

    public void Clear() => _list.Clear();

    public bool Contains(T? item) => _list.Contains(item);

    public void CopyTo(T?[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

    public int IndexOf(T? item) => _list.IndexOf(item);

    public void Insert(int index, T? item) => _list.Insert(index, item);

    public void MoveBack(int step = 1)
    {
        if (_list.Count == 0)
            return;

        _position = GetPositionInsideList(_position -= step);
    }

    public void MoveNext(int step = 1)
    {
        if (_list.Count == 0)
            return;

        _position = GetPositionInsideList(_position += step);
    }

    public bool Remove(T? item) => _list.Remove(item);

    public void RemoveAt(int index) => _list.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private bool IsEmpty => _list.Count == 0;
    
    private int GetPositionInsideList(int position)
    {
        while (position >= _list.Count)
        {
            HeadReached?.Invoke(this, Head);
            position -= _list.Count;
        }

        while (position < 0)
        {
            HeadReached?.Invoke(this, Head);
            position += _list.Count;
        }

        return position;
    }
}