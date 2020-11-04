using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCityScreen : ScreenBase
{
    MainCityCtrl mCtrl;

    public MainCityScreen(UIOpenScreenParameterBase param = null) : base(UIConst.UIMainCity, param)
    {
        
    }

    protected override void OnLoadSuccess() // 做一些初始化工作
    {
        base.OnLoadSuccess();
        mCtrl = mCtrlBase as MainCityCtrl;
        mCtrl.txtLv.text = 20.ToString();
        mCtrl.btnGuild.onClick.AddListener(OnGuildClick);
        mCtrl.btnTask.onClick.AddListener(OnTaskClick);
    }

    private void OnGuildClick()
    {
        GameUIManager.GetInstance().OpenUI(typeof(GuildScreen));
    }

    private void OnTaskClick()
    {
        GameUIManager.GetInstance().OpenUI(typeof(TaskScreen));
    }

    protected override void UIAdapt(Vector2Int res)
    {
        Debug.Log(string.Format("分辨率发生了变化，宽为{0}，高为{1}", res.x, res.y));
    }
}
