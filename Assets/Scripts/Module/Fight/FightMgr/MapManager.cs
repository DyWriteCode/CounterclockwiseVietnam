using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 格子显示方向枚举 枚举字符串与图片资源路径一致
/// </summary>
public enum BlockDirection
{
    none = -1,
    down,
    horizontal,
    left,
    left_down,
    left_up,
    right,
    right_down,
    right_up,
    up,
    vertical,
    max,
}

/// <summary>
/// 地图管理器 存储地图网格的信息
/// </summary>
public class MapManager
{
    private Tilemap tileMap;
    public Block[,] mapArr;
    public int RowCount; // 地图行数
    public int ColCount; // 地图列数
    public List<Sprite> dirSpArr; // 存储箭头方向图片的集合

    // 初始化地图信息
    public void Init()
    {
        dirSpArr = new List<Sprite>();
        for (int i = 0; i < (int)BlockDirection.max; i++)
        {
            dirSpArr.Add(Resources.Load<Sprite>($"Icon/{(BlockDirection)i}"));
        }
        tileMap = GameObject.Find("Grid/ground").GetComponent<Tilemap>();
        // 地图大小 可以把此数据写入配置表 从配置表读取
        RowCount = 12;
        ColCount = 20;
        // 读取配置表方式获取
        // ConfigData tempData = GameApp.ConfigManager.GetConfigData("xxxx");
        // RowCount = int.Parse(tempData.GetDataById(10001)["Row"]);
        // ColCount = int.Parse(tempData.GetDataById(10001)["Col"]);
        mapArr = new Block[RowCount, ColCount];
        List<Vector3Int> tempPosArr = new List<Vector3Int>(); // 临时存储瓦片地图每一个格子的位置
        // 遍历瓦片地图
        foreach (var pos in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                tempPosArr.Add(pos);
            }
        }
        // 把一维数组的位置转换为二维数组的Block 进行存储
        Object prefabObj = Resources.Load("Model/block");
        for (int i = 0; i < tempPosArr.Count; i++)
        {
            int row = i / ColCount;
            int col = i % ColCount;
            Block b = (Object.Instantiate(prefabObj) as GameObject).AddComponent<Block>();
            b.RowIndex = row;
            b.ColIndex = col;
            b.transform.position = tileMap.CellToWorld(tempPosArr[i]) + new Vector3(0.5f, 0.5f, 0);
            mapArr[row, col] = b;
        }
    }

    // 获得格子位置
    public Vector3 GetBlockPos(int row, int col)
    {
        return mapArr[row, col].transform.position;
    }

    public BlockType GetBlockType(int row, int col)
    {
        return mapArr[row, col].Type;
    }

    public void ChangeBlockType(int row, int col, BlockType type)
    {
        mapArr[row, col].Type = type;
    }

    // 显示移动的区域
    public void ShowStepGird(ModelBase model, int step)
    {
        _BFS bfs = new _BFS(RowCount, ColCount);

        List<_BFS. Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].ShowGird(Color.blue);
        } 
    }

    // 隐藏移动的区域
    public void HideStepGird(ModelBase model, int step)
    {
        _BFS bfs = new _BFS(RowCount, ColCount);

        List<_BFS.Point> points = bfs.Search(model.RowIndex, model.ColIndex, step);

        for (int i = 0; i < points.Count; i++)
        {
            mapArr[points[i].RowIndex, points[i].ColIndex].HideGird();
        }
    }

    // 根据方向枚举 设置格子的方向图标和颜色
    public void SetBlockDir(int rowIndex, int colIndex, BlockDirection dir, Color color)
    {
        mapArr[rowIndex, colIndex].SetDirSp(dirSpArr[(int)dir], color);
    }

    // 得到开始点和下一个点算出方向
    public BlockDirection GetDirection1(AStarPoint start, AStarPoint next)
    {
        int row_offset = next.RowIndex - start.RowIndex;
        int col_offset = next.ColIndex - start.ColIndex;
        if (row_offset == 0)
        {
            return BlockDirection.horizontal;
        }
        else if (col_offset == 0)
        {
            return BlockDirection.vertical;
        }
        else
        {
            return BlockDirection.none;
        }
    }

    // 终点 与 前一个点 算出方向
    public BlockDirection GetDirection2(AStarPoint end, AStarPoint pre)
    {
        int row_offset = end.RowIndex - pre.RowIndex;
        int col_offset = end.ColIndex - pre.ColIndex;
        if (row_offset == 0 && col_offset > 0)
        {
            return BlockDirection.right;
        }
        else if (row_offset == 0 && col_offset < 0)
        {
            return BlockDirection.left;
        }
        else if (col_offset == 0 && row_offset > 0)
        {
            return BlockDirection.up;
        }
        else if (col_offset == 0 && row_offset < 0)
        {
            return BlockDirection.down;
        }
        else
        {
            return BlockDirection.none;
        }
    }

    // 三个点 算出方向
    public BlockDirection GetDirection3(AStarPoint pre, AStarPoint current, AStarPoint end)
    {
        BlockDirection dir = BlockDirection.none;
        int row_offset_1 = pre.RowIndex - current.RowIndex;
        int col_offset_1 = pre.ColIndex - current.ColIndex;
        int row_offset_2 = end.RowIndex - current.RowIndex;
        int col_offset_2 = end.ColIndex - current.ColIndex;
        int sum_row_offset = row_offset_1 + row_offset_2;
        int sum_col_offset = col_offset_1 + col_offset_2;
        if (sum_row_offset == 1 && sum_col_offset == -1)
        {
            dir = BlockDirection.left_up;
        }
        else if (sum_row_offset == 1 && sum_col_offset == 1)
        {
            dir = BlockDirection.right_up;
        }
        else if (sum_row_offset == -1 && sum_col_offset == -1)
        {
            dir = BlockDirection.left_down;
        }
        else if (sum_row_offset == -1 && sum_col_offset == 1)
        {
            dir = BlockDirection.right_down;
        }
        else
        {
            if (row_offset_1 == 0)
            {
                dir = BlockDirection.horizontal;
            }
            else
            {
                dir = BlockDirection.vertical;
            }
        }
        return dir;
    }

    // 显示攻击
    public void ShowAttackStep(ModelBase model, int attackStep, Color color)
    {
        // Debug.Log(attackStep);
        int minRow = model.RowIndex - attackStep >= 0 ? model.RowIndex - attackStep : 0;
        int minCol = model.ColIndex - attackStep >= 0 ? model.ColIndex - attackStep : 0;
        int maxRow = model.RowIndex + attackStep > RowCount - 1 ? RowCount - 1 : model.RowIndex + attackStep;
        int maxCol = model.ColIndex + attackStep > ColCount - 1 ? ColCount - 1 : model.ColIndex + attackStep;

        for (int row = minRow; row <= maxRow; row++)
        {
            for (int col = minCol; col <= maxCol; col++)
            {
                if (Mathf.Abs(model.RowIndex - row) + Mathf.Abs(model.ColIndex - col) <= attackStep)
                {
                    mapArr[row, col].ShowGrid(color);
                }
            }
        }

    }

    // 隐藏攻击
    public void HideAttackStep(ModelBase model, int attackStep)
    {
        int minRow = model.RowIndex - attackStep >= 0 ? model.RowIndex - attackStep : 0;
        int minCol = model.ColIndex - attackStep >= 0 ? model.ColIndex - attackStep : 0;
        int maxRow = model.RowIndex + attackStep > RowCount - 1 ? RowCount - 1 : model.RowIndex + attackStep;
        int maxCol = model.ColIndex + attackStep > ColCount - 1 ? ColCount - 1 : model.ColIndex + attackStep;

        for (int row = minRow; row <= maxRow; row++)
        {
            for (int col = minCol; col <= maxCol; col++)
            {
                if (Mathf.Abs(model.RowIndex - row) + Mathf.Abs(model.ColIndex - col) <= attackStep)
                {
                    mapArr[row, col].HideGrid();
                }
            }
        }
    }

    // 清空
    public void Clear()
    {
        mapArr = null;
        dirSpArr.Clear();
    }
}
