using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 生成一副牌
/// </summary>
public class CardModel  {

    CharacterType m_character = CharacterType.Library;//新生成的一副牌默认放在牌库
    Queue<Card> m_Queue = new Queue<Card>();//新建队列
	public void Init()
    {
        //生成52张牌
        for (int color = 1; color < 5; color++)//色号枚举的权值
        {
            for (int weight = 0; weight < 13; weight++)
            {
                Colors c = (Colors)color;//将int类型的值强转为色号枚举类型
                Weight w = (Weight)weight;//将int类型的值强转大小枚举类型
                string cardName = c.ToString() + w.ToString();
                Card card = new Card(cardName,w,c,m_character);
                m_Queue.Enqueue(card);//入队列
            }
        }
        //手动生成大小王
        Card lJoker = new Card("LJoker",Weight.LJoker,Colors.None,m_character);
        Card sJoker = new Card("SJoker", Weight.SJoker, Colors.None, m_character);
        m_Queue.Enqueue(sJoker);
        m_Queue.Enqueue(lJoker);
    }
  
    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
        //遍历队列，随机生成一个下标，作为当前遍历到的卡牌的在集合中的下标

        List<Card> m_CardList = new List<Card>();
        //遍历队列，随机一个的标且对应集合中的元素为空，将队列的元素放入集合
        foreach (var item in m_Queue)
        {
            int index = UnityEngine.Random.Range(0,m_CardList.Count-1);
            m_CardList.Insert(index,item);
        }
        m_Queue.Clear();
        //遍历集合，放入队列，此时队列里的牌的顺序已经被打乱
        foreach (var item in m_CardList)
        {
            m_Queue.Enqueue(item);
        }
        m_CardList.Clear();

    }
 
    /// <summary>
    /// 发牌
    /// </summary>
    /// <param name="sendto">牌的归属</param>
    public Card DealCard(CharacterType sendto)
    {
        Card card = m_Queue.Dequeue();
        card.BelongTo = sendto;
        return card;
    }
   
    /// <summary>
    /// 牌库剩余的牌数
    /// </summary>
    public int LastCardCount
    {
        get
        {
            return m_Queue.Count;
        }
    }
}
