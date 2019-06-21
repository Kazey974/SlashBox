using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    public Player player;
    public RectTransform curHP_image;

    private void Update()
    {
        DisplayHealth();
    }

    void DisplayHealth()
    {
        if(player.isAlive())
        {
            float HPpercent = (player.getCurHP()/player.getMaxHP())*100;
            curHP_image.sizeDelta = new Vector2(HPpercent,10);
        }
        else
        {
            curHP_image.sizeDelta = new Vector2(0,10);
        }
    }

}
