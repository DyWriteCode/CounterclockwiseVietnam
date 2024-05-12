using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 报文工厂
/// </summary>
public class BufferFactory 
    {
        enum MessageType
        {
            ACK=0,//确认报文
            Login=1,//业务逻辑的报文
        }

        /// <summary>
        /// 创建并且发送报文
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static BufferEntity CreateAndSendPackage(int messageID,IMessage message) {

            GameApp.HelperManager.JsonHelper.Log(messageID, message);

            //Debug.Log($"报文ID:{messageID}\n包体{JsonHelper.SerializeObject(message)}");
            BufferEntity buffer = new BufferEntity(USocket.local.endPoint,USocket.local.sessionID,0,0, MessageType.Login.GetHashCode(),
                messageID,ProtobufHelper.ToBytes(message));
            USocket.local.Send(buffer);
            return buffer;
        }

    }

