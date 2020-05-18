using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    int WolfCount;
    bool isPlayerDead;
    public Text screenText;
    public static int GamePhase;
    void Start()
    {
        screenText = GetComponent<Text>();
        WolfCount = 0;
        isPlayerDead = false;
        GamePhase = 1;
    }

    void Update()
    {
        if(15 - WolfCount > 0 && !isPlayerDead)
            screenText.text = "Wolf count: " + (15 - WolfCount).ToString();
        if (15 - WolfCount == 0 && !isPlayerDead)
        {
            screenText.text = "Flower count: 0";
            GamePhase = 2;
        }
    }

    public void IncrementWolfScore()
    {
        WolfCount++;
    }

    public void GameOver(bool status)
    {
        isPlayerDead = status;
        if(isPlayerDead)
            screenText.text = "Oh no! The wolf ate you!";
        else
            screenText.text = "Congratulations!";
    }
}
