using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 事件处理结束发送给View更新显示
/// </summary>
public class UpdateGameOverCommand:EventCommand
{
    [Inject]
    public RoundModel1 round { get; set; }
    public override void Execute()
    {
        GameOverShowArgs e = new GameOverShowArgs()
        {
            isWin=round.isWin,
            isLand=round.isLand,
        };
        dispatcher.Dispatch(ViewEvent.ViewUpdateGameOver,e);
    }
}
