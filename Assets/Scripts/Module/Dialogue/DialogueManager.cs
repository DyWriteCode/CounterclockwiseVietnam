using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏对话管理器
/// </summary>
public class DialogueManager
{
    public DialogueManager() 
    {
        
    }

    // 获得对应类型的整个对话列表数据
    public List<DialogueInfo> GetDialogueInfos(ConfigData data, int startId, bool isNext = false)
    {
        int Id = startId;
        List <DialogueInfo> result = new List<DialogueInfo>();
        // Id,Name,Msg,ImgPath,ImgLeft,ImgRight,Type,Ani,NextId
        if (isNext == false)
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
        }
        else
        {
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
        }
        return result;
    }

    // 打字机效果
    public IEnumerator TypeWriterEffect(float charsPer, string words, Text text)
    {
        float t = 0; // 经过的时间
        int charIndex = 0; // 字符串索引值
        while (charIndex < words.Length)
        {
            t += Time.deltaTime * charsPer; // 简单计时器赋值给t
            charIndex = Mathf.FloorToInt(t); // 把t转为int类型赋值给charIndex
            charIndex = Mathf.Clamp(charIndex, 0, words.Length);
            text.text = words.Substring(0, charIndex);

            yield return null;
        }
        text.text = words;
    }
}
