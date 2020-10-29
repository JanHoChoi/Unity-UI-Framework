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

    // uicamera
    Camera uiCamera;
    public Camera UiCamera { get => uiCamera; }

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

        // 如果已有界面,则不执行任何操作
        if (sb != null)
        {
            if (sb.CtrlBase != null && !sb.CtrlBase.ctrlCanvas.enabled)
            {
                sb.CtrlBase.ctrlCanvas.enabled = true;
            }
            return sb;
        }
        sb = (ScreenBase)Activator.CreateInstance(type, param);

        mTypeScreens.Add(type, sb);
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
    }

    //返回登陆界面时，重置常驻UI的状态
    public void Reset()
    {

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