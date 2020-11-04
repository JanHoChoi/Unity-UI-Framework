using UnityEngine;

public enum EScreenPriority
{
    Default = 0,            //大厅以下 预留 目前没有使用到
    PriorityLobby = 10,     //大厅层
    PriorityLobbyFace = 15,                    //大厅上运营活动
    PriorityLobbyForSystem = 20,   //大厅上各种外围系统层级
    PriorityLobbyForMatchingSystem0 = 40,   //大厅上的各种邀请或者浮动界面 
    PriorityLowLoadingCommonMessageBoxTips0 = 50, //游戏中各种通用弹框（低于loading页面）
    PriorityLobbyForLoading0 = 60, //各种loading页面层级
    PriorityUpLoadingCommonMessageBoxTips0 = 70, //游戏中各种通用弹框层级（高于loading页面）
    PriorityLobbyForNewPlayerGuide0 = 80, //游戏中新手指引层级

    //PriorityCount = 100
};

public enum EUICareAboutMoneyType
{
    Silver,// 银币
    Gold,// 金币
    Strength,// 体力
    Gem// 钻石
}

public class UICtrlBase : UIFEventAutoRelease
{
    [HideInInspector]
    public Canvas ctrlCanvas;

    [Tooltip("ScreenBase层级")]
    public EScreenPriority screenPriority = EScreenPriority.PriorityLobbyForSystem;

    [Tooltip("是否使用遮罩,点击遮罩会关闭当前页面")]
    public bool m_UseMask = false;

    [Tooltip("勾选后,不会被mHideOtherScreenWhenThisOnTop控制")]
    public bool mAlwaysShow = false;
    [Tooltip("勾选后,当该界面打开,会隐藏它下面的其他非AlwaysShow界面")]
    public bool mHideOtherScreenWhenThisOnTop = false;

    [Tooltip("是否关心货币栏的状态")]
    public bool mBCareAboutMoney = false;
    [Tooltip("如果CareAboutCurrency为True,那么它关心哪些类型")]
    public EUICareAboutMoneyType[] mLMoneyTypes;
    /*
        最上层界面判断自身是否关心货币栏. 如果关心,则通过事件告诉货币栏它关心哪些货币,货币栏本身决定怎么显示这些货币
    */

    void Awake()
    {
        ctrlCanvas = GetComponent<Canvas>();
    }
}