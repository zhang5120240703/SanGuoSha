using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour
{
    //每位玩家独立的id即几号位
    [SyncVar]
    private int PlayerID;

    //玩家身份
    [SyncVar(hook = nameof(OnIdentityChanged))]
    private Identity identity;

    [SyncVar(hook = nameof(OnDeathChanged))]
    private bool isDead = false;

    private GeneralCardOBJ General;

    [SyncVar(hook = nameof(OnHealthChanged))]
    private int currentHealth;

    [SyncVar(hook = nameof(OnCardCountChanged))]
    private int currentDeckCount;

    [SyncVar]
    private int AttackRange = 1;

    private void Update()
    {
        if(!isLocalPlayer) return;

        if(Input.GetMouseButtonDown(0))
        {
            bool isClickOnUI = EventSystem.current.IsPointerOverGameObject();
            if (isClickOnUI)
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;

                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raycastResults);

                if (raycastResults.Count > 0)
                {
                    // 输出第一个命中的UI元素名称（通常是最上层的UI）
                    Debug.Log("具体命中的UI：" + raycastResults[0].gameObject.name);
                }
            }
        }
    }

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

    public void UpdateCurrentDeckCount(int count)
    {
        currentDeckCount = count;
    }

    public int GetCurrentDeckCount()
    {
        return currentDeckCount;
    }

    public void SetAttackRange(int Range)
    {
        AttackRange = Range;
    }
    #endregion

    #region 网络同步钩子函数

    private void OnIdentityChanged(Identity Identity)
    {
        identity = Identity;
        Debug.Log($"玩家{PlayerID}的身份是：{identity.identity}");
    }

    private void OnHealthChanged(int Damage)
    {
        currentHealth-=Damage;
        Debug.Log($"玩家{PlayerID}的当前血量是：{currentHealth}");
        if(currentHealth<=0)
        {
            isDead = true;
            Debug.Log($"玩家{PlayerID}阵亡");
        }
    }

    private void OnCardCountChanged(int Count)
    {
        currentDeckCount += Count;
        Debug.Log($"玩家{PlayerID}的当前手牌数是：{currentDeckCount}");

    }

    private void OnDeathChanged(bool Death)
    {
        isDead = Death;
        if (isDead)
        {
            Debug.Log($"玩家{PlayerID}阵亡");
        }
    }
    #endregion
}
