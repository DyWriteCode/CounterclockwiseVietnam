using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 显示移动路径的指令
/// </summary>
public class ShowPathCommand : BaseCommand
{
    Collider2D pre; // 鼠标之前检测到的2D碰撞体
    Collider2D current; // 鼠标当前检测到的2D碰撞体
    AStar astar; // A星寻路算法对象
    AStarPoint start; // 开始点
    AStarPoint end; // 结束点
    List<AStarPoint> prePaths; // 之前检测到的路径集合 用来清空用

    public ShowPathCommand(ModelBase model) : base(model)
    {
        prePaths = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex, model.ColIndex);
        astar = new AStar(GameApp.MapManager.RowCount, GameApp.MapManager.ColCount);
    }

    public override bool Update(float dt)
    {
        // 点击鼠标后 确定启动的位置
        if (Input.GetMouseButtonDown(0))
        {
            GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent); // 执行未选中
            return true;
        }
        current = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition); // 检测当前鼠标位置是否含有2D碰撞器
        if (current != null)
        {
            // 之前的检测碰撞和与当前的碰撞盒不一致 才进行路径检测
            if (current != pre)
            {
                pre = current;
                Block b = current.GetComponent<Block>();
                if (b != null)
                {
                    // 检测到block脚本进行寻路
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    astar.FindPath(start, end, updatePath);
                }
                else
                {
                    // 没有检测到对之前路径进行清除
                    for (int i = 0; i < prePaths.Count; i++)
                    {
                        GameApp.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
                    }
                    prePaths.Clear();
                }
            }
        }
        return false;
    }

    private void updatePath(List<AStarPoint> paths)
    {
        // 如果之前有路径需要先清除
        if (prePaths.Count != 0)
        {
            for (int i = 0; i < prePaths.Count; i++)
            {
                GameApp.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
            }
        }
        if (paths.Count >= 2 && model.Step >= paths.Count - 1)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                BlockDirection dir = BlockDirection.down;
                if (i == 0)
                {
                    dir = GameApp.MapManager.GetDirection1(paths[i], paths[i + 1]);
                }
                else if (i == paths.Count - 1)
                {
                    dir = GameApp.MapManager.GetDirection2(paths[i], paths[i - 1]);
                }
                else
                {
                    dir = GameApp.MapManager.GetDirection3(paths[i - 1], paths[i], paths[i + 1]);
                }
                GameApp.MapManager.SetBlockDir(paths[i].RowIndex, paths[i].ColIndex, dir, Color.yellow);
            }
            prePaths = paths;
        }
    }
}
