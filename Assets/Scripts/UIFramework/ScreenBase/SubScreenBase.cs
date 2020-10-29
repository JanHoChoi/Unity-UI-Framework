/*
    子界面类，一个主界面管理多个子界面。
*/
public class SubScreenBase
{
    protected UISubCtrlBase mCtrlBase;  // 子界面也拥有各自的UISubCtrlBase

    public UICtrlBase CtrlBase { get => mCtrlBase;}

    public SubScreenBase(UISubCtrlBase ctrlBase)
    {
        mCtrlBase = ctrlBase;
        Init();
    }

    virtual protected void Init()
    {

    }

    virtual public void Dispose()
    {
        
    }

}