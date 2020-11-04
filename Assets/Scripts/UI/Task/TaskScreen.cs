using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScreen : ScreenBase
{
    TaskCtrl mCtrl;

    public TaskScreen(UIOpenScreenParameterBase param = null) : base(UIConst.UITask, param)
    {
        
    }

    protected override void OnLoadSuccess() // 做一些初始化工作
    {
        base.OnLoadSuccess();
        mCtrl = mCtrlBase as TaskCtrl;
        mCtrl.btnClose.onClick.AddListener(OnCloseClick);
    }

    private void OnCloseClick()
    {
        GameUIManager.GetInstance().CloseUI(typeof(TaskScreen));
    }
}
