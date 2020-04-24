using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationModel
{
    public int m_BasePoint;//每局游戏赌的基础分数
    public int m_Mulitiple;//翻倍数
    /// <summary>
    /// 最后结束时，赌的分数
    /// </summary>
    public int Result
    {
        get
        {
            return (m_BasePoint*m_Mulitiple);
        }
    }

    private int m_playerIntegration;  //玩家的分数
    private int m_computerRightIntegration;   //右边电脑的分数
    private int m_computerLeftIntegration;    //左边电脑的分数
    /// <summary>
    /// 方法组中限制玩家的分数最小不低于0
    /// </summary>
    public int PlayerIntegration
    {
        get
        {
            return m_playerIntegration;
        }
        set
        {
            if(value<0)
            {
                m_playerIntegration = 0;
            }
            else
            m_playerIntegration = value;
        }
    }
    public int ComputerRightIntegration
    {
        get
        {
            return m_computerRightIntegration;
        }

        set
        {
            if (value < 0)
            {
                m_computerRightIntegration = 0;
            }
            else
            m_computerRightIntegration = value;
        }
    }
    public int ComputerLeftIntegration
    {
        get
        {
            return m_computerLeftIntegration;
        }

        set
        {
            if(value<0)
            {
                m_computerLeftIntegration = 0;
            }
            else
            m_computerLeftIntegration = value;
        }
    }

    /// <summary>
    /// 初始化函数
    /// </summary>
    public void InitIntegration()
    {
        m_Mulitiple = 1;
        m_BasePoint = 100;
        PlayerIntegration = 1500;
        ComputerRightIntegration = 1500;
        ComputerLeftIntegration = 1500;
    }

}
