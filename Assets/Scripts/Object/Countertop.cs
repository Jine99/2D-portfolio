using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countertop : Objects
{
    private float foodSpeed = 4f;//���� �����ӵ�

    private int maxfood = 6;//���� �ִ뺸����

    private Dictionary<string, int> food = new Dictionary<string, int>();//���� ������

    private float delay=0f;//���� ���� �ּҽð�;
    private float delayTime=0.3f;//�������� ������ ����

    private void Start()
    {
        StartCoroutine(ObjectCoroutine());
    }
    private void Update()
    {

    }
    private void foodRenderer()
    {
        if (food["Food"] > 1)
        {

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.CompareTag("villain"))
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            if (food.Count <= 0) return;
            if (food["Food"]<= 0) { print("������ ���� ������"); return; }
            if (PlayerManeger.Instance.player.Invenadd("Food")) food["Food"]--;             
        }
    }

    //���� ���� �ڷ�ƾ
    protected override IEnumerator ObjectCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(foodSpeed);
            if (!food.ContainsKey("Food"))
            {
                print("������ ���� ���Ļ���");
                food.Add("Food", 1);
                
            }
            else if (food["Food"] == maxfood)
            {
                print("������ ���� ������");
                yield return null;
            }
            else
            {
                print("������ ���� ����");
                food["Food"]++;
                print($"������ ���� ���ķ� : {food["Food"]}");
            }
        }

    }

}

