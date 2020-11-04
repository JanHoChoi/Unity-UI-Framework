using UnityEngine;

public class GuildInfoSubScreen : SubScreenBase
{
    GuildInfoSubCtrl mCtrl;         // 公会信息界面控件

    public GuildInfoSubScreen(GuildInfoSubCtrl subCtrl) : base(subCtrl)
    {
    }

    protected override void Init()
    {
        mCtrl = mCtrlBase as GuildInfoSubCtrl;

        mCtrl.btnClose.onClick.AddListener(OnCloseClick);
        mCtrl.btnJumpTask.onClick.AddListener(OnJumpTask);
    }

    private void OnCloseClick()
    {
        GameUIManager.GetInstance().CloseUI(typeof(GuildScreen));
    }

    private void OnJumpTask()
    {
        GameUIManager.GetInstance().OpenUI(typeof(TaskScreen));
    }
}