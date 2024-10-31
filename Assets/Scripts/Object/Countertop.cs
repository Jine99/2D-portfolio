using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countertop : Objects
{
    private float foodSpeed = 4f;//음식 생성속도

    private int maxfood = 6;//음식 최대보유량

    private Dictionary<string, int> food = new Dictionary<string, int>();//음식 보유량

    private float delay=0f;//음식 전달 최소시간;
    private float delayTime=0.3f;//음식전달 딜레이 설정

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
            if (food["Food"]<= 0) { print("조리대 음식 부족함"); return; }
            if (PlayerManeger.Instance.player.Invenadd("Food")) food["Food"]--;             
        }
    }

    //음식 생성 코루틴
    protected override IEnumerator ObjectCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(foodSpeed);
            if (!food.ContainsKey("Food"))
            {
                print("조리대 최초 음식생성");
                food.Add("Food", 1);
                
            }
            else if (food["Food"] == maxfood)
            {
                print("조리대 음식 가득참");
                yield return null;
            }
            else
            {
                print("조리대 음식 조리");
                food["Food"]++;
                print($"조리대 보유 음식량 : {food["Food"]}");
            }
        }

    }

}

