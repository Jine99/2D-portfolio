using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Counter : Objects
{
    public Money moneyPrefabs;
    public GameObject Prefabs2;

    private const int MaxFood = 99;//계산대 소지가능 음식

    private bool CounterUpgrarade = false;//계산대 업그레이드 유무


    private new void Start()
    {
        Delay = 0.5f;//부모 딜레이 변수 재정의(판매속도 업그레이드 가능)
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
            print("빌런 점유중");
        }
    }
    //플레이어또는 업그레이드유무 확인후 거래진행
    public void Sell(Customer customer)
    {
        Collider2D FindCustomer1 = Physics2D.OverlapBox(transform.position + new Vector3(0.5f, 1f, 0f), new Vector2(2f, 1f), 0f);
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
            print("카운터에 돈이없다");
        }
    }
    // TODO: 손님 리스폰 구현해야함
    // TODO: 강화 구현해야함. 돈 오브젝트 생성방식 보완 필요
}
