using UnityEngine;

public class ScreenBase
{
    public GameObject mPanelRoot = null;
    public string mStrUIName = "";
    protected UICtrlBase mCtrlBase;
    
    public int mOpenOrder = 0;  // 界面打开顺序
    public int mSortingLayer = 0;   // sortingLayer层级

    // 界面打开的传入参数
    protected UIOpenScreenParameterBase mOpenParam;

    public UICtrlBase CtrlBase { get => mCtrlBase;}

    public ScreenBase(string UIName, UIOpenScreenParameterBase param = null)
    {
        StartLoad(UIName, param);
    }

    public virtual void StartLoad(string UIName, UIOpenScreenParameterBase param = null)
    {
        mStrUIName = UIName;
        mOpenParam = param;
        ResourcesMgr.GetInstance().LoadAsset<GameObject>(UIName, PanelLoadComplete);
    }

    // 资源加载完成
    void PanelLoadComplete(GameObject ao)
    {
        mPanelRoot = Object.Instantiate(ao, GameUIManager.GetInstance().GetUIRootTransform());
        // 获取控件对象
        mCtrlBase = mPanelRoot.GetComponent<UICtrlBase>();

        // 更新层级信息
        UpdateLayoutLevel();

        // 调用加载成功方法
        OnLoadSuccess();

        // 添加到控制层
        GameUIManager.GetInstance().AddUI(this);
    }
    // 脚本处理完成
    virtual protected void OnLoadSuccess()
    {
        // 注册适配监听事件
        mCtrlBase.AutoRelease(EventManager.OnScreenResolutionChange.Subscribe(UIAdapt));

        if(mCtrlBase.m_UseMask)
            MaskScreenManager.GetInstance().Show(this);
    }

    virtual protected void UIAdapt(Vector2Int res)
    {
        
    }

    virtual public void OnClose()
    {
        GameUIManager.GetInstance().RemoveUI(this);
    }

    // 设置order in layer
    public void SetOpenOrder(int openOrder)
    {
        mOpenOrder = openOrder;
        if(mCtrlBase != null && mCtrlBase.ctrlCanvas != null)
        {
            mCtrlBase.ctrlCanvas.sortingOrder = openOrder;
        }
    }

    // 更新UI的层级
    private void UpdateLayoutLevel()
    {
        var camera = GameUIManager.GetInstance().GetUICamera();
        if(camera != null)
        {
            mCtrlBase.ctrlCanvas.worldCamera = camera;
        }
        mCtrlBase.ctrlCanvas.pixelPerfect = true;
        mCtrlBase.ctrlCanvas.overrideSorting = true;
        mCtrlBase.ctrlCanvas.sortingLayerID = (int)mCtrlBase.screenPriority;
        mSortingLayer = (int)mCtrlBase.screenPriority;
        mCtrlBase.ctrlCanvas.sortingOrder = mOpenOrder;
    }

    virtual public void Dispose()
    {
        GameObject.Destroy(mPanelRoot);
    }

    virtual public void OnClickMaskArea()
    {
        OnClose();
    }

}
