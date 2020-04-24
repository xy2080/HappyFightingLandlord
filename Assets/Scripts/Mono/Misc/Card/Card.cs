using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌的属性类
/// </summary>
public class Card
{
  private  string cardName;//卡牌的名字 =色号+大小
  private  Weight weightValue;//权值
  private  Colors colorType;//色号
  private  CharacterType belongTo;//位置

    public string CardName
    {
        get
        {
            return cardName;
        }
    }
    public Weight WeightValue
    {
        get
        {
            return weightValue;
        }
    }
    public Colors ColorType
    {
        get
        {
            return colorType;
        }
    }
    public CharacterType BelongTo
    {
        get
        {
            return belongTo;
        }
        set
        {
            belongTo = value;
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="card">卡牌名字</param>
    /// <param name="weight">卡牌权值</param>
    /// <param name="colors">卡牌色号</param>
    /// <param name="type">归属位置</param>
    public Card(string card,Weight weight,Colors colors,CharacterType type)
    {
        cardName = card;
        weightValue = weight;
        colorType = colors;
        belongTo = type;
    }
}
