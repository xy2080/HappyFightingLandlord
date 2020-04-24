using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public  class RequestUpdateCommand:EventCommand
{
    [Inject]
    public IntegrationModel integrationModel { get; set; }
    public override void Execute()
    {
        GameData gameData = new GameData() {m_playerIntegration=integrationModel.PlayerIntegration,m_computerLeftIntegration=integrationModel.ComputerLeftIntegration,m_computerRightIntegration=integrationModel.ComputerRightIntegration };
        dispatcher.Dispatch(ViewEvent.UpdateIntegration,gameData);

    }
}
