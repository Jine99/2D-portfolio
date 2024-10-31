using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Player : MonoBehaviour
{
    internal Dictionary<string, int> Inventory = new Dictionary<string, int>();//가지고 있는 오브젝트 종류;
    internal bool Inventoryempty = true;//플레이어 인벤토리가 비어있는지 아닌지

    private int InvenSize = 4;//최대 소지가능개수



    private void Start()
    {
        PlayerManeger.Instance.player = this;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(x, y);
        transform.Translate(dir * Time.deltaTime);


    }
    //인벤토리의 상태 체크
    public bool Invencheck(string Key)
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

    //인벤토리에 같은 유형의 아이템이 있는지 확인
    public bool Invenadd(string Key)
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
}
