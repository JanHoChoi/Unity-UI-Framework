using UnityEngine;
using UnityEngine.UI;

public class MaskScreenManager
{
    protected static MaskScreenManager instance;

    public static MaskScreenManager GetInstance()
    {
        if(instance == null)
        {
            instance = new MaskScreenManager();
        }
        return instance;
    }

    GameObject goAutoMask;

    public void Show(ScreenBase screen)
    {
        if(goAutoMask == null)
        {
            ResourcesMgr.GetInstance().LoadAsset<GameObject>("UIAutoMask", (ao) => {
                goAutoMask = ao;
                AttachEvent(screen);
            });
        }
        else{
            AttachEvent(screen);
        }
    }

    private void AttachEvent(ScreenBase screen)
    {
        if(screen == null || screen.CtrlBase == null)   // 以防界面当帧打开后又立即关闭
            return;
        var go = Object.Instantiate(goAutoMask, screen.mPanelRoot.transform);   // 创建遮罩挂载好位置
        go.transform.SetAsFirstSibling();
        go.name = "UIAutoMask_Created by Mask ScreenManager";
        Button btnMask = go.GetComponent<Button>();
        if(btnMask != null)
        {
            btnMask.onClick.AddListener(screen.OnClickMaskArea);
        }
    }
}