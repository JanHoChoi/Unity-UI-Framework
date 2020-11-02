using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildScreen: ScreenBase
{
    GuildCtrl mCtrl;                    // 主界面控件

    GuildCreateSubScreen mSubCreate;    // 创建公会界面逻辑
    GuildInfoSubScreen mSubInfo;        // 公会详情界面逻辑

    public GuildScreen(UIOpenScreenParameterBase param = null) : base(UIConst.UIGuild, param)
    {

    }

    protected override void OnLoadSuccess()
    {
        base.OnLoadSuccess();
        mCtrl = mCtrlBase as GuildCtrl;
        
        bool bHaveGuild = PlayerData.GetInstance().HaveGuild();      // 有公会就打开公会详情 没有就打开创建界面

        // 子界面的显示/隐藏
        mCtrl.subInfo.gameObject.SetActive(bHaveGuild);
        mCtrl.subCreate.gameObject.SetActive(!bHaveGuild);

        if(bHaveGuild)
        {
            mSubInfo = new GuildInfoSubScreen(mCtrl.subInfo);
        }
        else
        {
            mSubCreate = new GuildCreateSubScreen(mCtrl.subCreate);
        }
    }
}