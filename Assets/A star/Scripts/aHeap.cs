using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class aHeap
{
    #region Variabiles
    private aNode[] nodes;
    private int currentItemcount;
    #endregion

    #region Constructor
    public aHeap(int maxHeapSize)
    {
        nodes = new aNode[maxHeapSize];
    }
    #endregion

    #region Heap Methods
    public void Add(aNode node)
    {
        node.HeapIndex = currentItemcount;
        nodes[currentItemcount] = node;
        SortUp(node);
        currentItemcount++;
    }
    public aNode RemoveFirst()
    {
        aNode firstNode = nodes[0];
        currentItemcount--;

        nodes[0] = nodes[currentItemcount];
        nodes[0].HeapIndex = 0;
        SortDown(nodes[0]);
        return firstNode;
    }
    public bool Contains(aNode node)
    {
        return Equals(nodes[node.HeapIndex], node);
    }
    public void UpdateItem(aNode node)
    {
        SortUp(node);
    }
    public int Count { get { return currentItemcount; } }
    #endregion

    #region Sorting
    private void SortUp(aNode node)
    {
        int parentIndex = (node.HeapIndex - 1) / 2;

        while(true)
        {
            aNode parentNode = nodes[parentIndex];
            if (node.CompareTo(parentNode) > 0)
                Swap(node, parentNode);
            else
                break;
        }
    }
    private void SortDown(aNode node)
    {
        while(true)
        {
            int childIndexLeft = node.HeapIndex * 2 + 1;
            int childIndexRight = node.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemcount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemcount && nodes[childIndexLeft].CompareTo(nodes[childIndexRight]) < 0) // if he has a child on the right and the child on the right has a higher priority
                        swapIndex = childIndexRight;

                if (node.CompareTo(nodes[swapIndex]) < 0)
                    Swap(node, nodes[swapIndex]);
                else // right place = exit
                    break;
            }

            else // no more childs = exit
                break;
        }
    }
    private void Swap(aNode a, aNode b)
    {
        nodes[a.HeapIndex] = b;
        nodes[b.HeapIndex] = a;

        int indexSwap = a.HeapIndex;
        a.HeapIndex = b.HeapIndex;
        b.HeapIndex = indexSwap;
    }
    #endregion
}
