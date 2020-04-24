using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 那个玩家胜利
/// </summary>
public  class GameOverArgs
{
    public bool PlayerWin = false;
    public bool ComputerLeft = false;
    public bool ComputerRigh = false;
    public CharacterType type;
    public bool isLand = false;
}
