using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase {
    public CharacterUI m_characterUI;

    Identity m_playerIdentity;
    /// <summary>
    /// 设置玩家的身份
    /// </summary>
    public Identity PlayerIdentity
    {
        get
        {
            return m_playerIdentity;
        }
        set
        {
            m_playerIdentity = value;
            m_characterUI.SetIdentity(value);
        }
    }

    /// <summary>
    /// 添加牌数，更新牌的数量显示
    /// </summary>
    /// <param name="card"></param>
    /// <param name="isSelected"></param>
    public override void AddCard(Card card, bool isSelected)
    {
        base.AddCard(card, isSelected);
        m_characterUI.SetRemainCard(RemainCardCount);
    }
    /// <summary>
    /// 出牌，更新剩余牌数显示
    /// </summary>
    /// <returns></returns>
    public override Card DealCard()
    {
        Card card= base.DealCard();
        m_characterUI.SetRemainCard(RemainCardCount);
        return card;
    }

    List<Card> cards=null;
    List<CardUI> cardsUI=null;
    /// <summary>
    /// 获取选中的牌的集合
    /// </summary>
    /// <returns></returns>
    public List<Card> FindCards()
    {
        cards = new List<Card>();
        cardsUI = new List<CardUI>();
        CardUI[] uIs = CreatePoint.GetComponentsInChildren<CardUI>();
        for (int i = 0; i < uIs.Length; i++)
        {
            if(uIs[i].IsSelected)
            {
                cards.Add(uIs[i].Card);
                cardsUI.Add(uIs[i]);
            }
        }
        ToolsManager.SortList(cards,true);//排列选中的牌
        return cards;
    }
    /// <summary>
    /// 出牌成功，移除卡牌，重新牌排序UI显示
    /// </summary>
    public void DestorySelected()
    {
        if(cards==null||cardsUI==null)
        {
            return;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cardsUI[i].Destory();
            Cards.Remove(cards[i]);
        }
        //出牌后，重新设置牌的位置
        CardUISort(cards);
        m_characterUI.SetRemainCard(RemainCardCount);
    }
}
