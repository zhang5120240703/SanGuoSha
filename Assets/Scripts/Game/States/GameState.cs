using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public List<Player> players = new List<Player>();
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
}
