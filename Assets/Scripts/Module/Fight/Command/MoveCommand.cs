using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移动指令
/// </summary>
public class MoveCommand : BaseCommand
{
    private List<AStarPoint> paths;
    private AStarPoint current;
    private int pathIndex;
    // 移动前导航列坐标 用于撤销
    private int preRowIndex;
    private int preColIndex;

    public MoveCommand(ModelBase model) : base(model)
    {

    }

    public MoveCommand(ModelBase model, List<AStarPoint> paths) : base(model)
    {
        this.paths = paths;
        pathIndex = 0;
    }

    public override void Do()
    {
        base.Do();
        this.preColIndex = this.model.ColIndex;
        this.preRowIndex = this.model.RowIndex;
        // 设置当前格子所占为null
        GameApp.MapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Null);
    }

    // 撤销动作
    public override void UnDo()
    {
        base.UnDo();
        // 回到之前的位置
        Vector3 pos = GameApp.MapManager.GetBlockPos(preColIndex, preRowIndex);
        pos.z = this.model.transform.position.z;
        this.model.transform.position = pos;
        GameApp.MapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Null);
        this.preColIndex = this.model.ColIndex;
        this.preRowIndex = this.model.RowIndex;
        GameApp.MapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Obstacle);
    }

    public override bool Update(float dt)
    {
        current = this.paths[pathIndex];
        if (this.model.Move(current.RowIndex, current.ColIndex, dt * 5))
        {
            pathIndex++;
            if (pathIndex > this.paths.Count - 1)
            {
                // 到达目的地
                this.model.PlayAni("idle");
                GameApp.MapManager.ChangeBlockType(this.model.RowIndex, this.model.ColIndex, BlockType.Obstacle);
                return true;
            }
        }
        this.model.PlayAni("move");
        return false;
    }
}
