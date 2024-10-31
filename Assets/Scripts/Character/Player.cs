using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Player : MonoBehaviour
{ 

    internal Dictionary<Enum, int> Inventory = new Dictionary<Enum, int>();//가지고 있는 오브젝트 종류;
    internal bool Inventoryempty = true;//플레이어 인벤토리가 비어있는지 아닌지

    private int InvenSize = 4;//최대 소지가능개수



    private void Start()
    {
        //플레이어를 매니저에 넣어줌
        PlayerManeger.Instance.player = this;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(x, y);
        transform.Translate(dir * Time.deltaTime);


    }
    //인벤토리에 추가
    public bool Invenadd(Enum Key)
    {
        if (Invencheck(Key)) return false;
        if (!Inventory.ContainsKey(Key))
        {
            Inventory.Clear();
            print($"플레이어 {Key} 생성");
            Inventory.Add(Key, 1);
            return true;
        }
        else if (Inventory.ContainsKey(Key))
        {
            print($"플레이어 {Key}에추가");
            Inventory[Key]++;
            print($"플레이어 소지량 : {Inventory[Key]}");
            return true;

        }
        return false;
    }
    //인벤토리의 상태 체크
    public bool Invencheck(Enum Key)
    {
        if (Inventory.Count == 0)
        {
            return false;
        }
        if (Inventory.ContainsKey(Key))
        {
            if (Inventory[Key] < InvenSize)
            {
                return false;
            }
            else
            {
                print($"플레이어 인벤토리 가득참");
                return true;
            }
        }
        if (!Inventory.ContainsKey(Key))
        {
            return true;
        }
        return false;
    }
    //인벤토리 체크후 쓰레기통에 아이템 버리기
    public void InvenTrash()
    {
        if (Inventory.Count <= 0)
        {
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Food))
        {
            Inventory[Objecttype.Food]=Inventory[Objecttype.Food]-100;
            if(Inventory[Objecttype.Food]<=0) Inventory.Clear();
            print("음식을 버렸다");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Monmey))
        {
            Inventory[Objecttype.Monmey] = Inventory[Objecttype.Monmey] - 100;
            if (Inventory[Objecttype.Monmey] <= 0) Inventory.Clear();
            print("돈을 버렸다");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Trash))
        {
            Inventory[Objecttype.Trash] = Inventory[Objecttype.Trash] - 100;
            if (Inventory[Objecttype.Trash] <= 0) Inventory.Clear();
            print("쓰레기를 버렸다");
            return;
        }
    }
    
    
    
}
