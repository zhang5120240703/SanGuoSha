using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public List<Player> players = new List<Player>();

    #region 游戏宏观状态

    public enum GameMode
    {
        Waiting,
        Playing,
        Ended
    }
    public GameMode currentGameMode = GameMode.Waiting;

    #endregion

    #region 游戏胜负判定
    public bool AllFanZeiDied()
    {
        //查询每个玩家身份是反贼的玩家是否全部阵亡
        return players.Where(p=>p.GetIdentity().identity == shenfen.FanZei).All(p=>p.GetDead());
    }
    public bool AllNeiJianDied()
    {
        return players.Where(p=>p.GetIdentity().identity == shenfen.NeiJian).All(p=>p.GetDead());
    }
    public bool ZhuGongDied()
    {
        return players.Where(p=>p.GetIdentity().identity == shenfen.ZhuGong).All(p=>p.GetDead());
    }
    public bool AllZhongChengDied()
    {
        return players.Where(p=>p.GetIdentity().identity == shenfen.ZhongCheng).All(p=>p.GetDead());
    }
    #endregion

    #region 玩家行动状态
    public enum playerTurnState
    {
        Start,
        PanDing,
        MoPai,
        ChuPai,
        QiPai,
        End
    }
    public playerTurnState currentPlayerTurnState;
    //当前行动玩家的索引
    public int currentPlayerId = 0;
    #endregion

    #region 牌堆状态

    public int LeastDeckCount = 0;//牌堆剩余牌数
    public int DiscardPileCount = 0;//弃牌堆牌数

    #endregion


}
