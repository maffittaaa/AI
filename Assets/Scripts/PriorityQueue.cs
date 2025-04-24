using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    private List<(float priority, T item)> elements = new List<(float, T)>();

    public int Count => elements.Count;

    public void Enqueue(T item, float priority)
    {
        elements.Add((priority, item));
        elements.Sort((a, b) => a.priority.CompareTo(b.priority)); // Sort by priority
    }

    public T Dequeue()
    {
        if (elements.Count == 0) return default;
        T item = elements[0].item;
        elements.RemoveAt(0);
        return item;
    }

    public void EnqueueOrUpdate(T item, float priority)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(elements[i].item, item))
            {
                // If item already exists, update its priority if it's lower
                if (priority < elements[i].priority)
                {
                    elements[i] = (priority, item);
                    elements.Sort((a, b) => a.priority.CompareTo(b.priority)); // Re-sort the queue
                }
                return;
            }
        }
        // If item is not found, enqueue as a new element
        Enqueue(item, priority);
    }

    // Method to print the queue contents
    public void PrintQueue()
    {
        Debug.Log("Priority Queue Contents:");
        foreach (var element in elements)
        {
            Debug.Log($"Item: {element.item}, Priority: {element.priority}");
        }
    }
}
