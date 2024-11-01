using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Table : Objects
{
    private int DeleteFood = 2;//한번에 삭제할 음식 개수

   


    private new void Start()
    {
        ObjectList.Add(Objecttype.Trash, 0);
    }
    private void Update()
    {
        FindColliderOnPlayer();
    }
    private void FindColliderOnPlayer()
    {
        if (ObjectSituation)
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position + Vector3.left, Vector2.one,0f);
            if (hit == null) return;
            if (hit.TryGetComponent<Player>(out Player player))
            {
                if (ObjectList[Objecttype.Trash] > 0)
                {
                    GameManager.Instance.player.Invenadd(Objecttype.Trash);
                    ObjectList[Objecttype.Trash]--;
                    print("플레이어 찾음");
                }
                else
                {
                    print("쓰레기 없음");
                }
            }
            Collider2D hitdahit = Physics2D.OverlapBox(transform.position + Vector3.up, Vector2.one,0f);
            if(hitdahit == null) return;
            if (hitdahit.TryGetComponent<Customer>(out Customer customer))
            {
                print("손님 찾음");
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
        Gizmos.DrawWireCube(transform.position+Vector3.up,new Vector2(1f,1f));
    }



    private new void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D (collision);
        if (ObjectSituation)
        {

        }
    }

}
