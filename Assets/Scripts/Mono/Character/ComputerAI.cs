using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 电脑玩家智能出牌
/// </summary>
public class ComputerAI : MonoBehaviour {
    //要出的牌的集合
    public List<Card> m_DealCardsList = new List<Card>();
    //当前牌的类型
    public CardType m_cardType;
    public void SmartSelectCards(List<Card> cards,CardType cardType,int weight,int length,bool isBiggest)
    {
     //   m_cardType = isBiggest ? CardType.None : cardType;
        if(isBiggest)
        {
            m_cardType = CardType.None;
        }
        else
        {
            m_cardType = cardType;
        }
        m_DealCardsList.Clear();
        switch (m_cardType)
        {
            case CardType.None:
                //随机出牌
              m_DealCardsList= SelectSmallLastCard(cards);
                break;
            case CardType.Single:
                //单牌
                m_DealCardsList = SelectSingle(cards,weight);
                break;
            case CardType.Double:
                //对儿
                m_DealCardsList = SelectDouble(cards, weight);
               
                break;
            case CardType.Straight:
                //顺子
                m_DealCardsList = SelectStraight(cards, weight,length);
                if(m_DealCardsList.Count==0)
                {
                    //顺子压不上找炸弹，更改牌的类型
                    m_DealCardsList = SelectBoom(cards,weight);
                    m_cardType= CardType.Boom;
                    //找王炸
                    if (m_DealCardsList.Count == 0)
                    {
                        m_DealCardsList = SelectJoker(cards);
                        m_cardType = CardType.JokerBoom;
                    }
                }
                break;
            case CardType.DoubleStraight:
                //双顺
                m_DealCardsList = SelectDoubleStraight(cards, weight, length);
                if (m_DealCardsList.Count == 0)
                {
                    //双顺压不上找炸弹，更改牌的类型
                    m_DealCardsList = SelectBoom(cards, weight);
                    m_cardType = CardType.Boom;
                    //找王炸
                    if (m_DealCardsList.Count == 0)
                    {
                        m_DealCardsList = SelectJoker(cards);
                        m_cardType = CardType.JokerBoom;
                    }
                }
                break;
            case CardType.TripleStraight:
                //飞机            
                    //飞机压不上找炸弹，更改牌的类型
                    m_DealCardsList = SelectBoom(cards, weight);
                    m_cardType = CardType.Boom;
                    //找王炸
                    if (m_DealCardsList.Count == 0)
                    {
                        m_DealCardsList = SelectJoker(cards);
                        m_cardType = CardType.JokerBoom;
                     }
                break;
            case CardType.Three:
                //三不带
                m_DealCardsList = SelectThree(cards,weight);
                break;
            case CardType.ThreeAndOne:
                //三带一
                m_DealCardsList = SelectThreeAndOne(cards, weight);
                break;
            case CardType.ThreeAndTwo:
                //三带二
                m_DealCardsList = SelectThreeAndTwo(cards, weight);
                break;
            case CardType.Boom:
                //炸弹
                m_DealCardsList = SelectBoom(cards, weight);
                //找王炸
                if (m_DealCardsList.Count == 0)
                {
                    m_DealCardsList = SelectJoker(cards);
                    m_cardType = CardType.JokerBoom;
                }
                break;
            case CardType.JokerBoom:
                break;
            default:
                break;
        }
   
    }
    /// <summary>
    /// 选择合适的牌出
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public List<Card> SelectSmallLastCard(List<Card> cards)
    {
        List<Card> selectedCard = new List<Card>();
        //先找顺
        for (int i = 12; i >=5; i--)
        {
            selectedCard = SelectStraight(cards,-1,i);
            if(selectedCard.Count>0)
            {
                m_cardType = CardType.Straight;
                break;
            }
        }
        //找三带二
        if(selectedCard.Count==0)
        {
            for (int i = 0; i < 36; i+=3)
            {
                selectedCard = SelectThreeAndTwo(cards,i-1);
                if(selectedCard.Count>0)
                {
                    m_cardType = CardType.ThreeAndTwo;
                    break;
                }
            }
        }
        //找三带一
        if (selectedCard.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                selectedCard = SelectThreeAndOne(cards, i - 1);
                if (selectedCard.Count > 0)
                {
                    m_cardType = CardType.ThreeAndOne;
                    break;
                }
            }
        }
        //三不带
        if (selectedCard.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                selectedCard = SelectThree(cards, i - 1);
                if (selectedCard.Count > 0)
                {
                    m_cardType = CardType.Three;
                    break;
                }
            }
        }
        //对
        if (selectedCard.Count == 0)
        {
            for (int i = 0; i < 24; i += 2)
            {
                selectedCard = SelectDouble(cards, i - 1);
                if (selectedCard.Count > 0)
                {
                    m_cardType = CardType.Double;
                    break;
                }
            }
        }
        //单
        if (selectedCard.Count == 0)
        {
            selectedCard = SelectSingle(cards,-1);
            m_cardType = CardType.Single;
        }
        return selectedCard;
    }

    /// <summary>
    /// 选择一个单
    /// </summary>
    /// <param name="cards">排序的手牌</param>
    /// <param name="weight">已出牌的权值大小</param>
    /// <returns></returns>
    public List<Card> SelectSingle(List<Card> cards,int weight)
    {
        List<Card> selectedCard = new List<Card>();
        foreach (var item in cards)
        {
            if((int)item.WeightValue>weight)
            {
                selectedCard.Add(item);
                break;
            }
        }
        return selectedCard;
    }
    /// <summary>
    /// 找到一个对
    /// </summary>
    /// <param name="cards">已有的手牌</param>
    /// <param name="weight">上家的牌的权值大小</param>
    /// <returns></returns>
    public List<Card> SelectDouble(List<Card> cards, int weight)
    {
        List<Card> selectedCard = new List<Card>();

        for (int i = 0; i < cards.Count-1; i++)
        {
            if (cards[i].WeightValue == cards[i + 1].WeightValue)
            {
                int num = (int)cards[i].WeightValue + (int)cards[i + 1].WeightValue;
                if (num > weight)
                {
                    selectedCard.Add(cards[i]);
                    selectedCard.Add(cards[i + 1]);
                    break;
                }
            }
        }
        return selectedCard;
    }
    /// <summary>
    /// 顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public List<Card> SelectStraight(List<Card> cards,int weight,int length)
    {
        #region//思路讲解
        //首先定义counter记录我们已经找到的顺的长度，定义Array集合保存已经找到的牌的下标
        //顺子的比较，比较起始牌的大小以及同长度
        //首先遍历手牌找到比上家顺子大的起始牌，然后将其counter初始化，清空下标集合，添加当前下标，然后从
        //当前元素的下一个开始遍历之后的元素，先判断是否小于A,小于则继续判断下一元素减去上一元素是否等于已经记录的顺子的长度，若是则
        //长度+1，记录下标到集合，判断当前counter是否等于上家的长度，是跳出循环，否继续
        //在起始牌的循环下面判断是否已经找到顺子，若没有则继续找大于上家的起始，初始化Counter=1,清空下标集合，添加起始牌牌的下标
        #endregion
        List<Card> selectedCard = new List<Card>();
        int counter = 1;//顺子的长度
        List<int> Array = new List<int>();//选中的牌的下标
        for (int i = 0; i < cards.Count-4; i++)
        {
            if((int)cards[i].WeightValue>weight)
            {
                counter = 1;
                Array.Clear();
                Array.Add(i);
                for (int j = i+1; j < cards.Count; j++)
                {
                    if((int)cards[j].WeightValue<=(int)Weight.One)
                    {
                        if((int)cards[j].WeightValue-(int)cards[i].WeightValue==counter)
                        {
                            counter++;
                            Array.Add(j);
                        }
                    }
                    else
                    {
                        break;
                    }
                    if(counter==length)
                    {
                        break;
                    }
                    
                }
            }
            if (counter == length)
            {
                break;
            }
        }
        if (counter == length)
        {
            foreach (int item in Array)
            {
                selectedCard.Add(cards[item]);
            }           
        }
        Array.Clear();
        return selectedCard;
    }
    /// <summary>
    /// 双顺
    /// </summary>
    /// <param name="cards">手牌</param>
    /// <param name="weight">上家的拍的权值</param>
    /// <param name="length">上家牌的长度</param>
    /// <returns></returns>
    public List<Card> SelectDoubleStraight(List<Card> cards,int weight,int length)
    {
        List<Card> selectedCard = new List<Card>();
        int counter = 0;//顺子的长度
        List<int> Array = new List<int>();//选中的牌的下标
        for (int i = 0; i < cards.Count - 4; i++)
        {
            if ((int)cards[i].WeightValue > weight)
            {
                counter = 0;
                Array.Clear();
                Array.Add(i);
                int temp = 0;
                for (int j = i + 1; j < cards.Count; j++)
                {
                    if ((int)cards[j].WeightValue <= (int)Weight.One)
                    {
                        if ((int)cards[j].WeightValue - (int)cards[i].WeightValue == counter)
                        {
                            temp++;
                            if (temp % 2 == 1)
                            {
                                counter++;
                            }
                            Array.Add(j);
                        }
                    }
                    else
                    {
                        break;
                    }
                    if (counter*2 == length)
                    {
                        break;
                    }

                }
            }
            if (counter*2 == length)
            {
                break;
            }
        }
        if (counter*2== length)
        {
            foreach (int item in Array)
            {
                selectedCard.Add(cards[item]);
            }
        }
        Array.Clear();
        return selectedCard;
    }

    /// <summary>
    /// 三不带
    /// </summary>
    /// <param name="cards">手牌</param>
    /// <param name="weight">上家的牌的大小</param>
    /// <returns></returns>
    public List<Card> SelectThree(List<Card> cards, int weight)
    {
        List<Card> selectedCard = new List<Card>();

        for (int i = 0; i < cards.Count - 2; i++)
        {
            if (cards[i].WeightValue == cards[i + 1].WeightValue&& cards[i + 1].WeightValue== cards[i + 2].WeightValue)
            {
                int num = (int)cards[i].WeightValue + (int)cards[i + 1].WeightValue+(int)cards[i + 2].WeightValue;
                if (num > weight)
                {
                    selectedCard.Add(cards[i]);
                    selectedCard.Add(cards[i + 1]);
                    selectedCard.Add(cards[i + 2]);
                    break;
                }
            }
        }
        return selectedCard;
    }
    /// <summary>
    /// 三带一
    /// </summary>
    /// <param name="cards">手牌</param>
    /// <param name="weight">上家牌牌的权值</param>
    /// <returns></returns>
    public List<Card> SelectThreeAndOne(List<Card> cards, int weight)
    {
        List<Card> selectedCard = new List<Card>();

        List<Card> Three = SelectThree(cards, weight);

        if (Three.Count > 0)
        {
            foreach (var item in Three)
            {
                cards.Remove(item);
            }
            List<Card> single =SelectSingle(cards, -1);
            if (single.Count > 0)
            {
                selectedCard.AddRange(Three);
                selectedCard.AddRange(single);
            }
        }
        return selectedCard;
    }
    /// <summary>
    /// 三带二
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public List<Card> SelectThreeAndTwo(List<Card> cards, int weight)
    {
        List<Card> selectedCard = new List<Card>();

        List<Card> Three = SelectThree(cards,weight);
        
        if(Three.Count>0)
        {
            foreach (var item in Three)
            {
                cards.Remove(item);
            }
            List<Card> Two =SelectDouble(cards,-1);
            if(Two.Count>0)
            {
                selectedCard.AddRange(Three);
                selectedCard.AddRange(Two);
            }
        }
        return selectedCard;
    }
    /// <summary>
    /// 炸弹
    /// </summary>
    /// <param name="cards">手牌</param>
    /// <param name="weight">上家炸弹权值大小</param>
    /// <returns></returns>
    public List<Card> SelectBoom(List<Card> cards, int weight)
    {
        List<Card> selectedCard = new List<Card>();

        for (int i = 0; i < cards.Count - 2; i++)
        {
            if (cards[i].WeightValue == cards[i + 1].WeightValue && cards[i + 1].WeightValue == cards[i + 2].WeightValue &&
                cards[i + 2].WeightValue == cards[i + 3].WeightValue)
            {
                int num = (int)cards[i].WeightValue + (int)cards[i + 1].WeightValue + (int)cards[i + 2].WeightValue + (int)cards[i + 3].WeightValue;
                if (num > weight)
                {
                    selectedCard.Add(cards[i]);
                    selectedCard.Add(cards[i + 1]);
                    selectedCard.Add(cards[i + 2]);
                    selectedCard.Add(cards[i + 3]);
                    break;
                }
            }
        }
        return selectedCard;
    }

    /// <summary>
    /// 王炸
    /// </summary>
    /// <param name="cards">手牌</param>
    /// <returns></returns>
    public List<Card> SelectJoker(List<Card> cards)
    {
        List<Card> selectedCard = new List<Card>();
        for (int i = 0; i < cards.Count - 1; i++)
        {
            if (cards[i].WeightValue == Weight.SJoker && cards[i + 1].WeightValue == Weight.LJoker)
            {
                selectedCard.Add(cards[i]);
                selectedCard.Add(cards[i + 1]);
            }
        }
        return selectedCard;
    }
}
