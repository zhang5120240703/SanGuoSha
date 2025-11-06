using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour
{
    //每位玩家独立的id即几号位
    [SyncVar]
    private int PlayerID;

    //玩家身份
    [SyncVar(hook = nameof(OnIdentityChanged))] private Identity identity;
    [SyncVar(hook = nameof(OnIdentitySync))] private string IdentityName;
    [SyncVar(hook = nameof(OnDeathChanged))] private bool isDead = false;
    [SyncVar(hook = nameof(OnHealthChanged))] private int currentHealth;
    [SyncVar(hook = nameof(OnCardCountChanged))] private int currentCardCount;

    private GeneralCardOBJ General;

    [SyncVar]
    private int AttackRange = 1;

    [Server]
    public void SetPlayerID(int id) => PlayerID = id;
    public int GetPlayerID() => PlayerID;

    [Server]
    public void Init(Identity iden)
    {
        this.identity = iden;
        IdentityName = identity.identity.ToString();
    }

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

    public void InitPlayer(int id)
    {
        if(!isServer)
        {
            return;
        }
        PlayerID = id;
        if(General!=null)
        {
            currentHealth = General.HP;
        }
        else
        {
            Debug.LogError("玩家未设置武将");
        }
    }

    #region 数据接口区域
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

    public void UpdateCurrentCardCount(int count)
    {
        currentCardCount = count;
    }

    public int GetCurrentCardCount()
    {
        return currentCardCount;
    }

    public void SetAttackRange(int Range)
    {
        AttackRange = Range;
    }
    #endregion

    #region 网络同步钩子函数

    private void OnIdentityChanged(Identity oldIdentity,Identity newIdentity)
    {
        identity = newIdentity;
        Debug.Log($"玩家{PlayerID}的身份是：{identity.identity}");
    }

    private void OnHealthChanged(int oldHealth,int newHealth)
    {
        currentHealth = newHealth;
        Debug.Log($"玩家{PlayerID}的当前血量是：{currentHealth}");
        if(currentHealth<=0)
        {
            isDead = true;
            Debug.Log($"玩家{PlayerID}阵亡");
        }
    }

    private void OnCardCountChanged(int oldCount,int newCount)
    {
        currentCardCount = newCount;
        Debug.Log($"玩家{PlayerID}的当前手牌数是：{currentCardCount}");

    }

    private void OnDeathChanged(bool oldDeath,bool newDeath)
    {
        isDead = newDeath;
        if (isDead)
        {
            Debug.Log($"玩家{PlayerID}阵亡");
        }
    }

    private void OnIdentitySync(string oldname,string newname)
    {
        IdentityName = newname;
        identity = Resources.Load<Identity>($"Identity/{newname}");
        Debug.Log($"玩家{PlayerID}身份为{IdentityName}");
    }

    [Server]
    public void TakeDamage(int damage)
    {
        if(isDead) return;
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        if(currentHealth == 0)
        {
            isDead = true;
            Debug.Log($"玩家{PlayerID}阵亡");
        }
    }

    [Command]
    public void CmdTakeDamage(int damage) => TakeDamage(damage);
    #endregion


}
