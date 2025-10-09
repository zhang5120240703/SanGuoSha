using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "卡牌效果/基本牌/杀")]
public class Sha : CardsEffect
{
    public override void Effect()
    {
        Debug.Log("Sha Effect");
    }
}

[CreateAssetMenu(menuName = "卡牌效果/锦囊牌/无中生有")]
public class WuZhongShengYou : CardsEffect
{
    public override void Effect()
    {
        Debug.Log("WuZhongShengYou Effect");
    }
}