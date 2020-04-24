using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
public class CardUI : MonoBehaviour {

    
    Card m_Card;
    public Image m_imgCard;
    bool m_isSelected=false;
    public  LearnButton m_Btn;
    public Card Card
    {
        get
        {
            return m_Card;
        }
        set
        {
            m_Card = value;
            SetImage();
        }
    }
    /// <summary>
    /// 卡牌的属性已经生成，设置图片的显示
    /// </summary>
    public void SetImage()
    {
        if (m_Card.BelongTo == CharacterType.Player || m_Card.BelongTo == CharacterType.Desk)
        {
            if(m_imgCard==null)
            {
                Debug.Log("显示失败");
            }
            //若纸牌归属于玩家或桌面，则正面显示出来
            m_imgCard.sprite = Resources.Load<Sprite>("Pokers/"+m_Card.CardName);
        }
        else
        {
            //电脑玩家的纸牌，则显示背面
            m_imgCard.sprite = Resources.Load<Sprite>("Pokers/FixedBack");
        }
    }
    /// <summary>
    /// 地主牌的显示
    /// </summary>
    public void SetLandCardImage()
    {
        m_imgCard.sprite = Resources.Load<Sprite>("Pokers/CardBack");
    }
    /// <summary>
    /// 设置卡牌的点击
    /// </summary>
    public bool IsSelected
    {
        get
        {
            return m_isSelected;
        }

        set
        {
            //若不是玩家的牌不能点击，且每一次的值必须不一样
            if (m_Card.BelongTo != CharacterType.Player || m_isSelected == value)
            {
                return;
            }
            if (value)
            {
                transform.localPosition += Vector3.up * 10;
            }
            else
            {
                transform.localPosition -= Vector3.up * 10;
            }
            m_isSelected = value;
        }
    }
    /// <summary>
    /// 设置卡牌的显示的父物体以及排序
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="index"></param>
    public void  SetCardPosition(Transform parent,int index)
    {
        transform.SetParent(parent);//放在父物体下
        transform.SetSiblingIndex(index);//索引父物体下的所有兄弟姐妹

        if(m_Card.BelongTo==CharacterType.Player||m_Card.BelongTo==CharacterType.Desk)
        {
            transform.localPosition = Vector3.right * 25 * index;//往右偏移10
            //重新排列，防止卡牌被还原
            if (m_isSelected)
            {
                transform.localPosition += Vector3.up * 10;
            }
        }
        else if(m_Card.BelongTo==CharacterType.ComputerLeft)
        {
            transform.localPosition = -Vector3.up * 8*index + Vector3.left * 8 * index;
        }
        else if ( m_Card.BelongTo == CharacterType.ComputerRight)
        {
            transform.localPosition = -Vector3.up * 6 * index + Vector3.left * 6* index;
        }

    }
    /// <summary>
    /// 初始化卡牌数据
    /// </summary>
    public void OnSpawn()
    {
        m_imgCard = GetComponent<Image>();
        
        //为LearnButton脚本的委托注册事件
        m_Btn.PressedBtn += M_Btn_PressedBtn;
        m_Btn.HighlightedBtn += M_Btn_HighlightedBtn;
    }
        
    private void M_Btn_HighlightedBtn()
    {
        if(Input.GetMouseButton(1))
        {
            OnBtnClick();
        }
    }

    private void M_Btn_PressedBtn()
    {
        OnBtnClick();
    }

    /// <summary>
    /// 回收卡牌脏数据
    /// </summary>
    public void  OnDespawn()
    {
        m_Btn.onClick.RemoveListener(OnBtnClick);
        m_isSelected = false;
        m_imgCard = null;
        m_Card = null;
    }
    private void OnBtnClick()
    {
        if(m_Card.BelongTo==CharacterType.Player)
        {
            IsSelected = !IsSelected;
            Sound.Instance.PlayEffect(Consts.Select);//音效
        }
    }
    /// <summary>
    /// 回收对象
    /// </summary>
    public void Destory()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
