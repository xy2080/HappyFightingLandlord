using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 提供方法判断牌是否能出
/// </summary>
public static class Rulers
{
    public static bool CanPOp(List<Card> cards,out CardType type )
    {
        type = CardType.None;
        bool isPop = false;
        switch (cards.Count)
        {
            case 1:
                if(IsSingle(cards))
                {
                    type = CardType.Single;
                    isPop = true;
                }//判断是不是单牌
                break;
            case 2:
                if(IsBoom(cards))
                {
                    type = CardType.JokerBoom;
                    isPop = true;
                }//王炸
                else if(IsDouble(cards))
                {
                    type = CardType.Double;
                    isPop = true;
                }//双顺
                break;
            case 3:
                if(IsThree(cards))
                {
                    type = CardType.Three;
                    isPop = true;
                }//三不带
                break;
            case 4:
                if (IsBoom(cards))
                {
                    type = CardType.Boom;
                    isPop = true;
                }//炸弹
                else if ( IsThreeAndOne(cards))
                {
                    type = CardType.ThreeAndOne;
                    isPop = true;
                }//三带一
                break;
            case 5:
                if (IsThreeAndTwo(cards))
                {
                    type = CardType.ThreeAndTwo;
                    isPop = true;
                }//三带二
                else if (IsStraight(cards))
                {
                    type = CardType.Straight;
                    isPop = true;
                }//顺子
                break;
            case 6:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    isPop = true;
                }//双顺
                else if (IsStraight(cards))
                {
                    type = CardType.Straight;
                    isPop = true;
                }//顺子
                else if (IsTripleStraight(cards))
                {
                    type = CardType.TripleStraight;
                    isPop = true;
                }//飞机
                break;
            case 7:
                if (IsStraight(cards))
                {
                    type = CardType.Straight;
                    isPop = true;
                }//顺子
                break;
            case 8:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    isPop = true;
                }//双顺
                else if (IsStraight(cards))
                {
                    type = CardType.Straight;
                    isPop = true;
                }//顺子
                break;
            case 9:
                if (IsStraight(cards))
                {
                    type = CardType.Straight;
                    isPop = true;
                }//顺子
                else if (IsTripleStraight(cards))
                {
                    type = CardType.TripleStraight;
                    isPop = true;
                }//飞机
                break;
            case 10:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    isPop = true;
                }//双顺
                else if (IsStraight(cards))
                {
                    type = CardType.Straight;
                    isPop = true;
                }//顺子
                break;
            case 11:
                if (IsStraight(cards))
                {
                    type = CardType.Straight;
                    isPop = true;
                }//顺子
                break;
            case 12:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    isPop = true;
                }//双顺
                else if (IsStraight(cards))
                {
                    type = CardType.Straight;
                    isPop = true;
                }//顺子
                else if (IsTripleStraight(cards))
                {
                    type = CardType.TripleStraight;
                    isPop = true;
                }//飞机
                break;
            case 13:
                break;
            case 14:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    isPop = true;
                }//双顺
                break;
            case 15:
                if (IsTripleStraight(cards))
                {
                    type = CardType.TripleStraight;
                    isPop = true;
                }//飞机
                break;
            case 16:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    isPop = true;
                }//双顺
                break;
            case 17:
                break;
            case 18:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    isPop = true;
                }//双顺
                else if (IsTripleStraight(cards))
                {
                    type = CardType.TripleStraight;
                    isPop = true;
                }//飞机
                break;
            case 19:
                break;
            case 20:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    isPop = true;
                }//双顺
                break;
            default:
                break;
        }
        return isPop;
    }
    /// <summary>
    /// 判断是否是一张牌
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsSingle(List<Card> cards)
    {
        return cards.Count == 1;
    }
  
    /// <summary>
    /// 判断出的牌是否是对
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsDouble(List<Card> cards)
    {
        if (cards.Count == 2)
        {
            if (cards[0].WeightValue == cards[1].WeightValue)
            {
                return true;
            }
        }
       return false;
        
    }
   
    /// <summary>
    /// 判断出的牌是否为顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsStraight(List<Card> cards)
    {
        //牌的数量5-12
        if(cards.Count<5||cards.Count>12)
        {
            return false;
        }
        //牌是连续的并且最大只能到A
        for (int i = 0; i < cards.Count-1; i++)
        {
            //不连续，非顺子
            if(cards[i+1].WeightValue-cards[i].WeightValue!=1)
            {
                return false;
            }
            //顺子只能到尖子
            if(cards[i].WeightValue>Weight.One||cards[i+1].WeightValue>Weight.One)
            {
                return false;
            }
        }
        return true;
    }
  
    /// <summary>
    /// 是否是双顺
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsDoubleStraight(List<Card> cards)
    {
        if(cards.Count<6||cards.Count%2!=0)
        {
            return false;
        }
        for (int i = 0; i < cards.Count-2; i+=2)
        {
            //相邻两个是对
            if(cards[i].WeightValue!=cards[i+1].WeightValue)
            {
                return false;
            }
            //奇数的下标的牌是递增的
            if(cards[i+2].WeightValue-cards[i].WeightValue!=1)
            {
                return false;
            }
            //最高只能到AA
            if(cards[i].WeightValue>Weight.One||cards[i+2].WeightValue>Weight.One)
            {
                return false;
            }
        }
        return true;
    }
  
    /// <summary>
    /// 飞机
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsTripleStraight(List<Card> cards)
    {
       if(cards.Count<6||cards.Count%3!=0)
        {
            return false;
        }
        for (int i = 0; i < cards.Count - 3; i += 3)
        {
            //相邻两个是对
            if (cards[i].WeightValue != cards[i + 1].WeightValue|| cards[i+2].WeightValue != cards[i + 1].WeightValue|| cards[i].WeightValue != cards[i + 2].WeightValue)
            {
                return false;
            }
            //奇数的下标的牌是递增的
            if (cards[i + 3].WeightValue - cards[i].WeightValue != 1)
            {
                return false;
            }
            //最高只能到AA
            if (cards[i].WeightValue > Weight.One || cards[i + 2].WeightValue > Weight.One)
            {
                return false;
            }
        }

        return true;
    }
   
    /// <summary>
    /// 三不带
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThree(List<Card> cards)
    {
        if(cards.Count!=3)
        {
            return false;
        }    
        //相邻两个是对
       if (cards[0].WeightValue == cards[1].WeightValue &&cards[2].WeightValue != cards[1].WeightValue)
       {
          return true;
       }
        return false;
    }
   
    /// <summary>
    /// 三带一
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThreeAndOne(List<Card> cards)
    {
        if(cards.Count!=4)
        {
            return false;
        }
        if (cards[0].WeightValue == cards[1].WeightValue && cards[2].WeightValue == cards[1].WeightValue )
        {
            return true;
        }
        else if(cards[2].WeightValue == cards[3].WeightValue && cards[2].WeightValue == cards[1].WeightValue)
        {
            return true;
        }

        return false;

    }
   
    /// <summary>
    /// 三带二
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThreeAndTwo(List<Card> cards)
    {
        if(cards.Count!=5)
        {
            return false;
        }
        if(cards[0].WeightValue==cards[1].WeightValue&& cards[2].WeightValue == cards[3].WeightValue && cards[3].WeightValue == cards[4].WeightValue)
        {
            return true;
        }
        else if(cards[3].WeightValue == cards[4].WeightValue && cards[0].WeightValue == cards[1].WeightValue && cards[1].WeightValue== cards[2].WeightValue)
        {
            return true;
        }
        return false;
    }
  
    /// <summary>
    /// 炸弹
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsBoom(List<Card> cards)
    {
        if(cards.Count!=4)
        {
            return false;
        }

        if(cards[0].WeightValue == cards[1].WeightValue&& cards[1].WeightValue == cards[2].WeightValue&& cards[2].WeightValue == cards[3].WeightValue)
        {
            return true;
        }
        return false;
    }
  
    /// <summary>
    /// 王炸
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsJoker(List<Card> cards)
    {
        if(cards.Count!=2)
        {
            return false;
        }
        if(cards[0].WeightValue==Weight.SJoker&&cards[1].WeightValue==Weight.LJoker)
        {
            return true;
        }
        return false;
    }
}
