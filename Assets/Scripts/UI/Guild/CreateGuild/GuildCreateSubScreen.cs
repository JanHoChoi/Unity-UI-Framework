using UnityEngine;

public class GuildCreateSubScreen : SubScreenBase
{
    GuildCreateSubCtrl mCtrl;       // 创建公会界面控件

    public GuildCreateSubScreen(GuildCreateSubCtrl subCtrl) : base(subCtrl)
    {
    }

    protected override void Init()
    {
        mCtrl = mCtrlBase as GuildCreateSubCtrl;
        
        mCtrl.btnCreate.onClick.AddListener(OnCreateClick);
        mCtrl.btnClose.onClick.AddListener(OnCloseClick);
        Debug.Log("Init create sub screen");
    }

    private void OnCreateClick()
    {
        PlayerData.GetInstance().SetHaveGuild(true);
    }

    private void OnCloseClick()
    {
        Debug.Log("关闭界面xxx");
        GameUIManager.GetInstance().CloseUI(typeof(GuildScreen));
    }

}