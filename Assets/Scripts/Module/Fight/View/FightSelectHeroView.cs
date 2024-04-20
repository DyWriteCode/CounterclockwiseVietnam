using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 选择英雄面板
/// </summary>
public class FightSelectHeroView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bottom/startBtn").onClick.AddListener(onFightBtn);
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    // 选择好英雄后进入玩家回合
    private void onFightBtn()
    {
        // 如果一个英雄都没选需要提示玩家选择英雄
        if (GameApp.FightManager.heros.Count == 0)
        {
            GameApp.ViewManager.Open(ViewType.TipView, "玩家请先选择至少一个英雄");
        }
        else
        {
            GameApp.ViewManager.Close(ViewId);
            // 英雄已准备完成
            GameApp.FightManager.IsHerosReady = true;
            // 切换到玩家回合
            GameApp.FightManager.ChangeState(GameState.Player);
        }
    }

    public override void Open(params object[] args)
    {
        base.Open(args);
        GameObject prefab0bj = Find("bottom/grid/item");
        Transform gridTf = Find("bottom/grid").transform;
        for (int i = 0; i < GameApp.GameDataManager.heros.Count; i++)
        {
            Dictionary<string, string> data = GameApp.ConfigManager.GetConfigData("player").GetDataById(GameApp.GameDataManager.heros[i]);
            GameObject obj = Object.Instantiate(prefab0bj, gridTf);
            obj.SetActive(true);
            HeroItem item = obj.AddComponent<HeroItem>();
            item.Init(data);
        }
    }
}
