using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildScreenParam: UIOpenScreenParameterBase
{
    GuildCtrl mCtrl;

    GuildCreateSubScreen mSubCreate;    // 创建公会界面逻辑
    GuildInfoSubScreen mSubInfo;        // 公会详情界面逻辑

    public GuildScreen(UIOpenScreenParameterBase param = null) : base(UIConst.UIGuild, param)
    {

    }

    protected override void OnLoadSuccess()
    {
        base.OnLoadSuccess();
        mCtrl = mCtrlBase as GuildCtrl;
        
    }
}