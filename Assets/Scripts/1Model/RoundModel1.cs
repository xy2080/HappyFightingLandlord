using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RoundModel1
{
    public bool isLand;
    public bool isWin;
    /// <summary>
    /// 定义两个委托，等待外界的注册
    /// </summary>
    public static event Action<bool> PlayerHandler;
    public static event Action<ComputerSmartArgs> ComputerHandler;
    int m_currentWeight;
    int m_currentLength;
    CardType m_currentCardType;//当前出的牌的类型
    CharacterType m_biggestCharacter;//当前出的牌的最大的玩家
    CharacterType m_currentCharacter;//当前轮到的玩家出牌
  
    /// <summary>
    /// 当前出的牌的最大权值
    /// </summary>
    public int CurrentWeight
    {
        get
        {
            return m_currentWeight;
        }

        set
        {
            m_currentWeight = value;
        }
    }
  
    /// <summary>
    /// 当前出的牌的数
    /// </summary>
    public int CurrentLength
    {
        get
        {
            return m_currentLength;
        }

        set
        {
            m_currentLength = value;
        }
    }
  
    /// <summary>
    /// 当前出的牌的类型
    /// </summary>
    public CardType CurrentCardType
    {
        get
        {
            return m_currentCardType;
        }

        set
        {
            m_currentCardType = value;
        }
    }
   
    /// <summary>
    ///最大的牌的玩家
    /// </summary>
    public CharacterType BiggestCharacter
    {
        get
        {
            return m_biggestCharacter;
        }

        set
        {
            m_biggestCharacter = value;
        }
    }
   
    /// <summary>
    /// 当前轮到出牌的玩家
    /// </summary>
    public CharacterType CurrentCharacter
    {
        get
        {
            return m_currentCharacter;
        }

        set
        {
            m_currentCharacter = value;
        }
    }

    /// <summary>
    /// 初始化函数
    /// </summary>
    public void Init()
    {
        m_currentWeight = -1;
        m_currentLength = -1;
        m_currentCardType = CardType.None;
        m_biggestCharacter = CharacterType.Desk;
        m_currentCharacter = CharacterType.Desk;
    }

    /// <summary>
    /// 出牌回合开始，由地主触发
    /// </summary>
    /// <param name="type"></param>
    public void Start(CharacterType type)
    {
        m_biggestCharacter = type;
        m_currentCharacter = type;
        BeginWith(type);
    }
    public void BeginWith(CharacterType type)
    {
        if(type==CharacterType.Player)
        {
            //玩家出牌
           if(PlayerHandler!=null)
            {
                PlayerHandler(BiggestCharacter != CharacterType.Player);
            }
        }
        else
        {
            //电脑出牌
           if(ComputerHandler!=null)
            {
                ComputerSmartArgs args = new ComputerSmartArgs() { cardType = CurrentCardType, weight = CurrentWeight, Length = CurrentLength, isBiggest = BiggestCharacter,currentCharacter=CurrentCharacter };
                ComputerHandler(args);
            }
        }
    }

    public void Turn()
    {
        m_currentCharacter++;
        if(m_currentCharacter==CharacterType.Desk)
        {
            m_currentCharacter = CharacterType.Player;
        }
        BeginWith(m_currentCharacter);
    }
}
