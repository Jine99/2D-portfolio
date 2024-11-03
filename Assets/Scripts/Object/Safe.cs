using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : Objects
{
    private float DepositDelay;//�Ա� ������ ���ؽð�
    private float DepositAmount;//�Ա� ������ �����ð�


    //TODO: ��ŸƮ �ٲ���ϴ��� üũ
    private new void Start()
    {
        Delay = 30f;
        DepositDelay = 1f;
    }


    //��ȭ ȭ�� ���
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
            print("���� ������");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + Vector3.left, Vector3.one);
    }
    //���� �÷��̾� üũ
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
                //TODO:  ���˸� ���� �����ؾ��ҵ�
                print("������ �� ��ġ����");
                if (Time.time - ObjectDelay < Delay) return;
                GameManager.Instance.Gamemoney = GameManager.Instance.Gamemoney / 2;
            }
        

    }
    //TODO:���� ���׷��̵� ����
    private void UpgradeObject()
    {
        print("���׷��̵� ȣ��");
    }
}
