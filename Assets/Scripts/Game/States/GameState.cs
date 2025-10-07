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
        return players.Where(p=>p.identity.identity == shenfen.FanZei).All(p=>p.isDead);
    }
    public bool AllNeiJianDied()
    {
        return players.Where(p=>p.identity.identity == shenfen.NeiJian).All(p=>p.isDead);
    }
    public bool ZhuGongDied()
    {
        return players.Where(p=>p.identity.identity == shenfen.ZhuGong).All(p=>p.isDead);
    }
    public bool AllZhongChengDied()
    {
        return players.Where(p=>p.identity.identity == shenfen.ZhongCheng).All(p=>p.isDead);
    }
}
