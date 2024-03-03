using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 命令管理器
/// </summary>
public class CommandManager
{
    private Queue<BaseCommand> willDoCommandQueue; // 将要执行的命令队列
    private Stack<BaseCommand> unDoStack; // 撤销的命令堆栈
    private BaseCommand current; // 当前执行的命令

    // 初始化
    public CommandManager() 
    { 
        willDoCommandQueue = new Queue<BaseCommand>();
        unDoStack = new Stack<BaseCommand>();
    }

    // 是否在执行命令
    public bool IsRunningCommand
    {
        get
        {
            return current != null;
        }
    }

    // 添加命令
    public void AddCommand(BaseCommand cmd)
    {
        willDoCommandQueue.Enqueue(cmd); // 把命令添加到执行队列
        unDoStack.Push(cmd); // 把命令添加到撤销堆栈
    }

    // 每一帧执行
    public void Update(float dt)
    {
        if (current == null)
        {
            if (willDoCommandQueue.Count > 0)
            {
                current = willDoCommandQueue.Dequeue();
                current.Do(); // 执行命令
            }
        }
        else
        {
            if (current.Update(dt) == true) 
            {
                current = null;
            }
        }
    }

    // 清除掉所有命令
    public void Clear()
    {
        willDoCommandQueue.Clear();
        unDoStack.Clear();
        current = null;
    }

    // 撤销上一个命令
    public void UnDo()
    {
        if (unDoStack.Count > 0)
        {
            unDoStack.Pop().UnDo();
        }
    }
}
