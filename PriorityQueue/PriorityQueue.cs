using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// A container class to be used in our priority queue... it's really just a pair class.
/// </summary>
public class PriorityNode<T>
{
    public T Data;
    public int Priority;

    public PriorityNode(T data, int priority)
    {
        Data = data;
        Priority = priority;
    }
}

public class PriorityQueue<T> 
{
    //we'll make a tree where the minimum priority is at the top

    //rather than making our own array and managing its size, we'll use an existing implementation
    //the first index will be empty... we'll take advantage of this for the Swap function
    private List<PriorityNode<T>> Container;
    public bool isEmpty
    {
        get { return Container.Count <= 1; }
    }

    public int Count
    {
        get { return Container.Count - 1; }
    }

    public PriorityQueue()
    {
        Container = new List<PriorityNode<T>>();
        Container.Add(new PriorityNode<T>(default(T), -1));//empty space to accommodate calculations
    }

    /// <summary>
    /// A helper function designed to get the parent of the current index
    /// </summary>
    /// <param name="currentIndex">the current index in our heap, we're looking for its parent</param>
    /// <returns>the index of the parent of currentIndex</returns>
    int GetParentIndex(int currentIndex)
    {
        return currentIndex >> 1;   //div by 2
    }

    /// <summary>
    /// A helper function designed to get the left child of the current index
    /// </summary>
    /// <param name="currentIndex">the parent index</param>
    /// <returns>the index of the left child of currentIndex</returns>
    int GetLeftIndex(int currentIndex)
    {
        return currentIndex << 1;    //mult by 2
    }

    /// <summary>
    /// A helper function designed to get the right child of the current index
    /// </summary>
    /// <param name="currentIndex">the parent index</param>
    /// <returns>the index of the right child of currentIndex</returns>
    int GetRightIndex(int currentIndex)
    {
        return (currentIndex << 1) + 1;
    }

    /// <summary>
    /// swaps the items at the given indices around... this function takes advantage of the extra space at the beginning of the heap to avoid creating a temp variable
    /// </summary>
    /// <param name="i">index of an item to be swapped</param>
    /// <param name="n">index of an item to be swapped</param>
    void Swap(int i, int n)
    {
        Container[0] = Container[i];
        Container[i] = Container[n];
        Container[n] = Container[0];
    }

    /// <summary>
    /// takes the node at given index and moves it upwards if it has a lower priority value
    /// </summary>
    /// <param name="index">the index we plan on shifting</param>
    void ShiftUp(int index)
    {
        if (index >= Container.Count)
            return;

        //determine if parent is greater
        int parentIndex = GetParentIndex(index);

        //index > 1 means the item is not the root (and therefore has a parent)
        while (index > 1 && Container[index].Priority < Container[parentIndex].Priority)
        {
            Swap(index, parentIndex);
            index = parentIndex;
            parentIndex = GetParentIndex(index);
        }
    }

    /// <summary>
    /// takes the node at given index and moves it downwards if it has a higher priority value
    /// </summary>
    /// <param name="index">the index we plan on shifting</param>
    void ShiftDown(int index)
    {
        int left = GetLeftIndex(index);
        int right = 0;
        int smaller = 0;

        while (left < Container.Count)
        {
            right = left + 1;
            smaller = left;

            if (right < Container.Count)
                smaller = (Container[left].Priority < Container[right].Priority) ? left : right;

            if (Container[index].Priority < Container[smaller].Priority)
                break;

            Swap(index, smaller);
            index = smaller;
            left = GetLeftIndex(index);
        }
    }

    /// <summary>
    /// adds the given object to our heap based on its priority
    /// </summary>
    /// <param name="obj">the item to store</param>
    /// <param name="priority">the priority value of the given item</param>
    public void Enqueue(T obj, int priority)
    {
        Container.Add(new PriorityNode<T>(obj, priority));
        ShiftUp(Container.Count - 1);
    }

    /// <summary>
    /// Dequeues the minimum item on the heap (in this case the first item). 
    /// After removal we have to adjust the heap
    /// </summary>
    /// <returns>the item with the lowest priority value</returns>
    public T DequeueMin()
    {
        //store the lowest priority item (index 1)
        //put the last item in the first index
        //remove the last index
        //shift the first item down the list according to priority

        T output = Container[1].Data;
        Container[1] = Container[Container.Count - 1];
        Container.RemoveAt(Container.Count - 1);

        ShiftDown(1);
        return output;
    }

    /// <summary>
    /// Returns the highest priority value item by comparing the latter half of the heap
    /// </summary>
    /// <returns>the item with the highest priority value</returns>
    public T DequeueMax() //slight alteration on the return type from what the question asks to be implemented
    {
        //find the index of the item with the highest priority value
        //replace it with the last item
        //remove the last item from the system
        //shift the item down from the current index

        int largest = GetMaxIndex();
        T output = Container[largest].Data;
        Container[largest] = Container[Container.Count - 1];
        Container.RemoveAt(Container.Count - 1);

        ShiftDown(largest);
        return output;
    }

    /// <summary>
    /// Searches the last half of the heap (the leaves) for the highest value item and returns its index
    /// </summary>
    /// <returns>the index of the highest priority item</returns>
    private int GetMaxIndex()
    {
        if (Container.Count <= 1)
            throw new ArgumentException();

        //the idea is to searh the last half of the items as they will be end points (leaves)
        //since we built a min-heap the largest item has to be a leaf
        int start = Container.Count >> 1;
        int largest = start;
        for(int i = Container.Count-1; i > start; i--)
            largest = Container[i].Priority > Container[largest].Priority ? i : largest;

        return largest;
    }

    /// <summary>
    /// Returns the maximum priority item (this is not cached)
    /// </summary>
    /// <returns>the item with the highest priorty value</returns>
    public T PeakMax() 
    {
        int larger = GetMaxIndex();
        return Container[larger].Data;
    }

    /// <summary>
    /// returns the item with the lowest priority value (the item at index 1 in our heap)
    /// </summary>
    /// <returns>the item with the lowest priority value</returns>
    public T PeakMin()
    {
        return Container[1].Data;
    }

    //not a proper ToString implementation, this setup is just for testing
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i < Container.Count; i++)
        {
            sb.Append(Container[i].Priority);
            sb.Append(", ");
        }
        return sb.ToString();
    }
}
