using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : MonoBehaviour
{
    //每位玩家独立的id即几号位
    private int PlayerID;
    //玩家身份
    private Identity identity;
    private int currentCardCount = 0;

    private bool isDead = false;
    private GeneralCardOBJ General;

#region 数据接口区域
    public void SetPlayerID(int id)
    {
        PlayerID = id;
    }
    public int GetPlayerID()
    {
        return PlayerID;
    }
    public void SetDead(bool dead)
    {
        isDead = dead;
    }
    public bool GetDead()
    {
        return isDead;
    }
    public Identity GetIdentity()
    {
        return identity;
    }
    public void SetIdentity(Identity iden)
    {
        identity = iden;
    }
    
    public void SetGeneral(GeneralCardOBJ general)
    {
        General = general;
    }
    #endregion
}
