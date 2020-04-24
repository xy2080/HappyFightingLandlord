using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeskUI : MonoBehaviour {

    Transform m_showPoint;//地主牌的父物体的Transform
   
    /// <summary>
    /// 获取地主牌的父物体的
    /// </summary>
    public Transform ShowPoint;
    /// <summary>
    /// 得到CanvasGroup组件
    /// </summary>
    public CanvasGroup ShowCanvas
    {
        get {
            return ShowPoint.GetComponent<CanvasGroup>();
        }
    }
    /// <summary>
    /// 设置CanvasGroup组件的Alpha值管理地主牌的显示
    /// </summary>
    public void SetAlpha(int num)
    {
        ShowCanvas.alpha = num;
    }
    /// <summary>
    /// 更新地主牌
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    public void SetShowCard(Card card,int index)
    {
        Image[] images = ShowPoint.GetComponentsInChildren<Image>();
        images[index].sprite= Resources.Load<Sprite>("Pokers"+card.CardName);
        //显示地主牌
        SetAlpha(0);
    }
}
