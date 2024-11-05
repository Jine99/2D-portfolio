using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Countertop : Objects
{
    private Vector3 myvec3;

    private Vector2[] myvec2 = new Vector2[6];

    private new void Start()
    {
        CoroutineSpeed = 4f;//코루틴 스피드 설정
        maxObject = 6;//음식 최대보유량
        base.Start();
        myvec3 = transform.position;
        myvec2[0] = new Vector3(myvec3.x - 1, myvec3.y);
        myvec2[1] = new Vector3(myvec3.x - 1, myvec3.y - 1);
        myvec2[2] = new Vector3(myvec3.x, myvec3.y - 1);
        myvec2[3] = new Vector3(myvec3.x + 1, myvec3.y - 1);
        myvec2[4] = new Vector3(myvec3.x + 2, myvec3.y);
        myvec2[5] = new Vector3(myvec3.x + 2, myvec3.y - 1);
        StartCoroutine(VillainManager.Instance.FirstSpawn(myvec2[Random.Range(0,myvec2.Length)]));

    }
    private void Update()
    {
        int a = Random.Range(0, myvec2.Length);

        VillainManager.Instance.villainSpawn(myvec2[a]);


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
        base.OnTriggerStay2D(collision);
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

    //TODO:조리대 강화 만들어야함
}


