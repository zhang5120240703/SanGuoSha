using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : MonoBehaviour
{
    //每位玩家独立的id即几号位
    public int PlayerID;
    //玩家身份
    public Identity identity;
    public int currentCardCount = 0;

    public bool isDead = false;
    public GeneralCardOBJ General;


}
