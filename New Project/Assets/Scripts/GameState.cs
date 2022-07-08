using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private string currentPlayer;
    private bool inGame = true;

    public void setPlayer(string player)
    {
        currentPlayer = player;
    }

    public string getPlayer()
    {
        return currentPlayer;
    }

    public void setGameStatus(bool value)
    {
        inGame = value;
    }

    public bool getGameStatus()
    {
        return inGame;
    }

}
