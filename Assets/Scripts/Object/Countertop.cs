using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Countertop : Objects
{

    private new void Start()
    {
        CoroutineSpeed = 4f;//코루틴 스피드 설정
        maxObject = 6;//음식 최대보유량
        base.Start();
    }
    private void Update()
    {

    }
    //음식 랜더링
    private void foodRenderer()
    {
        if (ObjectList[Objecttype.Food] > 1)
        {

        }
    }
    //조리대 음식생성 트리거
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D (collision);
        if (collision.CompareTag("Player"))
        {
            if (ObjectSituation)
            {
                print(ObjectSituation);
                if (ObjectList.Count <= 0) return;
                if (ObjectList[Objecttype.Food] <= 0) { print("조리대 음식 부족함"); return; }
                if (GameManager.Instance.player.Invenadd(Objecttype.Food)) ObjectList[Objecttype.Food]--;
            }
            else
            {
                print("빌런 접촉중으로 상호작용 불가");
                return;
            }
        }
    }
    //음식 생성 코루틴
    protected override IEnumerator ObjectCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(CoroutineSpeed);

            if (ObjectSituation)
            {

                print(ObjectSituation);
                if (!ObjectList.ContainsKey(Objecttype.Food))
                {
                    print("조리대 최초 음식생성");
                    ObjectList.Add(Objecttype.Food, 1);

                }
                else if (ObjectList[Objecttype.Food] == maxObject)
                {
                    print("조리대 음식 가득참");
                    yield return null;
                }
                else
                {
                    print("조리대 음식 조리");
                    ObjectList[Objecttype.Food]++;
                    print($"조리대 보유 음식량 : {ObjectList[Objecttype.Food]}");
                }
            }

        }

    }
}


