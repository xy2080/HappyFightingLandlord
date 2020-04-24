using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GrabLnadlordCommand:EventCommand
{
    [Inject]
    public RoundModel1 round { set; get; }
    public override void Execute()
    {
        GrabAndDisGrab temp = (GrabAndDisGrab)evt.data;
        //发出派发地主牌的命令
        dispatcher.Dispatch(ViewEvent.DealThreeCards, temp);
        //地主触发
        round.Start(temp.type);
    }
}

