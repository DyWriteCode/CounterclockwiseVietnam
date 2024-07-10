using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 双向链表类
/// </summary>
public class DoubleLinkedList<T> where T : class, new()
{
    // 前一个节点
    public DoubleLinkedListNode<T> Head = null;
    // 最后后一个节点
    public DoubleLinkedListNode<T> Tail = null;
    // 双向链表结构对象池
    public ClassObjectPool<DoubleLinkedListNode<T>> m_DoubleLinkedNodePool = GameApp.ObjectManager.GetOrCreateClassPool<DoubleLinkedListNode<T>>(500);
    // 节点的总个数
    protected int m_Count = 0;
    public int Count
    {
        get
        {
            return m_Count;
        }
    }

    // 添加一个节点到头部
    public DoubleLinkedListNode<T> AddToHeader(T t)
    {
        DoubleLinkedListNode<T> pList = m_DoubleLinkedNodePool.Spawn(true);
        pList.prev = null;
        pList.next = null;
        pList.t = t;
        return AddToHeader(pList);
    }

    // 添加一个节点到头部
    public DoubleLinkedListNode<T> AddToHeader(DoubleLinkedListNode<T> pNode)
    {
        if (pNode == null)
        {
            return null;
        }
        pNode.prev = null;
        if (Head == null)
        {
            Head = pNode;
            Tail = pNode;
        }
        else
        {
            pNode.next = Head;
            Head.prev = pNode;
            Head = pNode;
        }
        m_Count++;
        return Head;
    }

    // 添加一个节点到尾部
    public DoubleLinkedListNode<T> AddToTail(T t)
    {
        DoubleLinkedListNode<T> pList = m_DoubleLinkedNodePool.Spawn(true);
        pList.prev = null;
        pList.next = null;
        pList.t = t;
        return AddToTail(pList);
    }

    // 添加一个节点到尾部
    public DoubleLinkedListNode<T> AddToTail(DoubleLinkedListNode<T> pNode)
    {
        if (pNode == null)
        {
            return null;
        }
        pNode.next = null;
        if (Head == null)
        {
            Head = pNode;
            Tail = pNode;
        }
        else
        {
            pNode.prev = Tail;
            Tail.next = pNode;
            Tail = pNode;
        }
        m_Count++;
        return Tail;
    }

    // 移除掉某一个节点
    public void RemoveNode(DoubleLinkedListNode<T> pNode)
    {
        if (pNode == null)
        {
            return;
        }
        if (pNode == Head)
        {
            Head = pNode.next;
        }
        if (pNode == Tail)
        {
            Tail = pNode.prev;
        }
        if (pNode.next != null)
        {
            pNode.prev.next = pNode.next;
        }
        if (pNode.prev != null)
        {
            pNode.next.prev = pNode.prev;
        }
        pNode.prev = pNode.next = null;
        pNode.t = null;
        m_DoubleLinkedNodePool.Recycle(pNode);
        m_Count--;
    }

    // 把某一个节点移至头部
    public void MoveToHead(DoubleLinkedListNode<T> pNode)
    {
        if (pNode == null || pNode == Head)
        {
            return;
        }
        if (pNode.prev == null && pNode.next == Head)
        {
            return;
        }
        if (pNode == Tail)
        {
            Tail = pNode.prev;
        }
        if (pNode.prev != Tail)
        {
            pNode.prev.next = pNode.next;
        }
        if (pNode.next != Tail)
        {
            pNode.next.prev = pNode.prev;
        }
        pNode.prev = Tail;
        pNode.next = Head;
        pNode.prev = pNode;
        Head = pNode;
        if (Tail == null)
        {
            Tail = Head;
        }
    }
}

/// <summary>
/// 双向链表节点类
/// </summary>
public class DoubleLinkedListNode<T> where T : class, new()
{
    // 前一个节点
    public DoubleLinkedListNode<T> prev = null;
    // 后一个节点
    public DoubleLinkedListNode<T> next = null;
    // 当前节点
    public T t = null;
}

/// <summary>
/// 基于AssetBundle的资源管理
/// </summary>
public class ResourceManager
{
    
}
