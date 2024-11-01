using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : Objects
{
    public GameObject Prefabs1;
    public GameObject Prefabs2;

    private const int MaxFood = 99;//계산대 소지가능 음식

    private bool Playerstay = false;//계산대에 플레이어 여부


    private new void Start()
    {
        Delay = 0.5f;//부모 딜레이 변수 재정의
        ObjectList.Add(Objecttype.Monmey, 0);
        ObjectList.Add(Objecttype.Food, 0);
    }
    private void Update()
    {
        FindPlayer();
    }


    private void FindPlayer()
    {
        if (ObjectSituation)
        {
            Collider2D Findplayer1 = Physics2D.OverlapBox(transform.position + Vector3.down, new Vector2(1f, 1f), 0f);
            if (Findplayer1 == null) return;
            if (Findplayer1.TryGetComponent<Player>(out Player player))
            {
                print("플레이어 찾음");
            }
            Collider2D Findplayer2 = Physics2D.OverlapBox(transform.position + new Vector3(2f, 0.5f, 0), new Vector2(1f, 2f), 0f);
            if (Findplayer2 == null) return;
            if (Findplayer2.TryGetComponent<Player>(out Player player1))
            {
                if (GameManager.Instance.player.Inventory.ContainsKey(Objecttype.Food)&& ObjectList[Objecttype.Food] < MaxFood)
                {
                    GameManager.Instance.player.Inventory[Objecttype.Food]--;
                    ObjectList[Objecttype.Food]++;
                    print($"계산대 음식 채우는중{ObjectList[Objecttype.Food]}");
                }
                else if(ObjectList[Objecttype.Food] >= MaxFood)
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
            print("빌런 점유중");
        }
    }
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.TryGetComponent<Player>(out Player player))
        {
            Playerstay = true;
            print("플레이어 장사중");
        }
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.CompareTag("Player"))
        {
            Playerstay = false;
            print("플레이어 떠남");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + Vector3.down, new Vector2(1f,1f));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(2f, 0.5f, 0), new Vector2(1, 2));
    }



}
