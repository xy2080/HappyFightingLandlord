using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PassCardCommand:EventCommand
{
    [Inject]
    public RoundModel1 round { get; set; }
    public override void Execute()
    {
        round.Turn();
        Sound.Instance.PlayEffect(Consts.PassCard[UnityEngine.Random.Range(0,3)]);//不要音效
    }
}
