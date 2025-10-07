using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum shenfen
{
    ZhuGong,
    FanZei,
    NeiJian,
    ZhongCheng
}
[CreateAssetMenu(menuName = "身份类型")]
public class Identity : ScriptableObject
{
    public shenfen identity;

    public bool CheckWinCondition(GameState gameState)
    {
        switch (identity)
        {
            case shenfen.ZhuGong:
                return gameState.AllFanZeiDied() && gameState.AllNeiJianDied();
            case shenfen.FanZei:
                return gameState.ZhuGongDied();
            case shenfen.NeiJian:
                return gameState.ZhuGongDied() && gameState.AllZhongChengDied() && gameState.AllFanZeiDied();
            case shenfen.ZhongCheng:
                return gameState.AllFanZeiDied() && gameState.AllNeiJianDied() && !gameState.ZhuGongDied();
            default:
                return false;
        }
    }
}
