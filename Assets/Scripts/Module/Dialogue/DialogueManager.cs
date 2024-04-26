using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏对话管理器
/// </summary>
public class DialogueManager
{
    public DialogueManager() 
    {
        
    }

    // 获得对应类型的整个对话列表数据
    public List<DialogueInfo> GetDialogueInfos(ConfigData data, int startId)
    {
        int Id = startId;
        List <DialogueInfo> result = new List<DialogueInfo>();
        // Id,Name,Msg,ImgPath,ImgLeft,ImgRight,Type,Ani,NextId
        while (Id != 0)
        {
            result.Add(new DialogueInfo()
            {
                Name = data.GetDataById(Id)["Name"],
                MsgIxt = data.GetDataById(Id)["Msg"],
                ImgPath = data.GetDataById(Id)["Name"],
                ImgPathLeft = data.GetDataById(Id)["ImgLeft"],
                ImgPathRight = data.GetDataById(Id)["ImgRight"],
                Type = data.GetDataById(Id)["Type"],
            });
            Id = int.Parse(data.GetDataById(Id)["NextId"]);
        }
        return result;
    }

    // 获得某一对话的数据
}
