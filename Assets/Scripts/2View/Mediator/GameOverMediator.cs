using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMediator : EventMediator {
    [Inject]
    public GameOverView gameOverView { set; get; }
    public override void OnRegister()
    {
        dispatcher.AddListener(ViewEvent.ViewUpdateGameOver,OnUpdateGameOver);

        gameOverView.Btn.onClick.AddListener(OnReStartClick);

        dispatcher.Dispatch(CommandEvent.CommandUpdateGameOver);//初始化派发命令
    }
    /// <summary>
    /// 继续游戏按钮的点击事件
    /// </summary>
    private void OnReStartClick()
    {
        Destroy(this.gameObject);//移除当前面板
        //通知CharacterView,IntegrationView初始化游戏
        dispatcher.Dispatch(ViewEvent.ReStart);
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(ViewEvent.ViewUpdateGameOver, OnUpdateGameOver);
    }
    /// <summary>
    /// 监听游戏结束面板的更新回调函数
    /// </summary>
    /// <param name="payload"></param>
    private void OnUpdateGameOver(IEvent payload)
    {
        GameOverShowArgs args = (GameOverShowArgs)payload.data;//获取到当前玩家是否胜利以及是否是地主的数据
        gameOverView.Init(args.isLand,args.isWin);
    }
}
