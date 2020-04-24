using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterUI : MonoBehaviour {

   public Image m_imgHead;//头像
   public  Text m_textScore;//积分
   public  Text m_textRemain;//剩余手牌
    /// <summary>
    /// 依据身份设置玩家的头像显示
    /// </summary>
    /// <param name="identity"></param>
    public void SetIdentity(Identity identity)
    {
        switch (identity)
        {
            case Identity.Farmer:
                m_imgHead.sprite = Resources.Load<Sprite>("Pokers/Role_Farmer");
                break;
            case Identity.Landlord:
                m_imgHead.sprite = Resources.Load<Sprite>("Pokers/Role_Landlord");
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 更新玩家当前的积分显示
    /// </summary>
    /// <param name="num"></param>
    public void SetIntegration(int num)
    {
        m_textScore.text=num.ToString();
    }
    /// <summary>
    /// 更新玩家的剩余卡牌数
    /// </summary>
    /// <param name="num"></param>
    public void SetRemainCard(int num)
    {
        m_textRemain.text = num.ToString();
    }
}
