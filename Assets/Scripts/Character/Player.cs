using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.WSA;

//유저
public class Player : MonoBehaviour
{

    internal Dictionary<Enum, int> Inventory = new Dictionary<Enum, int>();//가지고 있는 오브젝트 종류;
    internal bool Inventoryempty = true;//플레이어 인벤토리가 비어있는지 아닌지

    private int InvenSize = 4;//최대 소지가능개수
    private int walletSize = 40;//지갑 사이즈(안정해줘서 임의로 정함)

    private float InputDelay;//키입력 딜레이 시간
    private float Delay = 0.5f;//키입력 딜레이 기준시간

    //보여줄 오브젝트 랜더링
    public GameObject Trash;
    public GameObject Money;
    public GameObject Food;

    private void Start()
    {
        //플레이어를 매니저에 넣어줌
        GameManager.Instance.player = this;



    }

    private void Update()
    {
        PlayerMove();

        if (Inventory.ContainsValue(0)) Inventory.Clear();
        ObjectRender();
    }


    private void ObjectRender()
    {
        Trash.SetActive(false);
        Money.SetActive(false);
        Food.SetActive(false);
        if (Inventory.Count <= 0) return;
        if (Inventory.ContainsKey(Objecttype.Trash))
        {
            Trash.SetActive(true);
        }
        else if (Inventory.ContainsKey(Objecttype.Money))
        {
            Money.SetActive(true);
        }
        else if (Inventory.ContainsKey(Objecttype.Food))
        {
            Food.SetActive(true);
        }
        else
        {
            return;
        }


    }

    public void PlayerMove()
    {
        if (Time.time - InputDelay < Delay) return;
        if(Time.timeScale == 0f ) return;
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector3.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector3.down;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector3.left;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector3.right;

        }
        if (direction != Vector3.zero)
        {
            bool move = true;
            Collider2D[] collision2D = Physics2D.OverlapCircleAll(transform.position + direction, 0.35f);
            foreach (Collider2D collision in collision2D)
            {
                if (collision.CompareTag("Object") || collision.CompareTag("Customer") || collision.CompareTag("Villain"))
                {
                    move = false;
                    print("오브젝트 찾음");
                }

            }
            if (move)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + direction, 1f);
                InputDelay = Time.time;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, 0.35f);
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
    public bool Invenadd(Enum Key, int value)
    {
        if (Invencheck(Key)) return false;
        if (!Inventory.ContainsKey(Key))
        {
            Inventory.Clear();
            print($"플레이어 {Key} 생성");
            Inventory.Add(Key, value);
            return true;
        }
        else if (Inventory.ContainsKey(Key))
        {
            print($"플레이어 {Key}에추가");
            Inventory[Key] += value;
            print($"플레이어 소지량 : {Inventory[Key]}");
            return true;

        }
        return false;
    }
    //인벤토리 꽉찼는지 체크
    public bool Invencheck(Enum Key)
    {
        if (Inventory.Count == 0)
        {
            return false;
        }
        if (Inventory.ContainsKey(Key))
        {
            if (Inventory.ContainsKey(Objecttype.Money))
            {
                if (Inventory[Key]< walletSize)
                {
                    return false;
                }
                else
                {
                    print("지갑 가득참");
                    return true;
                }
            }
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
            print("버릴 오브젝트가 없다");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Food))
        {
            Inventory[Objecttype.Food] -= 100;
            if (Inventory[Objecttype.Food] <= 0) Inventory.Clear();
            print("음식을 버렸다");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Money))
        {
            Inventory[Objecttype.Money] -= 100;
            if (Inventory[Objecttype.Money] <= 0) Inventory.Clear();
            print("돈을 버렸다");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Trash))
        {
            Inventory[Objecttype.Trash] -= 10;
            if (Inventory[Objecttype.Trash] <= 0) Inventory.Clear();
            print("쓰레기를 버렸다");
            return;
        }
    }

    public void MoneyDeposit(Enum Key)
    {
        if (Inventory.Count == 0) return;
        if (!Inventory.ContainsKey(Key)) return;
        if (Inventory.ContainsKey(Objecttype.Money))
        {
            if (Inventory[Objecttype.Money] >= 100)
            {
                GameManager.Instance.Deposit(100);
                Inventory[Objecttype.Money] -= 100;
                if (Inventory[Objecttype.Money] <= 0) Inventory.Clear();
            }
            else if (Inventory[Objecttype.Money] < 100)
            {
                GameManager.Instance.Deposit(Inventory[Objecttype.Money]);
                Inventory.Clear();
            }

        }
        return;
    }


}
