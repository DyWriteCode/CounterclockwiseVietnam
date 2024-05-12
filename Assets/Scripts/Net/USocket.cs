using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;


/// <summary>
/// USocket 提供Socket发送的接口 以及 Socket接收的业务
/// </summary>
public class USocket
    {
        UdpClient udpClient;

        string ip = "127.0.0.1";//服务器主机
        int port = 8899;//服务器程序

        public static UClient local;//客户端代理:完成发送的逻辑 和处理逻辑 保证报文的顺序

        public static IPEndPoint server;

        public USocket(Action<BufferEntity> dispatchNetEvent)
        {
            udpClient = new UdpClient(0);
            server = new IPEndPoint(IPAddress.Parse(ip), port);
            local = new UClient(this,server,0,0,0, dispatchNetEvent);
            ReceiveTask();//接收消息
        }
        ConcurrentQueue<UdpReceiveResult> awaitHandle = new ConcurrentQueue<UdpReceiveResult>();
       /// <summary>
       /// 接收报文
       /// </summary>
        public async void ReceiveTask()
        {
            while (udpClient != null)
            {
                try
                {
                    UdpReceiveResult result = await udpClient.ReceiveAsync();
                    awaitHandle.Enqueue(result);
                    Debug.Log("接收到了消息!");
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }

        /// <summary>
        /// 发送报文的接口
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        public async void Send(byte[] data,IPEndPoint endPoint) {

            if (udpClient!=null)
            {
                try
                {
                   int length=await udpClient.SendAsync(data,data.Length,ip,port);
                }
                catch (Exception e)
                {
                    Debug.LogError($"发送异常:{e.Message}");
                }
            }
        }

        //发送ACK报文 解包后 马上调用
        public void SendACK(BufferEntity bufferEntity) {
             Send(bufferEntity.buffer, server);
        }

        //Update去进行调用
        public void Handle() {

            if (awaitHandle.Count>0)
            {
                UdpReceiveResult data;
                if (awaitHandle.TryDequeue(out data))
                {
                    //反序列化
                    BufferEntity bufferEntity = new BufferEntity(data.RemoteEndPoint,data.Buffer);
                    if (bufferEntity.isFull)
                    {
                        Debug.Log($"处理消息,id:{bufferEntity.messageID},序号:{bufferEntity.sn}");
                        //处理业务逻辑
                        local.Handle(bufferEntity);
                    }
                }
            }
        }

        /// <summary>
        /// 关闭udpClient
        /// </summary>
        public void Close() {
            if (local != null)
            {
                local = null;
            }

            if (udpClient!=null)
            {
                udpClient.Close();
                udpClient = null;
            }
           
        }
    }
