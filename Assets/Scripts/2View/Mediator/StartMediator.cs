using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

public class StartMediator : EventMediator {
    /// <summary>
    /// 持有StartView的引用
    /// </summary>
    [Inject]
   public StartView view { set; get; }
    /// <summary>
    /// 给控件注册点击事件，当GameContext类中的mapBindings()方法，绑定结束后，
    /// 在程序运行挂载的脚本时会自行注册方法
    /// </summary>
    public override void OnRegister()
    {
        view.m_Normal.onClick.AddListener(OnNormalClick);
        view.m_Double.onClick.AddListener(OnDoubleClick);
    }
    /// <summary>
    /// 移除监听事件，当StartView脚本或控件被移除
    /// </summary>
    public override void OnRemove()
    {
        
    }

    private void  OnNormalClick()
    {
        //更改倍数
        dispatcher.Dispatch(CommandEvent.ChangeMulitiple,1);    //派发命令的派发器.派发命令的方法(类型,数值)
        //删除当前显示面板
        Destroy(view.gameObject);
    }
    private void  OnDoubleClick()
    {

        //更改倍数
        dispatcher.Dispatch(CommandEvent.ChangeMulitiple, 2);    //派发命令的派发器.派发命令的方法(类型,数值)
        //删除当前显示面板
        Destroy(view.gameObject);
    }
}
