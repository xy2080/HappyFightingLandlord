using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class ComputerController : CharacterBase {
    public CharacterUI m_characterUI;
    public CanvasGroup m_CanvasGroup;
    public ComputerAI m_ComputerAI;//智能出牌类的对象
  //  List<Card> m_Selected;
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
    /// 要出的牌的集合
    /// </summary>
    public List<Card> SelectedCard
    {
        get
        {
            return m_ComputerAI.m_DealCardsList;
        }
     
       
    }
    /// <summary>
    /// 要出的牌的类型
    /// </summary>
    public CardType  cardType
    {
        get
        {
            return m_ComputerAI.m_cardType;
        }
    }


    /// <summary>
    /// 添加牌数，更新牌的数量显示
    /// </summary>
    /// <param name="card"></param>
    /// <param name="isSelected"></param>
    public  void AddCard(Card card, bool isSelected)
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
        Card card = base.DealCard();
        m_characterUI.SetRemainCard(RemainCardCount);
        return card;
    }
    /// <summary>
    /// 不出
    /// </summary>
    public void  ComputerPass()
    {
        m_CanvasGroup.alpha = 1;
        StartCoroutine(PassWaitTime());
    }
    IEnumerator PassWaitTime()
    {
        yield return new WaitForSeconds(Random.Range(1,6));
        m_CanvasGroup.alpha = 0;
    }
   
    /// <summary>
    /// AI出牌
    /// </summary>
    /// <param name="cards">手牌</param>
    /// <param name="cardType">上家出的牌的类型</param>
    /// <param name="weight">上家牌的权值</param>
    /// <param name="length">牌的长度</param>
    /// <param name="isBiggest">是否是最大的牌</param>
   public bool SmartSelectCards(CardType cardType, int weight, int length, bool isBiggest)
    {
        m_ComputerAI.SmartSelectCards(Cards,cardType,weight,length,isBiggest);
        if(SelectedCard.Count!=0)
        {
            //删除手牌
            DestoryCard();
            return true;
        }
        else
        {
            //不出
            ComputerPass();
            return false;
        }
       
    }
    /// <summary>
    /// 出牌成功后删除出的牌
    /// </summary>
    public void DestoryCard()
    {
        CardUI[] cardUIs = CreatePoint.GetComponentsInChildren<CardUI>();
        foreach (Card item in SelectedCard)
        {
            foreach  (CardUI uI in cardUIs)
            {
                if(item==uI.Card)
                {
                    uI.Destory();
                    Cards.Remove(item);
                }
            }
        }
        //排序UI显示
        //更新剩余牌数显示
        CardUISort(Cards);
        m_characterUI.SetRemainCard(RemainCardCount);
    }
}
