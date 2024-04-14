using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗中的状态枚举
/// </summary>
public enum GameState
{
    Idle,
    Enter,
    Player,
}

/// <summary>
/// 战斗管理器(用于管理战斗相关的实体[敌人 英雄 地图 格子等等])
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle; // 默认状态
    private FightUnitBase current; // 当前所处战斗单元
    public FightUnitBase Current { get => current;}
    public List<Hero> heros; // 正在战斗中的英雄集合
    public List<Enemy> enemys; // 正在战斗中的敌人集合
    public int RoundCount; // 回合数目

    public FightWorldManager() 
    {
        heros = new List<Hero>();
        enemys = new List<Enemy>();
        ChangeState(GameState.Idle);
    }

    public void Update(float dt)
    {
        if (current != null && current.Update(dt) == true)
        {
            // to do ......
        }
        else
        {
            current = null;
        }
    }

    // 切换战斗状态
    public void ChangeState(GameState state)
    {
        FightUnitBase _current = current;
        this.state = state;
        switch (this.state) 
        {
            case GameState.Idle:
                _current = new FightIdle();
                break;
            case GameState.Enter:
                _current = new FightEnter();
                break;
            case GameState.Player:
                _current = new FightPlayerUnit();
                break;
        }
        _current.Init();
    }

    // 进入战斗 初始化一些信息 敌人信息 关卡信息 等
    public void EnterFight()
    {
        RoundCount = 1;
        heros = new List<Hero>();
        enemys = new List<Enemy>();
        // 把场景中的敌人脚本进行存储
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy"); // 给敌人添加了"Enemy"标签
        for(int i = 0; i < objs.Length; i++)
        {
            Enemy enemy = objs[i].GetComponent<Enemy>();
            // 当前方块被占用了把方块类型改为障碍物
            GameApp.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
            enemys.Add(enemy);
        }
        // test
        // Debug.Log("enemy:" + objs.Length);
    }

    // 创建(添加)英雄
    public void AddHero(Block b, Dictionary<string, string> data)
    {
        GameObject obj = UnityEngine.Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
        obj.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(data, b.RowIndex, b.ColIndex);
        // 这个位置有方块了 改变方块的类型为障碍物
        b.Type = BlockType.Obstacle;
        heros.Add(hero);
    }

    // 移除敌人
    public void RemoveEnemy(Enemy enemy)
    {
        enemys.Remove(enemy);
    }
}
