using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverView : EventView {

    public Image ShowImage;
    public List<Sprite> sprites;

    public Button Btn;

    /// <summary>
    /// 游戏结束，玩家结算面板的显示
    /// </summary>
    /// <param name="isLand"></param>
    /// <param name="isWin"></param>
    public void Init(bool isLand,bool isWin)
    {
        if(isLand)
        {
            if(isWin)
            {
                ShowImage.sprite= sprites[0];
            }
            else
            {
                ShowImage.sprite = sprites[1];
            }
        }
        else
        {
            if (isWin)
            {
                ShowImage.sprite = sprites[2];
            }
            else
            {
                ShowImage.sprite = sprites[3];
            }
        }
    }


}
