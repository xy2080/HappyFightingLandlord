using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : View {
    public PlayerController m_player;
    public ComputerController m_ComputerLeft;
    public ComputerController m_ComputerRight;
    public DeskController m_Desk;
    /// <summary>
    /// 初始化玩家的身份
    /// </summary>
    public void Init()
    {
        m_player.PlayerIdentity = Identity.Farmer;
        m_ComputerLeft.PlayerIdentity = Identity.Farmer;
        m_ComputerRight.PlayerIdentity = Identity.Farmer;
    }
    /// <summary>
    /// 给玩家轮流发牌以及出牌的显示
    /// </summary>
    /// <param name="card">发的牌</param>
    /// <param name="type">发给谁</param>
    /// <param name="isSelected">是否被选中</param>
    public void AddCards(Card card,CharacterType type,bool isSelected,ShowPoint pos)
    {
        switch (type)
        {
            case CharacterType.Player:
                m_player.AddCard(card,isSelected);
                m_player.CardsSort(false);//降序排列
                break;
            case CharacterType.ComputerRight:
                m_ComputerRight.AddCard(card, isSelected);
                break;
            case CharacterType.ComputerLeft:
                m_ComputerLeft.AddCard(card,isSelected);
                break;
            case CharacterType.Desk:
                //地主牌以及其他玩家出牌,根据牌的归属的判断设置牌的位置
                m_Desk.AddCard(card,isSelected,pos);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 发三张地主牌
    /// </summary>
    /// <param name="type"></param>
    public void  DealThreeCard(CharacterType type)
    {
        //
        switch (type)
        {
            case CharacterType.Player:
                //从桌面取出，添加到玩家的手牌
                for (int i = 0; i < 3; i++)
                {
                    Card card = m_Desk.DealCard();
                    m_player.AddCard(card,true);
                    m_Desk.SetCard(card,i);//桌面显示地主牌
                }
                m_player.PlayerIdentity = Identity.Landlord; //设置玩家的地主身份
                m_player.CardsSort(false);//重新排序
                break;
            case CharacterType.ComputerRight:
                //从桌面取出，添加到右边电脑玩家的手牌
                for (int i = 0; i < 3; i++)
                {
                    Card card = m_Desk.DealCard();
                    m_ComputerRight.AddCard(card, false);
                    m_Desk.SetCard(card, i);//桌面显示地主牌
                }
                m_ComputerRight.PlayerIdentity = Identity.Landlord; //设置玩家的地主身份
                m_ComputerRight.CardsSort(true);//重新排序
                break;
            case CharacterType.ComputerLeft:
                //从桌面取出，添加到左边电脑玩家的手牌
                for (int i = 0; i < 3; i++)
                {
                    Card card = m_Desk.DealCard();
                    m_ComputerLeft.AddCard(card, false);
                    m_Desk.SetCard(card, i);//桌面显示地主牌
                }
                m_ComputerLeft.PlayerIdentity = Identity.Landlord; //设置玩家的地主身份
                m_ComputerLeft.CardsSort(true);//重新排序
                break;
            default:
                break;
        }
        m_Desk.ClearList(ShowPoint.Desk);
    }
}
