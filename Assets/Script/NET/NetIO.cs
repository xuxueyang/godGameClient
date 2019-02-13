using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class NetIO {


    //单例模式，强链接
    private static NetIO instance;
    private Socket socket;
    private string ip = "127.0.0.1";
    private int port = 6666;
    private byte[] readbuff = new byte[1024];

    List<byte> cache = new List<byte>();
    public List<SocketModel> messages = new List<SocketModel>();

    private bool isReading = false;
   // private bool isWriting = false;

    public static NetIO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetIO();
            }
            return instance;
        }
    }
    private NetIO()
    {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //同步连接，堵塞
            socket.Connect(ip, port);
            //socket.Receive();//异步读取，但是性能是icp封装
            //开启异步消息接收，消息到达后会直接写入到缓冲区//消息过长会自动粘包。
            socket.BeginReceive(readbuff, 0, 1024, SocketFlags.None, ReceiveCallBack, readbuff);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }

    }
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            //获取当前收到的消息长度
            int length = socket.EndReceive(ar);
            byte[] message = new byte[length];
            Buffer.BlockCopy(readbuff, 0, message, 0, length);
            cache.AddRange(message);
            if (!isReading)
            {
                isReading = true;
                onData();
            }
            //开启异步消息接收，消息到达后会直接写入到缓冲区//消息过长会自动粘包。
            socket.BeginReceive(readbuff, 0, 1024, SocketFlags.None, ReceiveCallBack, readbuff);
        }
        catch(Exception e)
        {
            Debug.Log("远程服务器主动断开连接"+e.Message);
            //反之这时候唯一可能错误的只有远程服务器主动t了
            socket.Close();
        }

        
    }
    public void write(byte type,int area,int command,object message)
    {
        ByteArray ba = new ByteArray();
        ba.write(type);
        ba.write(area);
        ba.write(command);
        if (message != null)
        {
            ba.write(SerialUtil.encode(message));
        }
        ByteArray ba1 = new ByteArray();
        ba1.write(ba.Length);
        ba1.write(ba.getBuff());
        try
        {
            socket.Send(ba1.getBuff());
        }
        catch(Exception e)
        {
            Debug.Log("网络错误，请重新登陆" + e.Message);
        }
    }


    //缓存中有数据处理
    private void onData()
    {
        byte[] result = decode(ref cache);
        //返回为空，说明数据不全，等待下条消息过来补全
        if (result == null)
        {
            isReading = false;
            return;
        }
        SocketModel message = mdecode(result);
        if (message == null)
        {
            isReading = false;
            return;
        }
        //进行消息的处理
        //****************unity是无法处理之外的线程！！！！
        //->unity因为采取帧的概念，所以....
        messages.Add(message);
        //伪递归。防止消息处理过程中，有其他消息到达而没有经过处理
        onData();
    }
    private static byte[] decode(ref List<byte> cache)
    {
        if (cache.Count < 4) return null;
        MemoryStream ms = new MemoryStream(cache.ToArray());
        BinaryReader br = new BinaryReader(ms);
        int lenght = br.ReadInt32();//从缓存中读取int的消息长度
        if (lenght > ms.Length - ms.Position)
        {
            return null;
        }
        byte[] result = br.ReadBytes(lenght);
        cache.Clear();
        cache.AddRange(br.ReadBytes((int)(ms.Length - ms.Position)));
        br.Close();
        ms.Close();
        return result;
    }
    private static SocketModel mdecode(byte[] value)
    {
        ByteArray ba = new ByteArray(value);
        SocketModel model = new SocketModel();
        byte type;
        int area;
        int command;
        ba.read(out type);
        ba.read(out area);
        ba.read(out command);
        model.type = type;
        model.area = area;
        model.command = command;
        if (ba.Readnable)
        {
            byte[] message;
            ba.read(out message, ba.Length - ba.Position);
            model.message = SerialUtil.decode(message);
        }
        ba.Close();
        return model;
    }
}
