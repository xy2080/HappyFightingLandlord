using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 积分数据类
/// </summary>
[Serializable]
public class GameData
{
    public int m_playerIntegration;  //玩家的分数
    public int m_computerRightIntegration;   //右边电脑的分数
    public int m_computerLeftIntegration;    //左边电脑的分数
}
