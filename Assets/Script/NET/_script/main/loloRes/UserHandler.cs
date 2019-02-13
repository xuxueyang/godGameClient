using Assets.Scripts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UserHandler : MonoBehaviour, IHandler
{/*
    public void MessageReceive(SocketModel model)
    {
        switch (model.command)
        {
            case Protocol.UserProtocol.INFO_SRES:
                info(model.getMessage<UserDTO>());
                break;
            case Protocol.UserProtocol.CREATE_SRES:
                create(model.getMessage<bool>());
                break;
            case Protocol.UserProtocol.ONLINE_SRES:
                online(model.getMessage<UserDTO>());
                break;
        }
    }

    private void info(UserDTO user)
    {
        GameData.user = user;
        if (user == null)
        {
            //显示创建面板
            SendMessage("OpenCreate");
        }
        else
        {
            //已经有召唤师，加载游戏
            this.Write(Protocol.Protocol.TYPE_USER, 0, Protocol.UserProtocol.ONLINE_CREQ, null);
        }
    }
    private void online(UserDTO user)
    {
        GameData.user = user;
        //移除遮罩
        SendMessage("CloseMask");
        //刷新UI数据
        GetComponent<MainScene>().RefreshView();

        warningManage.errors.Add(new WarningModel("已上线"));

    }
    private void create(bool value)
    {
        if (value)
        {
            warningManage.addError(new WarningModel("创建成功",delegate {
                //关闭创建面板
                SendMessage("CloseCreate");
                //直接申请登陆
                this.Write(Protocol.Protocol.TYPE_USER, 0, Protocol.UserProtocol.ONLINE_CREQ, null);
            }));
;
        }
        else
        {
            warningManage.addError(new WarningModel("创建失败", delegate {
                //刷新创建面板
                SendMessage("OpenCreate");
            }));

        }
    }
    */
    public void MessageReceive(SocketModel model)
    {
        throw new NotImplementedException();
    }
}

