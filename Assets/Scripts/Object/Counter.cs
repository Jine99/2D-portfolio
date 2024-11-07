using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Counter : Objects
{
    public Money moneyPrefab;
    public List<Customer> Customerprefabs;


    private const int MaxFood = 99;//계산대 소지가능 음식

    private float CustomerspawnDelay = 1f;//손님 스폰 쿨타임
    private float CustomersDelay = 0f;//손님 스폰 기준쿨타임

    private bool CounterUpgrarade = false;//계산대 업그레이드 유무

    protected bool Customerbool = true;//손님 소환 가능여부

    public GameObject Foods;//랜더링할 음식


    private new void Start()
    {
        Delay = 0.5f;//부모 딜레이 변수 재정의(판매속도 업그레이드 가능)
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
                    print($"계산대 음식 채우는중{ObjectList[Objecttype.Food]}");
                }
                else if (ObjectList[Objecttype.Food] >= MaxFood)
                {
                    print("계산대 음식이 가득참");
                }
                else
                {
                    print("플레이어 음식 부족함");
                }
            }
        }
        else
        {
            print("음식 채우기 불가능");
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
                    print("고객 플레이어 기다리는중");
                    Sell(customer);
                }
            }
        }
        else
        {
            print("빌런 점유중");
        }
    }
    //플레이어또는 업그레이드유무 확인후 거래진행
    public void Sell(Customer customer)
    {
        Collider2D FindCustomer1 = Physics2D.OverlapBox(transform.position + new Vector3(0.5f, 1f, 0f), new Vector2(1.8f, 0.8f), 0f);
        if (FindCustomer1 == null) return;
        if (FindCustomer1.TryGetComponent<Player>(out Player player) || CounterUpgrarade)
        {
            print("플레이어 거래하러 도착");
            while (customer.MaxFood > customer.CustomerFood && ObjectList[Objecttype.Food] > 0)
            {
                //딜레이추가
                if (Time.time - ObjectDelay < Delay) return;
                //손님에게 푸드 추가
                customer.CustomerFoodSell();
                //카운터에서 푸드 빼고 머니생성
                ObjectList[Objecttype.Food]--;
                ObjectList[Objecttype.Money] = ObjectList[Objecttype.Money] + 10;
                print($"계산대 보유 음식양{ObjectList[Objecttype.Food]}");
                ObjectDelay = Time.time;

            }
            if (ObjectList[Objecttype.Food] <= 0)
            {
                print("카운터 음식이 부족함");
            }

        }
        else
        {
            print("손님이 플레이어 찾는중");
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
            print("카운터에 돈이없다");
        }
    }
    // TODO: 손님 리스폰 구현해야함
    public void CustomerSpawn()
    {

        Collider2D customer = Physics2D.OverlapBox(transform.position + Vector3.down, Vector3.one, 0);
        if (customer.CompareTag("Customer") || customer.CompareTag("Player") || customer.CompareTag("Villain"))
        {
            print("누군가 있음");
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




    // TODO: 강화 구현해야함. 돈 오브젝트 생성방식 보완 필요
}
