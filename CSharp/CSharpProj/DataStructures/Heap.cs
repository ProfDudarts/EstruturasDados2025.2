using System.Text;

namespace DataStructures;

// 
// A array that acts like a tree
// 

/// <summary>
/// A simple implementation of a min-Heap <br/>
/// Can be made into a max-Heap by giving another comparer
/// </summary>
public class MyHeap<T> : IEnumerable<T> where T : IComparable<T>
{
    private List<T> _data = []; // the main array
    public int Count => _data.Count; // count of items
    public bool IsEmpty => _data.Count == 0; // true is heap is empty

    private readonly IComparer<T> _comparer = Comparer<T>.Default; // comparer
    private readonly bool MaxHeap = false; // if it acts as a max heap

    /// <summary>
    /// Will be min-heap
    /// </summary>
    public MyHeap() { } // basic constructor

    /// <summary>
    /// if set to true the heap will be a max-heap
    /// </summary>
    public MyHeap(bool maxHeapMode) => MaxHeap = maxHeapMode;

    #region Operators

    /// <summary>
    /// Removes all elements of the Heap
    /// </summary>
    public void Clear() => _data.Clear();

    /// <summary>
    /// Adds an new value to the Heap
    /// </summary>
    public void Insert(T value)
    {
        _data.Add(value);
        HeapifyUp(Count - 1);
    }

    /// <summary>
    /// removes and returns the first value
    /// </summary>
    public T Remove()
    {
        var value = _data[0];
        _data[0] = _data[Count - 1];
        _data.RemoveAt(Count - 1);
        HeapifyDown(0);
        return value;
    }

    #endregion






    #region Getters

    /// <summary>
    /// return the first element in the heap
    /// </summary>
    /// <exception cref="InvalidOperationException"> if the heap is empty </exception>
    public T Peek()
    {
        if (IsEmpty) throw new InvalidOperationException("can't peek at an empty Heap");

        return _data[0];
    }

    /// <summary>
    /// returns true if Peek was successful
    /// </summary>
    /// <param name="value"> where the value goes out from </param>
    public bool TryPeek(out T? value)
    {
        if (IsEmpty)
        {
            value = default;
            return false;
        }

        value = _data[0];
        return true;
    }

    #endregion






    #region Helpers

    /// <summary>
    /// Moves up the value at childIndex to its correct position
    /// </summary>
    private void HeapifyUp(int childIndex)
    {
        if (childIndex == 0) return;

        var parentIndex = GetParentIndex(childIndex);

        var parentValue = _data[parentIndex];
        var childValue = _data[childIndex];

        if (Compare(parentValue, childValue))
        {
            Swap(parentIndex, childIndex);
            HeapifyUp(parentIndex);
        }
    }

    /// <summary>
    /// Moves down the value at parentIndex to its correct position
    /// </summary>
    private void HeapifyDown(int parentIndex)
    {
        var leftIndex = GetLeft(parentIndex);
        var rightIndex = GetRight(parentIndex);
        var targetIndex = parentIndex;

        if (IndexExist(leftIndex) && Compare(_data[targetIndex], _data[leftIndex]))
            targetIndex = leftIndex;

        if (IndexExist(rightIndex) && Compare(_data[targetIndex], _data[rightIndex]))
            targetIndex = rightIndex;

        if (targetIndex != parentIndex)
        {
            Swap(parentIndex, targetIndex);
            HeapifyDown(targetIndex);
        }
    }

    /// <summary>
    /// true if bigger greater to smaller <br/>
    /// false if smaller greater or equal to bigger <br/>
    /// how should be used -> bigger:parent | smaller:child <br/>
    /// same as (bigger > smaller) 
    /// </summary>
    private bool Compare(T bigger, T smaller)
    {
        if (MaxHeap)
            return _comparer.Compare(bigger, smaller) < 0; 
        else
            return _comparer.Compare(bigger, smaller) > 0; 
    }

    /// <summary> Swaps two values by their index </summary>
    private void Swap(int indexA, int indexB) => (_data[indexA], _data[indexB]) = (_data[indexB], _data[indexA]);

    /// <summary> Gets the index for where the right child should be </summary>
    private int GetRight(int parentIndex) => (2 * parentIndex) + 2;

    /// <summary> Gets the index for where the left child should be </summary>
    private int GetLeft(int parentIndex) => (2 * parentIndex) + 1;

    /// <summary> Gets the index for the child's parent </summary>
    private int GetParentIndex(int childIndex) => (childIndex - 1) / 2;

    /// <summary> Verifies if the index exist </summary>
    private bool IndexExist(int index) => index >= 0 && index < Count;

    #endregion






    #region Utils

    /// <summary>
    /// Gives enumerator functions to the heap <br/>
    /// be mindful in its use 
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        foreach (T item in _data) yield return item;
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    
    /// <summary>
    /// Gives the ability to build the Heap with an Enumerable
    /// </summary>
    public void Add(T value) => Insert(value);

    /// <summary>
    /// return a representation in string form of the heap
    /// </summary>
    public override string ToString()
    {
        var str = new StringBuilder();
        str.Append('[');
        bool first = true;
        foreach (T item in this)
        {
            if (!first) str.Append(", ");
            str.Append(item is null ? "null" : item.ToString());
            first = false;
        }
        str.Append(']');
        return str.ToString();
    }

    #endregion

}