using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntegrationView : View {
    public Button m_Deal;

    public Button m_Grab;
    public Button m_DisGrab;

 
    public Button m_Play;
    public Button m_Pass;
	
    /// <summary>
    /// 隐藏全部交互按钮
    /// </summary>
    public void DeactiveAll()
    {
        m_Play.gameObject.SetActive(false);

        m_Grab.gameObject.SetActive(false);
        m_DisGrab.gameObject.SetActive(false);

        m_Deal.gameObject.SetActive(false);
        m_Pass.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示发牌按钮
    /// </summary>
    public void  ActivePlay()
    {
        m_Play.gameObject.SetActive(false);
        m_Grab.gameObject.SetActive(false);
        m_DisGrab.gameObject.SetActive(false);
        m_Deal.gameObject.SetActive(true);
        m_Pass.gameObject.SetActive(false);
    }
  
    /// <summary>
    /// 显示是否抢地主面板
    /// </summary>
    public void ActiveGrab()
    {
        m_Play.gameObject.SetActive(false);
        m_Grab.gameObject.SetActive(true);
        m_DisGrab.gameObject.SetActive(true);
        m_Deal.gameObject.SetActive(false);
        m_Pass.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示是否出牌的按钮
    /// </summary>
    public void ActiveDeal(bool isActive)
    {
        m_Play.gameObject.SetActive(true);
        m_Grab.gameObject.SetActive(false);
        m_DisGrab.gameObject.SetActive(false);
        m_Deal.gameObject.SetActive(false);
        m_Pass.gameObject.SetActive(true);
        m_Pass.interactable = isActive;//设置为false时，则表示玩家必须出牌
    }

}
