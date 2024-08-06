using System.Collections;
using System.Collections.Generic;
using Game.Common;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public int LevelId; // 设置关卡ID

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // test
        // Debug.Log("enter");
        GameApp.MessageManager.PostEvent(Defines.ShowLevelDesEvent, LevelId);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        // test
        // Debug.Log("exit");
        GameApp.MessageManager.PostEvent(Defines.HideLevelDesEvent);
    }
}
