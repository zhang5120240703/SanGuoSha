using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public List<Player> players;
    public void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        Debug.Log("Game Started!");
    }
    
}
