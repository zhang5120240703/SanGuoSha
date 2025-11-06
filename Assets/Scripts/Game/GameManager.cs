using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    public List<Player> players = new List<Player>();
    public int MaxPlayers = 5;

    public Identity ZhuGong;
    public Identity ZhongChen;
    public Identity NeiJian;
    public Identity FanZei;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //新玩家加入
    public override void OnStartServer()
    {
        NetworkServer.RegisterHandler<AddPlayerMessage>(OnPlayerConnected);
        NetworkServer.OnDisconnectedEvent += OnPlayerDisconnected;
    }

    private void OnPlayerConnected(NetworkConnection conn, AddPlayerMessage message)
    {
        Player newPlayer = conn.identity.GetComponent<Player>();
        if(players.Count >= MaxPlayers)
        {
            conn.Disconnect();
            return;
        }

        newPlayer.SetPlayerID(players.Count+1);
        players.Add(newPlayer);
        Debug.Log($"玩家{newPlayer.GetPlayerID()}加入房间");
    }

    private void OnPlayerDisconnected(NetworkConnection conn)
    {
        Player player = conn.identity.GetComponent<Player>();
        if(player != null)
        {
            players.Remove(player);
        }
    }
    public void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        Debug.Log("Game Started!");
    }
    
}
