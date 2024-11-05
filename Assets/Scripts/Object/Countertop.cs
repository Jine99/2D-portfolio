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
        CoroutineSpeed = 4f;//�ڷ�ƾ ���ǵ� ����
        maxObject = 6;//���� �ִ뺸����
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
    //���� ������
    private void foodRenderer()
    {
        if (ObjectList[Objecttype.Food] > 1)
        {

        }
    }
    //������ ���Ļ��� Ʈ����
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.CompareTag("Player"))
        {
            if (ObjectSituation)
            {
                print(ObjectSituation);
                if (ObjectList.Count <= 0) return;
                if (ObjectList[Objecttype.Food] <= 0) { print("������ ���� ������"); return; }
                if (GameManager.Instance.player.Invenadd(Objecttype.Food)) ObjectList[Objecttype.Food]--;
            }
            else
            {
                print("���� ���������� ��ȣ�ۿ� �Ұ�");
                return;
            }
        }
    }
    //���� ���� �ڷ�ƾ
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
                    print("������ ���� ���Ļ���");
                    ObjectList.Add(Objecttype.Food, 1);

                }
                else if (ObjectList[Objecttype.Food] == maxObject)
                {
                    print("������ ���� ������");
                    yield return null;
                }
                else
                {
                    print("������ ���� ����");
                    ObjectList[Objecttype.Food]++;
                    print($"������ ���� ���ķ� : {ObjectList[Objecttype.Food]}");
                }
            }

        }

    }

    //TODO:������ ��ȭ ��������
}


