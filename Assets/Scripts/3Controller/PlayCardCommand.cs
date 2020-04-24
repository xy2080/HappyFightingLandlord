using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PlayCardCommand:EventCommand
{
    [Inject]
    public RoundModel1 round { set; get; }

    [Inject]
    public IntegrationModel model { set; get; }
    public override void Execute()
    {
       

        PlayCardArgs args = (PlayCardArgs)evt.data;
       


        if (args.characterType==CharacterType.Player)
        {
            if(args.cardType==round.CurrentCardType&&args.length==round.CurrentLength
                &&args.weight>round.CurrentWeight)//同类型牌，玩家是否大于上家
            {
                dispatcher.Dispatch(ViewEvent.SuccessedPlayCard);
            }
            else if (args.cardType == CardType.Boom && round.CurrentCardType!=CardType.Boom)//玩家是炸弹，而上家不是炸弹
            {
                dispatcher.Dispatch(ViewEvent.SuccessedPlayCard);
            }
            else if (args.cardType == CardType.JokerBoom)//玩家出牌王炸
            {
                dispatcher.Dispatch(ViewEvent.SuccessedPlayCard);
            }
            else if (args.characterType==round.BiggestCharacter)//玩家就是最大的牌，可以自由出牌
            {
                dispatcher.Dispatch(ViewEvent.SuccessedPlayCard);
            }
            else
            {
                UnityEngine.Debug.Log("请重新选择");
                return;
            }
        }
        //音效
        if(args.characterType!=round.BiggestCharacter&&
            args.cardType!=CardType.Single&&
            args.cardType!=CardType.Double
            )
        {
            Sound.Instance.PlayEffect(Consts.PlayCard[UnityEngine.Random.Range(0, 3)]);//音效
        }
        else
        {
            //播放对应牌型的音效
            switch (args.cardType)
            {
                case CardType.None:
                    break;
                case CardType.Single:
                    Sound.Instance.PlayEffect(Consts.Single[args.weight]);
                    break;
                case CardType.Double:
                    Sound.Instance.PlayEffect(Consts.Double[args.weight/2]);
                    break;
                case CardType.Straight:
                    Sound.Instance.PlayEffect(Consts.Straight);
                    break;
                case CardType.DoubleStraight:
                    Sound.Instance.PlayEffect(Consts.DoubleStraight);
                    break;
                case CardType.TripleStraight:
                    Sound.Instance.PlayEffect(Consts.TripleStraight);
                    break;
                case CardType.Three:
                    Sound.Instance.PlayEffect(Consts.Three);
                    break;
                case CardType.ThreeAndOne:
                    Sound.Instance.PlayEffect(Consts.ThreeAndOne);
                    break;
                case CardType.ThreeAndTwo:
                    Sound.Instance.PlayEffect(Consts.ThreeAndTwo);
                    break;
                case CardType.Boom:
                    Sound.Instance.PlayEffect(Consts.Boom);
                    break;
                case CardType.JokerBoom:
                    Sound.Instance.PlayEffect(Consts.JokerBoom);
                    break;
                default:
                    break;
            }

        }

        //玩家出牌成功更新当前的数据
        round.BiggestCharacter = args.characterType;
        round.CurrentCharacter = args.characterType;
        round.CurrentWeight = args.weight;
        round.CurrentCardType = args.cardType;
        round.CurrentLength = args.length;

        //炸弹或王炸，积分翻倍
        if (args.cardType == CardType.Boom)
        {
            model.m_Mulitiple *= 2;
        }
        else if (args.cardType == CardType.JokerBoom)
        {
            model.m_Mulitiple *= 4;
        }

        //切换到下家出牌

        round.Turn();

    }
}

