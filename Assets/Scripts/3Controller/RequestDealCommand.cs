using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestDealCommand : EventCommand {

    [Inject]
    public CardModel cardModel { set; get; }
    public DeskController deskController
    {
        get
        {
            return GameObject.FindObjectOfType<DeskController>();
        }
    }
    public override void Execute()
    {
        //执行发牌的操作

        //洗牌
        cardModel.Shuffle();

        //发牌
     deskController.StartCoroutine(DealCard());
    }
    IEnumerator DealCard()
    {
        CharacterType type = CharacterType.Player;
        for (int i = 0; i < 51; i++)
        {
            //判断是否发完一轮
            if(type==CharacterType.Desk||type==CharacterType.Library)
            {
                type = CharacterType.Player;
            }
            Deal(type);
            type++;
            yield return new WaitForSeconds(0.05f);
        }
        //发地主牌显示在桌面
        for (int i = 0; i < 3; i++)
        {
            Deal(CharacterType.Desk);
        }
        //反面显示
        CardUI[] cardUIs = deskController.transform.Find("img_CreatePoint").GetComponentsInChildren<CardUI>();
        foreach (var item in cardUIs)
        {
            item.SetLandCardImage();
        }
        //派发结束，发出发牌结束命令
        dispatcher.Dispatch(ViewEvent.CompleteDeal);
    }
    private void Deal(CharacterType m_type)
    {
        Card m_card= cardModel.DealCard(m_type);
        DealCardArgs deal = new DealCardArgs()
        {
            card = m_card,
            type = m_type,
            isSelected = false
        };
        dispatcher.Dispatch(ViewEvent.DealCard,deal);//封装发的牌的信息
    }
}
