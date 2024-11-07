using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    }
    private void Update()
    {
        FindPlayer();
        Moneysteal();
        print(GameManager.Instance.Gamemoney);
        VillainManager.Instance.theftvillainSpawn(transform.position + Vector3.down);
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

            if (playerStay&&player1&&player1.TryGetComponent<Player>(out Player newplayer1))
            {
                playerStay = false;
                UpgradeObject();
                
            }
            else if(player1 == null)
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
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (ObjectSituation)
        {
            if (collision.TryGetComponent<Player>(out Player newplayer) && Time.time - DepositAmount > DepositDelay)
            {
                print("�÷��̾� �� �ִ���");
                newplayer.MoneyDeposit(Objecttype.Money);
                DepositAmount = Time.time;
            }
        }

    }
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
    }
}
