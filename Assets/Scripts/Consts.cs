using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consts
{
    /// <summary>
    /// 数据存储的路径
    /// </summary>
    public static string DataPath = Application.dataPath + @"\Score.xml";

    //音效资源
    public static string Bg = "normal";
    //发牌音效
    public static string DealCard = "givecard";
    //抢地主
    public static string Grab = "qiangdizhu1";
    //不抢
    public static string DisGrab = "buqiang";
    //被选择
    public static string Select = "select";
    //Pass
    public static List<string> PassCard = new List<string>() { "buyao1","buyao2","buyao3"};
    //压住
    public static List<string> PlayCard = new List<string>() { "dani1", "dani2", "dani3" };
    //牌型的音效
    public static List<string> Single = new List<string>() { "3", "4", "5", "6", "7", "8", "9",
        "10","11","12","13","1","2","14","15"
    };
    public static List<string> Double = new List<string>() {"dui3", "dui4", "dui5", "dui6", "dui7", "dui8", "dui9",
        "dui10","dui11","dui12","dui13","dui1","dui2"
    };
    public static string Straight = "shunzi";
    public static string DoubleStraight = "liandui";
    public static string TripleStraight = "feiji";
    public static string Three = "sange";
    public static string ThreeAndOne = "sandaiyi";
    public static string ThreeAndTwo = "sandaiyidui";
    public static string Boom = "zhandan";
    public static string JokerBoom = "wangzha";
}
public enum PrefabType
{
    None,
    StartPanel,
    CharacterPanel,
    IntegrationPanel,
    GameOverPanel
}
/// <summary>
/// 命令事件的类型
/// </summary>
public enum CommandEvent
{
    ChangeMulitiple,
    RequestDealCommand,//派发出牌命令
      GrabLandord ,//抢地主
      PlayCard,//出牌成功
      PassCard,//不出牌命令
      GameOver,//游戏结束
      RequestUpdate,//更新积分命令
      CommandUpdateGameOver,//更新游戏结束面板
}

public enum ViewEvent
{
    DealCard,
    CompleteDeal, //结束发牌
    DealThreeCards,//发地主牌
    RequsetPlayCard,//出牌命令
    SuccessedPlayCard ,//玩家出牌成功
    UpdateIntegration,//更新积分显示命令
    ViewUpdateGameOver,//更新游戏结束面板
    ReStart,//重新开始游戏
}

/// <summary>
/// 卡牌能待的地方
/// </summary>
public enum CharacterType
{
    Library,
    Player,
    ComputerRight,
    ComputerLeft,
    Desk
}
/// <summary>
/// 卡牌的花色
/// </summary>
public enum Colors
{
    None,//大小王
    Club,//梅花
    Heart,//红桃
    Spade,//黑桃
    Square//方片
}
/// <summary>
/// 卡牌的大小
/// </summary>
public enum Weight
{
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    One,
    Two,
    SJoker,
    LJoker
}
/// <summary>
/// 卡牌的组合类型
/// </summary>
public enum CardType
{
    None,
    Single,//单
    Double,//对儿
    Straight,//顺子
    DoubleStraight,//双顺
    TripleStraight,//飞机
    Three,//三不带
    ThreeAndOne,//三带一
    ThreeAndTwo,//三带二
    Boom,//炸弹
    JokerBoom//王炸
}
/// <summary>
/// 牌的身份
/// </summary>
public enum Identity
{
    Farmer,//农民
    Landlord//地主
}
/// <summary>
/// Desk卡牌显示的位置
/// </summary>
public enum ShowPoint
{
    Desk,
    Player,
    ComputerRight,
    ComputerLeft
}
