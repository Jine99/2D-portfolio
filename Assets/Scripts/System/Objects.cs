using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Objects : MonoBehaviour
{
    protected Dictionary<Enum, int> ObjectList = new Dictionary<Enum, int>();//오브젝트 보관 딕셔너리



    protected float Delay = 1f;//딜레이 기준
    protected float ObjectDelay = 0;//오브젝트 쿨타임

    protected int maxObject = 6;//오브젝트 최대보유량

    protected float CoroutineSpeed;//오브젝트  코루틴속도

    public bool ObjectSituation = true;//빌런 점유상태 체크

    //상속하는 스타트 코루틴
    protected void Start()
    {
        StartCoroutine(ObjectCoroutine());
    }
    //자식에서 코루틴 재정의
    protected virtual IEnumerator ObjectCoroutine()
    {
        yield return null;
    }
    //빌런 접촉시 공통 동작수행
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Villain"))
        {
            print("빌런 접촉함");
            ObjectSituation = false;
            print(ObjectSituation);
        }
    }
    //빌런이 사라질시 공통 동작수행
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Villain"))
        {
            print("빌런이 떠남");
            ObjectSituation = true;
            print(ObjectSituation);
        }
    }

}

