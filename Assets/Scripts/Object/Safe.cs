using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : Objects
{
    private float DepositDelay;//입금 딜레이 기준시간
    private float DepositAmount;//입금 딜레이 남은시간

    private villain villain;
    bool playerStay = true;

    //TODO: 스타트 바꿔야하는지 체크
    private new void Start()
    {
        Delay = 30f;
        DepositDelay = 1f;
        base.Start();

    }
    private void Update()
    {
        FindPlayer();
        FinaPlayer2();
        Moneysteal();
        print(GameManager.Instance.Gamemoney);
        VillainManager.Instance.theftvillainSpawn(transform.position + Vector3.down);
    }
    private void FinaPlayer2() 
    {
        Collider2D Player = Physics2D.OverlapBox(transform.position + Vector3.down, Vector2.one * 0.7f, 0);
        if (ObjectSituation && Time.time - DepositAmount > DepositDelay)
        {
            if (Player.TryGetComponent<Player>(out Player newplayer))
            {
                print("플레이어 돈 넣는중");
                newplayer.MoneyDeposit(Objecttype.Money);
                DepositAmount = Time.time;
            }
        }
    }



    //TODO:강화 화면 출력
    public void FindPlayer()
    {
        if (ObjectSituation)
        {
            //이거 이렇게 구현하면 접근못함

            //Collider2D player = Physics2D.OverlapBox(transform.position + Vector3.left, Vector3.one, 0);

            //if (player == null) return;
            //if (player.TryGetComponent<Player>(out Player newplayer))
            //{
            //    UpgradeObject();
            //}

            Collider2D player1 = Physics2D.OverlapBox(transform.position + Vector3.right, Vector3.one*0.7f, 0);

            if (player1&&player1.TryGetComponent<Player>(out Player newplayer1))
            {
                if (playerStay)
                {
                  playerStay = false;
                  UpgradeObject();
                }
                
            }
            else
            {
                playerStay = true;
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position + Vector3.right, Vector3.one);
    }
    //빌런 플레이어 체크
    //protected override void OnTriggerStay2D(Collider2D collision)
    //{
    //    base.OnTriggerStay2D(collision);
    //    if (ObjectSituation && Time.time - DepositAmount > DepositDelay)
    //    {
    //        if (collision.TryGetComponent<Player>(out Player newplayer))
    //        {
    //            print("플레이어 돈 넣는중");
    //            newplayer.MoneyDeposit(Objecttype.Money);
    //            DepositAmount = Time.time;
    //        }
    //    }

    //}
    private void Moneysteal()
    {
        if (!ObjectSituation)
        {           
            //TODO:  경고알림 여기 구현해야할듯
            ObjectDelay += 1 * Time.deltaTime;
            print("빌런이 돈 훔치는중");
            print(ObjectDelay);
            if (ObjectDelay < Delay) return;
            GameManager.Instance.withdrawal(GameManager.Instance.Gamemoney / 2);
            print($"빌런이 돈 훔쳐감{GameManager.Instance.Gamemoney}");
            VillainManager.Instance.Die(touchVillain);
            ObjectDelay = 0;

        }
        if (ObjectSituation)
        {
            ObjectDelay = 0;
        }
    }
    //TODO:가게 업그레이드 구현
    private void UpgradeObject()
    {
        print("업그레이드 호출");
        UIManager.Instance.OnUpgradePanel();
    }
}
