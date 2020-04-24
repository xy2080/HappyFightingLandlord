using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class CharacterBase : MonoBehaviour {
    public CharacterType m_characterType;//设置当前的持有者

    public GameObject m_Prefab;
    List<Card> m_cards = new List<Card>();

    Transform m_CreatePoint;
    /// <summary>
    /// 父物体的Transform
    /// </summary>
    public Transform CreatePoint
    {
        get
        {
            if(m_CreatePoint==null)
            {
               m_CreatePoint= transform.Find("img_CreatePoint").transform;
            }
            return m_CreatePoint;
        }
    }
 
    /// <summary>
    /// 卡牌集合属性
    /// </summary>
    public List<Card> Cards
    {
        get
        {
            return m_cards;
        }
        set
        {
            m_cards = value;
        }
    }
  
    /// <summary>
    /// 判断是否有剩余
    /// </summary>
    public bool IHasCard
    {
        get
        {
            return m_cards.Count != 0;
        }
    }
  
    /// <summary>
    /// 剩余牌数
    /// </summary>
    public int RemainCardCount
    {
        get
        {
            return m_cards.Count;
        }
    }

    /// <summary>
    /// 添加牌
    /// </summary>
    /// <param name="card">要添加的牌</param>
    /// <param name="isSelected">是否上移</param>
    public virtual void AddCard(Card card,bool isSelected)
    {
        m_cards.Add(card);
        card.BelongTo = m_characterType;
        CreateCardUI(card,m_cards.Count-1,isSelected);
    }
 
    /// <summary>
    ///  创建卡牌
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    /// <param name="isSelected"></param>
    public void  CreateCardUI(Card card,int index,bool isSelected)
    {
        //
        GameObject temp = LeanPool.Spawn(m_Prefab);
      
        CardUI cardUI = temp.GetComponent<CardUI>();

        cardUI.Card = card;
        cardUI.IsSelected = isSelected;
        cardUI.SetCardPosition(CreatePoint,index);
        cardUI.OnSpawn();
       
    }
  
    /// <summary>
    /// 排序
    /// </summary>
    public void CardsSort(bool asc)
    {
        //排序牌的大小
        ToolsManager.SortList(m_cards,asc);
        CardUISort(m_cards);
    }
   
    /// <summary>
    /// 排序卡牌在UI的显示
    /// </summary>
    public void CardUISort(List<Card> cards)
    {
        CardUI[] cardUIs = m_CreatePoint.GetComponentsInChildren<CardUI>();
        //遍历集合与数组，若同下标下的元素相同，则将元素的位置设置偏移
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cardUIs.Length; j++)
            {
                if(cards[i]==cardUIs[j].Card)
                {
                    cardUIs[j].SetCardPosition(CreatePoint,i);
                }
            }
        }
    }

    /// <summary>
    /// 发出一张牌
    /// </summary>
    /// <returns></returns>
    public virtual Card  DealCard()
    {
        Card card = m_cards[m_cards.Count - 1];
        m_cards.Remove(card);
        return card;
    }
}
