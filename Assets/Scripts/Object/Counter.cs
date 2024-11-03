using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Counter : Objects
{
    public Money moneyPrefabs;
    public GameObject Prefabs2;

    private const int MaxFood = 99;//���� �������� ����

    private bool CounterUpgrarade = false;//���� ���׷��̵� ����


    private new void Start()
    {
        Delay = 0.5f;//�θ� ������ ���� ������(�Ǹżӵ� ���׷��̵� ����)
        ObjectList.Add(Objecttype.Money, 0);
        ObjectList.Add(Objecttype.Food, 0);
    }
    private void Update()
    {
        FindPlayer();
        RefillFood();
        spawnmoney();
    }
    private void RefillFood()
    {
        if (ObjectSituation)
        {
            Collider2D Findplayer2 = Physics2D.OverlapBox(transform.position + new Vector3(2f, 0.5f, 0f), new Vector2(1f, 2f), 0f);
            if (Findplayer2 == null) return;
            if (Findplayer2.TryGetComponent<Player>(out Player player1))
            {
                if (GameManager.Instance.player.Inventory.ContainsKey(Objecttype.Food) && ObjectList[Objecttype.Food] < MaxFood)
                {
                    GameManager.Instance.player.Inventory[Objecttype.Food]--;
                    ObjectList[Objecttype.Food]++;
                    print($"���� ���� ä�����{ObjectList[Objecttype.Food]}");
                }
                else if (ObjectList[Objecttype.Food] >= MaxFood)
                {
                    print("���� ������ ������");
                }
                else
                {
                    print("�÷��̾� ���� ������");
                }
            }
        }
        else
        {
            print("���� ä��� �Ұ���");
        }
    }

    private void FindPlayer()
    {
        if (ObjectSituation)
        {
            Collider2D Findplayer1 = Physics2D.OverlapBox(transform.position + Vector3.down, new Vector2(1f, 1f), 0f);
            if (Findplayer1 == null) return;
            if (Findplayer1.TryGetComponent<Customer>(out Customer customer))
            {
                Sell(customer);
            }
        }
        else
        {
            print("���� ������");
        }
    }
    //�÷��̾�Ǵ� ���׷��̵����� Ȯ���� �ŷ�����
    public void Sell(Customer customer)
    {
        Collider2D FindCustomer1 = Physics2D.OverlapBox(transform.position + new Vector3(0.5f, 1f, 0f), new Vector2(2f, 1f), 0f);
        if (FindCustomer1 == null) return;
        if (FindCustomer1.TryGetComponent<Player>(out Player player) || CounterUpgrarade)
        {
            print("�÷��̾� �ŷ��Ϸ� ����");
            while (customer.MaxFood > customer.CustomerFood && ObjectList[Objecttype.Food] > 0)
            {
                //�������߰�
                if (Time.time - ObjectDelay < Delay) return;
                //�մԿ��� Ǫ�� �߰�
                customer.CustomerFoodSell();
                //ī���Ϳ��� Ǫ�� ���� �Ӵϻ���
                ObjectList[Objecttype.Food]--;
                ObjectList[Objecttype.Money] = ObjectList[Objecttype.Money] + 10;
                print($"���� ���� ���ľ�{ObjectList[Objecttype.Food]}");
                ObjectDelay = Time.time;

            }
            if (ObjectList[Objecttype.Food] <= 0)
            {
                print("ī���� ������ ������");
            }

        }
        else
        {
            print("�մ��� �÷��̾� ã����");
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.5f, 1f, 0f), new Vector2(2f, 1f));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(2f, 0.5f, 0), new Vector2(1, 2));
    }

    private void spawnmoney()
    {

        if (ObjectList[Objecttype.Money] > 0)
        {
            Money ga = Instantiate(moneyPrefabs, transform.position, Quaternion.identity);
            ga.transform.position = transform.position + Vector3.left;
            ga.cost = ObjectList[Objecttype.Money];
            ObjectList[Objecttype.Money] = 0;
        }
        else
        {
            print("ī���Ϳ� ���̾���");
        }
    }
    // TODO: �մ� ������ �����ؾ���
    // TODO: ��ȭ �����ؾ���. �� ������Ʈ ������� ���� �ʿ�
}
