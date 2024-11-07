using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Counter : Objects
{
    public Money moneyPrefab;
    public List<Customer> Customerprefabs;


    private const int MaxFood = 99;//���� �������� ����

    private float CustomerspawnDelay = 1f;//�մ� ���� ��Ÿ��
    private float CustomersDelay = 0f;//�մ� ���� ������Ÿ��

    private bool CounterUpgrarade = false;//���� ���׷��̵� ����

    protected bool Customerbool = true;//�մ� ��ȯ ���ɿ���

    public GameObject Foods;//�������� ����


    private new void Start()
    {
        Delay = 0.5f;//�θ� ������ ���� ������(�Ǹżӵ� ���׷��̵� ����)
        ObjectList.Add(Objecttype.Money, 0);
        ObjectList.Add(Objecttype.Food, 0);
        StartCoroutine(VillainManager.Instance.FirstSpawn(transform.position + Vector3.down));
        Foods.SetActive(false);
    }
    private void Update()
    {
        FindCustomer();
        RefillFood();
        spawnmoney();
        RandomCustomer();
        foodRenderer();
        VillainManager.Instance.villainSpawn(transform.position + Vector3.down);
    }
    private void foodRenderer()
    {
        if (ObjectList[Objecttype.Food] > 0)
        {
            Foods.SetActive(true);
        }
        else
        {
            Foods.SetActive(false);
        }
    }
    private void RefillFood()
    {
        if (ObjectSituation)
        {
            Collider2D Findplayer2 = Physics2D.OverlapBox(transform.position + new Vector3(2f, 0.5f, 0f), new Vector2(0.8f, 1.8f), 0f);
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

    private void FindCustomer()
    {
        if (ObjectSituation)
        {
            Collider2D[] Findplayer1 = Physics2D.OverlapBoxAll(transform.position + Vector3.down, new Vector2(0.8f, 0.8f), 0f);
            if (Findplayer1 == null) return;
            foreach (Collider2D player in Findplayer1)
            {
                if (player.TryGetComponent<Customer>(out Customer customer))
                {
                    print("�� �÷��̾� ��ٸ�����");
                    Sell(customer);
                }
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
        Collider2D FindCustomer1 = Physics2D.OverlapBox(transform.position + new Vector3(0.5f, 1f, 0f), new Vector2(1.8f, 0.8f), 0f);
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
        Gizmos.DrawWireCube(transform.position + new Vector3(0.5f, 1f, 0f), new Vector2(1.8f, 0.8f));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(2f, 0.5f, 0), new Vector2(0.8f, 1.8f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + Vector3.down, Vector3.one * 0.8f);
    }

    private void spawnmoney()
    {

        if (ObjectList[Objecttype.Money] > 0)
        {
            Money ga = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
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
    public void CustomerSpawn()
    {

        Collider2D customer = Physics2D.OverlapBox(transform.position + Vector3.down, Vector3.one, 0);
        if (customer.CompareTag("Customer") || customer.CompareTag("Player") || customer.CompareTag("Villain"))
        {
            print("������ ����");
            return;
        }
        RandomCustomer();
    }
    public void RandomCustomer()
    {
        if (Customerbool && ObjectSituation)
        {
            CustomersDelay += Time.deltaTime;
            if (CustomersDelay < CustomerspawnDelay) return;
            float Ran = Random.Range(1, 100f);
            if (Ran <= StageManger.Instance.CistomerChance)
            {
                Customer newCustomer = Instantiate(Customerprefabs[0], transform.position + Vector3.down, Quaternion.identity);
                newCustomer.MaxFood = Random.Range(1, 4);
                CustomersDelay = 0f;
                return;
            }
            else
            {
                Customer newCustomer = Instantiate(Customerprefabs[1], transform.position + Vector3.down, Quaternion.identity);
                newCustomer.MaxFood = Random.Range(5, 11);
                CustomersDelay = 0f;
                return;
            }

        }
        else
        {
            CustomersDelay = 0f;
        }
    }
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.CompareTag("Customer") || collision.CompareTag("Player"))
        {
            Customerbool = false;
        }
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.CompareTag("Customer") || collision.CompareTag("Player"))
        {
            Customerbool = true;
        }
    }




    // TODO: ��ȭ �����ؾ���. �� ������Ʈ ������� ���� �ʿ�
}
