using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class DeskController : CharacterBase {
    /// <summary>
    /// 在桌面现实的三个玩家出的牌
    /// </summary>
    public DeskUI m_DeskUI;
    List<Card> m_PlayerList=new List<Card>();
    List<Card> m_LComputerList=new List<Card>();
    List<Card> m_RComputerList=new List<Card>();

    public  Transform PlayPoint;
    public  Transform ComputerRightPoint;
    public  Transform ComputerLeftPoint;
    public List<Card> PlayerList
    {
        get
        {
            return m_PlayerList;
        }
    }

    public List<Card> LComputerList
    {
        get
        {
            return m_LComputerList;
        }
    }

    public List<Card> RComputerList
    {
        get
        {
            return m_RComputerList;
        }
    }
    /// <summary>
    /// 设置地主牌的显示
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    public void SetCard(Card card,int index)
    {
        m_DeskUI.SetShowCard(card,index);
    }

    /// <summary>
    /// 生成手牌
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    /// <param name="isSelected"></param>
    /// <param name="type"></param>
    public void CreateCardUI(Card card, int index, bool isSelected,ShowPoint type)
    {
        //
        GameObject temp = LeanPool.Spawn(m_Prefab);

        temp.transform.localScale = new Vector3(1,1,1);

        CardUI cardUI = temp.GetComponent<CardUI>();
        cardUI.Card = card;
        cardUI.IsSelected = isSelected;

        if (type == ShowPoint.Player)
        {
            cardUI.SetCardPosition(PlayPoint, index);
        }
        else if (type == ShowPoint.ComputerRight)
        {
            cardUI.SetCardPosition(ComputerRightPoint, index);
        }
        else if (type == ShowPoint.ComputerLeft)
        {
            cardUI.SetCardPosition(ComputerLeftPoint, index);
        }
        else if (type == ShowPoint.Desk)
        {
            cardUI.SetCardPosition(CreatePoint, index);
        }

    }

    /// <summary>
    /// 添加卡牌
    /// </summary>
    /// <param name="card"></param>
    /// <param name="isSelected"></param>
    /// <param name="type"></param>
    public void AddCard(Card card, bool isSelected,ShowPoint type)
    {
        switch (type)
        {
            case ShowPoint.Desk:
                Cards.Add(card);
                card.BelongTo=m_characterType;
                CreateCardUI(card,Cards.Count-1,isSelected,type);
                break;
            case ShowPoint.Player:
                m_PlayerList .Add(card);
                card.BelongTo = m_characterType;
                CreateCardUI(card, m_PlayerList.Count - 1, isSelected, type);
                break;
            case ShowPoint.ComputerRight:
                m_RComputerList.Add(card);
                card.BelongTo = m_characterType;
                CreateCardUI(card, m_RComputerList.Count - 1, isSelected, type);
                break;
            case ShowPoint.ComputerLeft:
                m_LComputerList.Add(card);
                card.BelongTo = m_characterType;
                CreateCardUI(card, m_LComputerList.Count - 1, isSelected, type);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 清空卡牌
    /// </summary>
    /// <param name="type"></param>
    public void ClearList(ShowPoint type)
    {
        switch (type)
        {
            case ShowPoint.Desk:
                Cards.Clear();
                CardUI[] cardUI = CreatePoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < cardUI.Length; i++)
                {
                    cardUI[i].Destory();
                }
                break;
            case ShowPoint.Player:
                m_PlayerList.Clear();
                CardUI[] PcardUI = PlayPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < PcardUI.Length; i++)
                {
                    PcardUI[i].Destory();
                }
                break;
            case ShowPoint.ComputerRight:
                m_RComputerList.Clear();
                CardUI[] RcardUI = ComputerRightPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < RcardUI.Length; i++)
                {
                    RcardUI[i].Destory();
                }
                break;
            case ShowPoint.ComputerLeft:
                m_LComputerList.Clear();
                CardUI[] LcardUI = ComputerLeftPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < LcardUI.Length; i++)
                {
                    LcardUI[i].Destory();
                }
                break;
            default:
                break;
        }

    }
}
