using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : Objects
{
    private float DepositDelay;//�Ա� ������ ���ؽð�
    private float DepositAmount;//�Ա� ������ �����ð�

    private villain villain;
    bool playerStay = true;

    //TODO: ��ŸƮ �ٲ���ϴ��� üũ
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
                print("�÷��̾� �� �ִ���");
                newplayer.MoneyDeposit(Objecttype.Money);
                DepositAmount = Time.time;
            }
        }
    }



    //TODO:��ȭ ȭ�� ���
    public void FindPlayer()
    {
        if (ObjectSituation)
        {
            //�̰� �̷��� �����ϸ� ���ٸ���

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
            print("���� ������");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + Vector3.left, Vector3.one);

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position + Vector3.right, Vector3.one);
    }
    //���� �÷��̾� üũ
    //protected override void OnTriggerStay2D(Collider2D collision)
    //{
    //    base.OnTriggerStay2D(collision);
    //    if (ObjectSituation && Time.time - DepositAmount > DepositDelay)
    //    {
    //        if (collision.TryGetComponent<Player>(out Player newplayer))
    //        {
    //            print("�÷��̾� �� �ִ���");
    //            newplayer.MoneyDeposit(Objecttype.Money);
    //            DepositAmount = Time.time;
    //        }
    //    }

    //}
    private void Moneysteal()
    {
        if (!ObjectSituation)
        {           
            //TODO:  ���˸� ���� �����ؾ��ҵ�
            ObjectDelay += 1 * Time.deltaTime;
            print("������ �� ��ġ����");
            print(ObjectDelay);
            if (ObjectDelay < Delay) return;
            GameManager.Instance.withdrawal(GameManager.Instance.Gamemoney / 2);
            print($"������ �� ���İ�{GameManager.Instance.Gamemoney}");
            VillainManager.Instance.Die(touchVillain);
            ObjectDelay = 0;

        }
        if (ObjectSituation)
        {
            ObjectDelay = 0;
        }
    }
    //TODO:���� ���׷��̵� ����
    private void UpgradeObject()
    {
        print("���׷��̵� ȣ��");
        UIManager.Instance.OnUpgradePanel();
    }
}
