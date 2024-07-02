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
