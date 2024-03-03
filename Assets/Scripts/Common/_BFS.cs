using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 广度优先搜索算法
/// </summary>
public class _BFS
{
    // 搜索点
    public class Point
    {
        public int RowIndex; // 行坐标
        public int ColIndex; // 列坐标
        public Point Father; // 父节点 用来查找路径

        // 父节点初始化使用
        public Point(int row, int col) 
        {  
            this.RowIndex = row; 
            this.ColIndex = col;
        }

        // 子节点初始化使用
        public Point(int row, int col, Point Father)
        {
            this.RowIndex = row;
            this.ColIndex = col;
            this.Father = Father;
        }
    }

    public int RowCount; // 行总数
    public int ColCount; // 列总数
    public Dictionary<string, Point> finds; // 存储查找到点的字典(key:点的行列拼接而成的字符串 value:搜索点)

    // 初始化
    public _BFS(int row, int col)
    {
        this.RowCount = row;
        this.ColCount = col;
        finds = new Dictionary<string, Point>();
    }

    /// <summary>
    /// 搜索行走区域
    /// </summary>
    /// <param name="col">开始点的行坐标</param>
    /// <param name="row">开始点的列坐标</param>
    /// <param name="step">步数</param>
    /// <returns></returns>
    public List<Point> Search(int row, int col, int step)
    {
        // 定义搜索集合
        List<Point> searchs = new List<Point>();
        // 开始点
        Point startPoint = new Point(row, col);
        // 把开始点存入搜索集合
        searchs.Add(startPoint);
        // 开始点默认已经找到存入已经找到的字典
        finds.Add($"{row}_{col}", startPoint);
        //遍历步数 相当于可搜索的次数
        for (int i = 0; i < step; i++)
        {
            //定义一个临时的集合 用于存储目前找到满足条件的点
            List<Point> temps = new List<Point>();
            //遍历搜索集合
            for (int j = 0; j < searchs.Count; j++)
            {
                Point current = searchs[j];
                //查找当前点四周圈的点
                FindAroundPoints(current, temps);
            }
            if (temps.Count == 0)
            {
                // 临时集合一个点都找不到 相当于是死路 没必要搜索了
                break;
            }
            // 搜索的集合需要清空
            searchs.Clear();
            // 将临时集合的点添加到搜索集合
            searchs.AddRange(temps);
        }
        // 把查找到的点转换为集合并返回
        return finds.Values.ToList();
    }

    // 寻找周围的点 上下左右(可以拓展寻找斜边的点)
    public void FindAroundPoints(Point current, List<Point> temps)
    {
        // 上边
        if (current.RowIndex - 1 >= 0)
        {
            Addfinds(current.RowIndex - 1, current.ColIndex, current, temps);
        }

        // 下边
        if (current.RowIndex + 1 < RowCount)
        {
            Addfinds(current.RowIndex + 1, current.ColIndex, current, temps);
        }

        // 左边
        if (current.ColIndex - 1 >= 0)
        {
            Addfinds(current.RowIndex, current.ColIndex - 1, current, temps);
        }

        // 右边
        if (current.ColIndex + 1 < ColCount)
        {
            Addfinds(current.RowIndex, current.ColIndex + 1, current, temps);
        }
    }

    // 添加到查找到的字典
    public void Addfinds(int row, int col, Point Father, List<Point> temps)
    {
        // 不在查找的节点 而 对应节点的类型不是障碍物才能加入查找字典
        if (finds.ContainsKey($"{row}_{col}") == false && GameApp.MapManager.GetBlockType(row, col) != BlockType.Obstacle)
        {
            Point p = new Point(row, col, Father);
            // 添加到查找到的字典
            finds.Add($"{row}_{col}", p);
            // 添加到临时集合用于下次查找
            temps.Add(p);
        }
    }
}
