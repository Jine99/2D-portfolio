using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villain : MonoBehaviour
{
    private float Delay;//빌런 기준시간 딜레이
    private float VillainDelay = 3f;//빌런 딜레이


    internal bool theftVillain = false;//빌런 종류 true면 절도빌런

    void Start()
    {
        //빌런 생성직후 매니저에게 넣어줌
        VillainManager.Instance.DestroyVillain(this);

        VillainManager.Instance.villainList.Add(this);
        //절도 빌런이면 빌런 체크시간 1.5초
        if (theftVillain)
        {
            //TODO: 대응강화 넣어야함
            VillainDelay = 1.5f;

        }
    }

    void Update()
    {
        Playercheck();

    }


    private void Playercheck()//빌런 주변에 플레이어 체크
    {
        Collider2D[] Players = Physics2D.OverlapBoxAll(transform.position, Vector2.one * 2.8f, 0);
        if (Players == null) return;
        Player player1 = null;
        foreach (Collider2D player in Players)
        {
            if (player.TryGetComponent<Player>(out Player pp))
            {
                player1 = pp;
                Delay += 1f * Time.deltaTime;
                print(Delay);
                if (Delay > VillainDelay)
                {
                    Die();
                }
            }
            if (player1 == null)
            {
                Delay = 0;
            }
        }

    }

    public void Die()
    {
        VillainManager.Instance.villainList.Remove(this);
        if (!VillainManager.Instance.villainswitch) VillainManager.Instance.villainswitch = true;
        VillainManager.Instance.Delay1 = 0;
        VillainManager.Instance.Delay2 = 0;
        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector2.one * 2.8f);
    }


}
