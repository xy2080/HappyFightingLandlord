using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public  class GameOverCommand:EventCommand
{
    [Inject]
    public RoundModel1 round { set; get; }

    [Inject]
    public IntegrationModel integrationModel { set; get; }
    [Inject]
    public CardModel cardModel { set; get; }

    public override void Execute()
    {
        //更新数据
        int temp = integrationModel.Result;
        GameOverArgs e = (GameOverArgs)evt.data;
        //玩家胜利
        if(e.PlayerWin)
        {
            if(e.type==CharacterType.Player)
                integrationModel.PlayerIntegration += temp*2;
            else
            integrationModel.PlayerIntegration += temp;
        }
        else
        {
            if (e.type == CharacterType.Player)
                integrationModel.PlayerIntegration -= temp*2;
            else
                integrationModel.PlayerIntegration -= temp;

        }
        //右边电脑玩家胜利
        if (e.ComputerRigh)
        {
            if (e.type == CharacterType.ComputerRight)
                integrationModel.ComputerRightIntegration += temp* 2;
            else
                integrationModel.ComputerRightIntegration += temp;
        }
        else
        {
            if (e.type == CharacterType.ComputerRight)
                integrationModel.ComputerRightIntegration -= temp * 2;
            else
                integrationModel.ComputerRightIntegration -= temp;
        }
        //左边电脑玩家胜利
        if (e.ComputerLeft)
        {
            if (e.type == CharacterType.ComputerLeft)
                integrationModel.ComputerLeftIntegration += temp * 2;
            else
                integrationModel.ComputerLeftIntegration += temp;
        }
        else
        {
            if (e.type == CharacterType.ComputerLeft)
                integrationModel.ComputerLeftIntegration -= temp*2;
            else
                integrationModel.ComputerLeftIntegration -= temp;

        }
        ///用于更新结束面板的显示
        round.isLand = e.isLand;
        round.isWin = e.PlayerWin;
        //存储数据
        GameData data = new GameData()
        {
            m_playerIntegration = integrationModel.PlayerIntegration,
            m_computerLeftIntegration= integrationModel.ComputerLeftIntegration,
            m_computerRightIntegration= integrationModel.ComputerRightIntegration
        };
        ToolsManager.SaveData(data);
        //显示数据
        GameData gameData = new GameData()
        { m_playerIntegration = integrationModel.PlayerIntegration,
            m_computerLeftIntegration = integrationModel.ComputerLeftIntegration,
            m_computerRightIntegration = integrationModel.ComputerRightIntegration };
        dispatcher.Dispatch(ViewEvent.UpdateIntegration, gameData);
        //添加游戏结束面板
        ToolsManager.CreatePrefab(PrefabType.GameOverPanel);
        //清空游戏缓存,初始化游戏数据
        round.Init();
        cardModel.Init();
    }
}
