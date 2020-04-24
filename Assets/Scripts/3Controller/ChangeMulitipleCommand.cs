using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMulitipleCommand : EventCommand {

    [Inject]
    public   IntegrationModel model { set; get; }
    public override void Execute()
    {
        //更改倍数,获取派发器传送的值
        model.m_Mulitiple *= (int)evt.data;
        //添加新的面板
        ToolsManager.CreatePrefab(PrefabType.CharacterPanel);
        ToolsManager.CreatePrefab(PrefabType.IntegrationPanel);
    }
}
