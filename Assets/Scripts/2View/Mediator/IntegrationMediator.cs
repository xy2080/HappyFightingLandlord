using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationMediator : EventMediator {
    [Inject]
   public  IntegrationView m_View { set; get; }

    [Inject]
    public RoundModel1 round { set; get; }

    public override void OnRegister()
    {
        m_View.m_Deal.onClick.AddListener(OnDealClick);

        m_View.m_Grab.onClick.AddListener(OnGrabClick);//抢地主按钮点击事件
        m_View.m_DisGrab.onClick.AddListener(OnDisGrabClick);//不抢地主按钮的响应事件

        //监听发牌结束命令
        dispatcher.AddListener(ViewEvent.CompleteDeal,OnCompleteDeal);

        RoundModel1.PlayerHandler += RoundModel1_PlayerHandler;//注册玩家出牌事件
        
        m_View.m_Play.onClick.AddListener(OnPlayClick);//注册出牌按钮点击事件
        m_View.m_Pass.onClick.AddListener(OnPassClick);//注册不出按钮点击事件
        //监听玩家出牌结束命令
        dispatcher.AddListener(ViewEvent.SuccessedPlayCard,OnSuccessPlayCard);

        dispatcher.AddListener(ViewEvent.ReStart, OnReStart);//重新开始游戏
    }

    /// <summary>
    /// 重新开始游戏
    /// </summary>
    /// <param name="payload"></param>
    private void OnReStart(IEvent payload)
    {
        m_View.DeactiveAll();
        m_View.ActivePlay();
    }

    /// <summary>
    /// 不出牌的按钮的点击事件
    /// </summary>
    private void OnPassClick()
    {
        m_View.DeactiveAll();//隐藏交互面板
        //发出Pass命令
        dispatcher.Dispatch(CommandEvent.PassCard);
    }
   
    /// <summary>
    /// 玩家出牌成功，隐藏交互面板
    /// </summary>
    /// <param name="payload"></param>
    private void OnSuccessPlayCard(IEvent payload)
    {
        m_View.DeactiveAll();
    }

    /// <summary>
    /// 出牌的按钮的点击事件
    /// </summary>
    private void OnPlayClick()
    {
        dispatcher.Dispatch(ViewEvent.RequsetPlayCard);
    }

  
    /// <summary>
    /// 处理玩家出牌的委托
    /// </summary>
    private void RoundModel1_PlayerHandler(bool isPass)
    {
        m_View.ActiveDeal(isPass);    //显示出牌交互按钮
    }

    public override void OnRemove()
    {
        m_View.m_Deal.onClick.RemoveListener(OnDealClick);
        m_View.m_Grab.onClick.RemoveListener(OnGrabClick);//移除抢地主按钮点击事件
        m_View.m_DisGrab.onClick.RemoveListener(OnDisGrabClick);//移除不抢地主按钮的响应事件

        dispatcher.RemoveListener(ViewEvent.CompleteDeal, OnCompleteDeal);//移除派发发地主牌命令

        RoundModel1.PlayerHandler -= RoundModel1_PlayerHandler;//移除玩家出牌事件

        m_View.m_Play.onClick.RemoveListener(OnPlayClick);//移除出牌按钮点击事件
        m_View.m_Pass.onClick.RemoveListener(OnPassClick);//移除不出按钮点击事件

        dispatcher.RemoveListener(ViewEvent.ReStart, OnReStart);//重新开始游戏
    }
  
    /// <summary>
    /// 监听发牌结束的回调函数
    /// </summary>
    private void OnCompleteDeal()
    {
        //激活抢地主交互按钮
        m_View.ActiveGrab();
    }

    /// <summary>
    /// 发牌按钮的响应事件
    /// </summary>
    private void OnDealClick()
    {
        //派发出牌命令
        dispatcher.Dispatch(CommandEvent.RequestDealCommand);
        m_View.DeactiveAll();//隐藏所有交互面板
    }

    /// <summary>
    /// 抢地主的响应事件
    /// </summary>
    private void OnGrabClick()
    {
       
        m_View.DeactiveAll();

        GrabAndDisGrab grab = new GrabAndDisGrab(){type = CharacterType.Player};
        //派发抢地主事件命令
        dispatcher.Dispatch(CommandEvent.GrabLandord,grab);
        Sound.Instance.PlayEffect(Consts.Grab);//音效
    }

   
    /// <summary>
    /// 不抢地主的按钮的响应事件
    /// </summary>
    private void OnDisGrabClick()
    {
        m_View.DeactiveAll();

        CharacterType temp = (CharacterType)UnityEngine.Random.Range(2,4); 

        GrabAndDisGrab grab = new GrabAndDisGrab() { type = temp };
        //派发处理是否抢地主事件命令
        dispatcher.Dispatch(CommandEvent.GrabLandord, grab);
        Sound.Instance.PlayEffect(Consts.DisGrab);//音效
    }
   
 
}
