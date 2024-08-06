using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common.Algorithm;

/// <summary>
/// 敌人移动指令
/// </summary>
public class AiMoveCommand : BaseCommand
{
    private Enemy enemy;
    private _BFS bfs;
    private List<_BFS.Point> paths;
    private _BFS.Point current;
    private int pathIndex;
    private ModelBase target; // 移动到的目标

    public AiMoveCommand(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy;
        bfs = new _BFS(GameApp.MapManager.RowCount,GameApp.MapManager.ColCount);
        paths = new List<_BFS.Point>();
    }

    public override void Do()
    {
        base.Do();
        target = GameApp.FightManager.GetMinDisHero(enemy); // 获取最近的英雄
        if(target == null)
        {
            // 没有目标了
            isFinish = true;
        }
        else
        {
            paths = bfs.FindMinPath(this.enemy, this.enemy.Step, target.RowIndex, target.ColIndex);

            if(paths == null)
            {
                // 没路,这里可以随机一个点做移动
                isFinish = true;
            }
            else
            {
                // 将当前敌人的位置设置成不可移动
                GameApp.MapManager.ChangeBlockType(this.enemy.RowIndex,this.enemy.ColIndex,BlockType.Null);
            }
        }
    }

    public override bool Update(float dt)
    {
        if(paths.Count == 0)
        {
            return base.Update(dt);
        }
        else
        {
            current = paths[pathIndex];
            if (model.Move(current.RowIndex, current.ColIndex, dt * 5) == true)
            {
                // 到达目的地
                pathIndex++;
                if (pathIndex > paths.Count - 1)
                {
                    this.model.PlayAni("idle");
                    GameApp.MapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Obstacle);
                    return true;
                }
            }
        }
        model.PlayAni("move");
        return false;
    }
}
