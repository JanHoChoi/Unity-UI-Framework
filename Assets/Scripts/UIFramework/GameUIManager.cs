using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    // UI列表缓存
    static Dictionary<Type, ScreenBase> mTypeScreens = new Dictionary<Type, ScreenBase>();


    public GameObject uiRoot;
    public GameObject poolRoot // 缓存节点
    {
        get;
        private set;
    }

    public int mUIOpenOrder = 0;        // UI打开时的Order值 用来标识界面打开先后顺序

    // uicamera
    Camera uiCamera;
    public Camera UiCamera { get => uiCamera; }

    private static Vector2Int ScreenResolution = new Vector2Int(1136, 640);

    private void Update()
    {
        if(ScreenResolution.x != Screen.width || ScreenResolution.y != Screen.height)
        {
            ScreenResolution = new Vector2Int(Screen.width, Screen.height);
            EventManager.OnScreenResolutionChange.BroadcastEvent(ScreenResolution);
        }
    }

    protected override void Init()
    {
        // 初始化UI根节点
        uiRoot = Instantiate(Resources.Load<GameObject>("UIRoot"), transform);
        uiCamera = uiRoot.GetComponent<Canvas>().worldCamera;

        // 初始化UI缓存池
        poolRoot = new GameObject("UIPoolRoot");
        poolRoot.transform.SetParent(transform);

        Canvas canvas = poolRoot.AddComponent<Canvas>();
        canvas.enabled = false;
    }

    /// <summary>
    ///  UI打开入口没有判断条件直接打开
    /// </summary>
	public ScreenBase OpenUI(Type type, UIOpenScreenParameterBase param = null)
    {
        ScreenBase sb = GetUI(type);
        mUIOpenOrder += 1;

        // 如果已有界面,则不执行任何操作
        if (sb != null)
        {
            if (sb.CtrlBase != null && !sb.CtrlBase.ctrlCanvas.enabled)
            {
                sb.CtrlBase.ctrlCanvas.enabled = true;
            }
            // 处理最上层界面
            if (sb.CtrlBase.mHideOtherScreenWhenThisOnTop)
            {
                ProcessUIOnTop();
            }
            // 处理金币栏
            ProcessMoneyType();
            return sb;
        }
        sb = (ScreenBase)Activator.CreateInstance(type, param);

        mTypeScreens.Add(type, sb);
        sb.SetOpenOrder(mUIOpenOrder);

        // 处理最上层界面
        if (sb.CtrlBase.mHideOtherScreenWhenThisOnTop)
        {
            ProcessUIOnTop();
        }
        // 处理金币栏
        ProcessMoneyType();

        return sb;
    }

    /// <summary>
    /// UI外部调用的获取接口
    /// </summary>
    public ScreenBase GetUI(Type type)
    {
        if (!typeof(ScreenBase).IsAssignableFrom(type))     // 判断type能不能cast to基类
            return default;
        ScreenBase sb = null;
        if (mTypeScreens.TryGetValue(type, out sb))         // 判断字典里面有没有
            return sb;
        return null;
    }

    /// <summary>
    /// UI外部调用的获取接口
    /// </summary>
    public TScreen GetUI<TScreen>() where TScreen : ScreenBase
    {
        Type type = typeof(TScreen);

        ScreenBase sb = null;

        if (mTypeScreens.TryGetValue(type, out sb))
        {
            return (TScreen)sb;
        }

        return null;
    }

    /// <summary>
    /// UI外部调用的关闭接口
    /// </summary>
    public bool CloseUI(Type type)
    {
        ScreenBase sb = GetUI(type);
        if (sb != null)
        {
            if (type == typeof(ScreenBase))     // 标尺界面是测试界面 不用关闭
                return false;
            else
                sb.OnClose();
            return true;
        }
        return false;
    }

    public void CloseAllUI()
    {
        // 销毁会从容器中删除 不能用正常遍历方式
        List<Type> keys = new List<Type>(mTypeScreens.Keys);
        foreach (var k in keys)
        {
            if (k == typeof(ScreenBase))// 标尺界面是测试界面 不用关闭
            {
                continue;
            }
            if (mTypeScreens.ContainsKey(k))
                mTypeScreens[k].OnClose();
        }
    }

    /// <summary>
    /// UI创建时候自动处理的UI打开处理 一般不要手动调用
    /// </summary>
    public void AddUI(ScreenBase sBase)
    {
        sBase.mPanelRoot.transform.SetParent(GetUIRootTransform());

        sBase.mPanelRoot.transform.localPosition = Vector3.zero;
        sBase.mPanelRoot.transform.localScale = Vector3.one;
        sBase.mPanelRoot.transform.localRotation = Quaternion.identity;
        sBase.mPanelRoot.name = sBase.mPanelRoot.name.Replace("(Clone)", "");
    }

    /// <summary>
    /// UI移除时候自动处理的接口 一般不要手动调用
    /// </summary>
    public void RemoveUI(ScreenBase sBase)
    {
        if (mTypeScreens.ContainsKey(sBase.GetType()))  // 根据具体需求决定到底是直接销毁还是缓存
            mTypeScreens.Remove(sBase.GetType());
        sBase.Dispose();

        // 处理最上层界面
        if (sBase.CtrlBase.mHideOtherScreenWhenThisOnTop)
        {
            ProcessUIOnTop();
        }
        // 处理金币栏
        ProcessMoneyType();
    }

    //返回登陆界面时，重置常驻UI的状态
    public void Reset()
    {

    }

    List<ScreenBase> sortTemp = new List<ScreenBase>();
    private void ProcessUIOnTop()
    {
        sortTemp.Clear();
        foreach (var s in mTypeScreens.Values)
        {
            sortTemp.Add(s);
        }

        // 排序 按层级高->低的顺序
        sortTemp.Sort(
            (a, b) =>
            {
                if (a.mSortingLayer == b.mSortingLayer)
                {
                    return b.mOpenOrder.CompareTo(a.mOpenOrder);    // 1表示b.mOpenOrder > a, 所以a放在后面
                }
                return b.mSortingLayer.CompareTo(a.mSortingLayer);
            }
        );

        int index = 0;

        for (index = 0; index < sortTemp.Count; index++)
        {
            var tempC = sortTemp[index];
            if (tempC.CtrlBase.mHideOtherScreenWhenThisOnTop)
            {
                // 找到第一个mHideOtherScreenWhenThisOnTop为true的界面,记录它的index
                tempC.CtrlBase.ctrlCanvas.enabled = true;
                break;
            }
            else
            {
                tempC.CtrlBase.ctrlCanvas.enabled = true;
                continue;
            }
        }

        // index后面的UI界面需要隐藏 
        for (int i = index + 1; i < sortTemp.Count; i++)
        {
            var tempC = sortTemp[i];
            if (!tempC.CtrlBase.mAlwaysShow)
            {
                // 找到需要被隐藏的界面 隐藏就好
                tempC.CtrlBase.ctrlCanvas.enabled = false;
            }
        }
    }

    private void ProcessMoneyType()
    {
        sortTemp.Clear();
        foreach (var s in mTypeScreens.Values)
        {
            sortTemp.Add(s);
        }
        // 排序 按层级高->低的顺序
        sortTemp.Sort(
            (a, b) =>
            {
                if (a.mSortingLayer == b.mSortingLayer)
                {
                    return b.mOpenOrder.CompareTo(a.mOpenOrder);    // 1表示b.mOpenOrder > a, 所以a放在后面
                }
                return b.mSortingLayer.CompareTo(a.mSortingLayer);
            }
        );

        // 找到第一个关心货币栏的界面
        for(int i = 0; i < sortTemp.Count; ++i)
        {
            if(sortTemp[i].CtrlBase.mBCareAboutMoney)
            {
                EventManager.OnMoneyTypeChange.BroadcastEvent(sortTemp[i].CtrlBase.mLMoneyTypes);
                break;
            }
        }
    }

    #region 通用API
    //获取UIRoot节点
    public Transform GetUIRootTransform()
    {
        return transform;
    }

    public Camera GetUICamera()
    {
        return uiCamera;
    }
    #endregion
}