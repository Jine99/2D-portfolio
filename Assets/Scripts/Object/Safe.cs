using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : Objects
{
    private float DepositDelay;//입금 딜레이 기준시간
    private float DepositAmount;//입금 딜레이 남은시간


    //TODO: 스타트 바꿔야하는지 체크
    private new void Start()
    {
        Delay = 30f;
        DepositDelay = 1f;
    }


    //강화 화면 출력
    public void FindPlayer()
    {
        if (ObjectSituation)
        {
            Collider2D player = Physics2D.OverlapBox(transform.position + Vector3.left, Vector3.one, 0);

            if (player == null) return;
            if (player.TryGetComponent<Player>(out Player newplayer))
            {
                UpgradeObject();
            }
        }
        else
        {
            print("빌런 점유중");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + Vector3.left, Vector3.one);
    }
    //빌런 플레이어 체크
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (ObjectSituation)
        {
            if (collision.TryGetComponent<Player>(out Player newplayer) && Time.time - DepositAmount>DepositDelay)
            {
                newplayer.MoneyDeposit(Objecttype.Money);
                DepositAmount =Time.time;
            }
        }
            if (!ObjectSituation)
            {
                //TODO:  경고알림 여기 구현해야할듯
                print("빌런이 돈 훔치는중");
                if (Time.time - ObjectDelay < Delay) return;
                GameManager.Instance.Gamemoney = GameManager.Instance.Gamemoney / 2;
            }
        

    }
    //TODO:가게 업그레이드 구현
    private void UpgradeObject()
    {
        print("업그레이드 호출");
    }
}
