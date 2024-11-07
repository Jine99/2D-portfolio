using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Table : Objects
{

    internal bool TableReserved = true;//테이블 고객 예약여부

    protected bool eating = true;//플레이어 존재 여부

    internal int Trash;//쓰레기 개수

    public GameObject[] garbage;//쓰레기 랜더링

    private int RemoveFood = 2;//초당 쓰레기 삭제속도

    private new void Start()
    {
        Delay = 1f/ RemoveFood;//음식 삭제 딜레이
        foreach (GameObject obj in garbage)
        {
            obj.SetActive(false);
        }
        base.Start();

    }
    private void Update()
    {
        FindColliderOnPlayer();
        if (Trash <= 0)
        {
            eating = true;
        }
        else if (Trash > 0)
        {
            eating = false;
        }
        Rendergarbage();
    }
    private void Rendergarbage()
    {
        foreach(GameObject obj in garbage)
        {
            obj.SetActive(false );
        }
        if (Trash == 1)
        {
            for (int i = 0; i < 1; i++)
            {
                garbage[i].SetActive(true);
            }
        }
        if (Trash == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                garbage[i].SetActive(true);
            }
        }
        if (Trash >= 3)
        {
            for (int i = 0; i < garbage.Length; i++)
            {
                garbage[i].SetActive(true);
            }
        }
        else if (Trash <= 0)
        {
            return;
        }

    }
    private void FindColliderOnPlayer()
    {
        if (ObjectSituation)
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position + Vector3.left, Vector2.one * 0.8f, 0f);
            //if (hit == null) return;
            if (hit && hit.TryGetComponent<Player>(out Player player))
            {
                if (Trash > 0&& GameManager.Instance.player.Invenadd(Objecttype.Trash))
                {
                    Trash--;
                    print("플레이어 쓰레기 받음");
                }
                else
                {
                    print("쓰레기 없음");

                }
            }
            Collider2D[] hitdahit = Physics2D.OverlapBoxAll(transform.position + Vector3.up, Vector2.one * 0.8f, 0f);
            if(hitdahit == null) return;
            foreach (Collider2D collider in hitdahit)
            {
                if (collider.TryGetComponent<Customer>(out Customer customer))
                {
                    if (Time.time - ObjectDelay < Delay) return;
                    print("고객 음식 섭취중");
                    customer.Foodeat();
                    Trash++;
                    ObjectDelay = Time.time;
                }
            }
        }
        else
        {
            print("빌런이 점유중");
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + Vector3.up, Vector2.one * 0.8f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.left, Vector2.one * 0.8f);
    }
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.CompareTag("Customer"))
        {
            eating = false;
        }
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.CompareTag("Customer"))
        {
            eating = true;
        }
    }

    public bool tablesituation()
    {
        if (ObjectSituation == eating)
        {
            return true;
        }
        return false;
    }


    public void UpgradeTable(int speed)
    {
        RemoveFood += speed;
        Delay = 1f / RemoveFood;
    }
}
