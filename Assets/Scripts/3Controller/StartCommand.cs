using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartCommand : Command {
    /// <summary>
    /// IntegrationModel模型类已经被绑定，定义模型类的对象加[Inject]，该对象会自动被赋值
    /// </summary>
    [Inject]
    public IntegrationModel model { set; get; }

    [Inject]
    public RoundModel1 roundModelP { set; get; }

    [Inject]
    public CardModel cardModel { set; get; }
    /// <summary>
    /// 需要被执行的事件
    /// </summary>
    public override void Execute()
    {
     
        //调用数据模型初始化事件
        model.InitIntegration();    //初始化玩家基础积分以及游戏倍数
        roundModelP.Init();     //初始化当前最大牌的权值、类型、牌的归属
        cardModel.Init();       //生成一副牌

        //调用执行的事件
        ToolsManager.CreatePrefab(PrefabType.StartPanel);

        //读取数据
        GetData();

        //背景音效
      //  Sound.Instance.PlayBG(Consts.Bg);
    }
   
    /// <summary>
    /// 读取游戏存档数据并更新
    /// </summary>
    public void GetData()
    {
        FileInfo info = new FileInfo(Consts.DataPath);
        if(info.Exists)
        {
            GameData data = ToolsManager.GetData();
            model.PlayerIntegration = data.m_playerIntegration;
            model.ComputerLeftIntegration = data.m_computerLeftIntegration;
            model.ComputerRightIntegration = data.m_computerRightIntegration;
        }
    }
}
