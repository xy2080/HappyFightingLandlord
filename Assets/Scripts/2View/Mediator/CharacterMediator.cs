using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMediator : EventMediator {

	[Inject]
    public CharacterView characterView { set; get; }

    public override void OnRegister()
    {
        characterView.Init();//初始化为农民

        dispatcher.AddListener(ViewEvent.DealCard, DealCard);//监听发牌事件的命令，执行回调函数

        dispatcher.AddListener(ViewEvent.CompleteDeal,OnDealComplete);//监听发牌结束

        dispatcher.AddListener(CommandEvent.GrabLandord,OnGranCard);//监听发地主牌的命令

        dispatcher.AddListener(ViewEvent.RequsetPlayCard,OnPlayerPlayCard);//监听玩家请求出牌命令、

        dispatcher.AddListener(ViewEvent.SuccessedPlayCard,OnSuccessPlayCard);//玩家出牌成功

        RoundModel1.ComputerHandler += RoundModel1_ComputerHandler; //为电脑玩家出牌的委托注册事件
        RoundModel1.PlayerHandler += RoundModel1_PlayerHandler;

        dispatcher.AddListener(ViewEvent.UpdateIntegration,OnUpdateIntehration);//监听玩家积分显示命令

        dispatcher.Dispatch(CommandEvent.RequestUpdate);//派发更新积分

        dispatcher.AddListener(ViewEvent.ReStart, OnReStart);//重新开始游戏
    }
    /// <summary>
    /// 重新开始命令的回调函数
    /// </summary>
    /// <param name="payload"></param>
    private void OnReStart(IEvent payload)
    {
        //对象池回收
        Lean.Pool.LeanPool.DespawnAll();
        //清空数据
        characterView.m_player.Cards.Clear();
        characterView.m_ComputerLeft.Cards.Clear();
        characterView.m_ComputerRight.Cards.Clear();
        characterView.m_Desk.Cards.Clear();
        //更新UI显示
        characterView.Init();
        characterView.m_player.m_characterUI.SetRemainCard(0);
        characterView.m_ComputerLeft.m_characterUI.SetRemainCard(0);
        characterView.m_ComputerRight.m_characterUI.SetRemainCard(0);
        characterView.m_Desk.m_DeskUI.SetAlpha(0);
    }

    /// <summary>
    /// 更新UI积分显示
    /// </summary>
    /// <param name="payload"></param>
    private void OnUpdateIntehration(IEvent payload)
    {
        GameData data = (GameData)payload.data;
        characterView.m_player.m_characterUI.SetIntegration(data.m_playerIntegration);//玩家显示积分
        characterView.m_ComputerLeft.m_characterUI.SetIntegration(data.m_computerLeftIntegration);//电脑玩家积分
        characterView.m_ComputerRight.m_characterUI.SetIntegration(data.m_computerRightIntegration);//电脑玩家积分显示
    }

    /// <summary>
    /// 玩家出牌清空桌面集合再出
    /// </summary>
    /// <param name="obj"></param>
    private void RoundModel1_PlayerHandler(bool obj)
    {
        characterView.m_Desk.ClearList(ShowPoint.Player);
    }

    /// <summary>
    /// 注册电脑玩家的出牌委托
    /// </summary>
    /// <param name="obj"></param>
    public void   RoundModel1_ComputerHandler(ComputerSmartArgs obj)
     {

        StartCoroutine(DePlay(obj));
     }
     /// <summary>
     /// 电脑玩家出牌的方法
     /// </summary>
     /// <param name="obj"></param>
     /// <returns></returns>
     IEnumerator DePlay(ComputerSmartArgs obj)
    {
        yield return new WaitForSeconds(2);
        bool can = false;
        switch (obj.currentCharacter)
        {
          
            case CharacterType.ComputerRight:
                //清空桌面
                characterView.m_Desk.ClearList(ShowPoint.ComputerRight);      
                can = characterView.m_ComputerRight.SmartSelectCards(obj.cardType, obj.weight, obj.Length, CharacterType.ComputerRight == obj.isBiggest);
                //获取电脑玩家出牌的集合
                List<Card> Rightcards = characterView.m_ComputerRight.SelectedCard;
                //获取出牌的类型
                CardType RightcardType = characterView.m_ComputerRight.cardType;
                if (can)
                {
                    //桌面添加出的牌
                    foreach (var item in Rightcards)
                    {
                        characterView.m_Desk.AddCard(item, false, ShowPoint.ComputerRight);
                    }
                    //封装玩家出牌后的传递数据
                    PlayCardArgs play = new PlayCardArgs() { cardType = RightcardType, length = Rightcards.Count, weight = ToolsManager.GetWeight(Rightcards, RightcardType), characterType = CharacterType.ComputerRight };

                    //删除手牌
                    //在调用SmartSelectCards方法时，出牌成功，就自动删除
                    //出牌成功
                    if (!characterView.m_ComputerRight.IHasCard)//判断牌是否有剩余
                    {
                        //游戏结束
                        Identity p= characterView.m_player.PlayerIdentity;
                        Identity Left = characterView.m_ComputerLeft.PlayerIdentity;
                        Identity Right = characterView.m_ComputerRight.PlayerIdentity;
                        CharacterType gg=CharacterType.Player;
                        if(p==Identity.Landlord)
                        {
                            gg = CharacterType.Player;
                        }
                        else if (Left == Identity.Landlord)
                        {
                            gg = CharacterType.ComputerLeft;
                        }
                        else if (Right == Identity.Landlord)
                        {
                            gg = CharacterType.ComputerRight;
                        }
                        GameOverArgs e = new GameOverArgs()
                        {
                            ComputerRigh = true,
                            ComputerLeft = Right == Left ? true : false,
                            PlayerWin = p == Right ? true : false,
                            type = gg,
                            isLand = p == Identity.Landlord ? true : false
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver,e);
                    }
                    else
                    {
                        dispatcher.Dispatch(CommandEvent.PlayCard, play);//下家出牌
                    }
                }
                else
                {
                    //不出牌
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
            case CharacterType.ComputerLeft:
                //清空桌面
                characterView.m_Desk.ClearList(ShowPoint.ComputerLeft);
                can =characterView.m_ComputerLeft.SmartSelectCards(obj.cardType,obj.weight,obj.Length,CharacterType.ComputerLeft==obj.isBiggest);
                //获取电脑玩家出牌的集合
                List<Card> cards = characterView.m_ComputerLeft.SelectedCard;
                //获取出牌的类型
                CardType cardType = characterView.m_ComputerLeft.cardType;
                if (can)
                {
                    //桌面添加出的牌
                    foreach (var item in cards)
                    {
                        characterView.m_Desk.AddCard(item,false,ShowPoint.ComputerLeft);
                    }
                    //封装玩家出牌后的传递数据
                    PlayCardArgs play = new PlayCardArgs() { cardType = cardType, length = cards.Count, weight = ToolsManager.GetWeight(cards, cardType), characterType = CharacterType.ComputerLeft };

                    //删除手牌
                    //在调用SmartSelectCards方法时，出牌成功，就自动删除
                    //出牌成功
                    if (!characterView.m_ComputerLeft.IHasCard)//判断牌是否有剩余
                    {
                        //游戏结束
                        Identity p = characterView.m_player.PlayerIdentity;
                        Identity Left = characterView.m_ComputerLeft.PlayerIdentity;
                        Identity Right = characterView.m_ComputerRight.PlayerIdentity;
                        CharacterType gg = CharacterType.Player;
                        if (p == Identity.Landlord)
                        {
                            gg = CharacterType.Player;
                        }
                        else if (Left == Identity.Landlord)
                        {
                            gg = CharacterType.ComputerLeft;
                        }
                        else if (Right == Identity.Landlord)
                        {
                            gg = CharacterType.ComputerRight;
                        }
                        GameOverArgs e = new GameOverArgs()
                        {
                            ComputerLeft = true,
                            ComputerRigh = Right == Left ? true : false,
                            PlayerWin = p == Left ? true : false,
                            type=gg,
                            isLand = p == Identity.Landlord ? true : false
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver, e);//派发游戏结束命令
                    }
                    else
                    {
                        dispatcher.Dispatch(CommandEvent.PlayCard,play);//下家出牌
                    }
                }
                else
                {
                    //不出牌
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
            default:
                break;
        }
         // characterView.m_ComputerLeft.SmartSelectCards();
    }

    /// <summary>
    /// 响应玩家出牌成功的命令
    /// </summary>
    /// <param name="payload"></param>
    private void OnSuccessPlayCard()
    {
        List<Card> cardsList = characterView.m_player.FindCards();//获取被选中牌的集合
        characterView.m_Desk.ClearList(ShowPoint.Player);//清空玩家显示牌的桌面
        foreach (Card item in cardsList)//将出的牌显示到桌面
        {
            characterView.m_Desk.AddCard(item,false,ShowPoint.Player);
        }

        characterView.m_player.DestorySelected();//移除玩家手牌中被选中的牌
        characterView.m_player.CardsSort(false);

        if(!characterView.m_player.IHasCard)
        {
            //游戏结束
            Identity p = characterView.m_player.PlayerIdentity;
            Identity Left = characterView.m_ComputerLeft.PlayerIdentity;
            Identity Right = characterView.m_ComputerRight.PlayerIdentity;
            CharacterType gg = CharacterType.Player;
            if (p == Identity.Landlord)
            {
                gg = CharacterType.Player;
            }
            else if (Left == Identity.Landlord)
            {
                gg = CharacterType.ComputerLeft;
            }
            else if (Right == Identity.Landlord)
            {
                gg = CharacterType.ComputerRight;
            }
            GameOverArgs e = new GameOverArgs()
            {
                PlayerWin = true,
                ComputerRigh = Right == p ? true : false,
                ComputerLeft = p == Left ? true : false,
                type = gg,
                isLand = p == Identity.Landlord ? true:false
            };
            dispatcher.Dispatch(CommandEvent.GameOver, e);//派发游戏结束命令

        }
    }

    /// <summary>
    /// 玩家出牌的类型
    /// </summary>
    /// <param name="payload"></param>
    private void OnPlayerPlayCard()
    {
        List<Card> cardsList = characterView.m_player.FindCards();//获取被选中牌的集合
        CardType pcardType;
       if( Rulers.CanPOp(cardsList, out pcardType))//判断能否出牌，并返回出牌的类型
        {
            //把数据封装，让外界处理选择牌能否出成功
            PlayCardArgs play = new PlayCardArgs()
            {
                characterType = CharacterType.Player,
                length = cardsList.Count,
                weight = ToolsManager.GetWeight(cardsList,pcardType),
                cardType = pcardType
            };
            dispatcher.Dispatch(CommandEvent.PlayCard,play);//派发判断能否出牌成功的命令
        }

    }

    /// <summary>
    /// 发地主牌
    /// </summary>
    /// <param name="payload"></param>
    private void OnGranCard(IEvent payload)
    {
        //调用发牌命令
        GrabAndDisGrab temp =(GrabAndDisGrab) payload.data;
        characterView.DealThreeCard(temp.type);
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(ViewEvent.DealCard, DealCard);
        dispatcher.RemoveListener(ViewEvent.CompleteDeal, OnDealComplete);
        dispatcher.RemoveListener(CommandEvent.GrabLandord, OnGranCard);//移除监听发地主牌的命令
        dispatcher.RemoveListener (ViewEvent.RequsetPlayCard, OnPlayerPlayCard);//移除监听玩家请求出牌命令、
        dispatcher.RemoveListener(ViewEvent.SuccessedPlayCard, OnSuccessPlayCard);//移除玩家出牌成功
        dispatcher.RemoveListener(ViewEvent.UpdateIntegration, OnUpdateIntehration);//监听玩家积分显示命令
    }

    /// <summary>
    /// 监听命令的回调函数处理发牌
    /// </summary>
    /// <param name="e">//派发命令的参数值</param>
    private void DealCard(IEvent e)
    {
        DealCardArgs dc = (DealCardArgs)e.data;
        characterView.AddCards(dc.card,dc.type,dc.isSelected,ShowPoint.Desk);
        Sound.Instance.PlayEffect(Consts.DealCard);//发牌音效
    }

    /// <summary>
    /// 监听发牌结束的命令的回调函数
    /// </summary>
    /// <param name="payload"></param>
    private void OnDealComplete(IEvent payload)
    {
        characterView.m_ComputerRight.CardsSort(true);
        characterView.m_ComputerLeft.CardsSort(true);
        characterView.m_Desk.CardsSort(true);
    }
  

}
