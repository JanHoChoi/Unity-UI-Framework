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
    }

    private void OnGuildClick()
    {
        GameUIManager.GetInstance().OpenUI(typeof(GuildScreen));
    }
}
