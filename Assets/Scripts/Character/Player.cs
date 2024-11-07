using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements.Experimental;


//유저
public class Player : MonoBehaviour
{

    internal Dictionary<Enum, int> Inventory = new Dictionary<Enum, int>();//가지고 있는 오브젝트 종류;
    internal bool Inventoryempty = true;//플레이어 인벤토리가 비어있는지 아닌지

    private int InvenSize = 4;//최대 소지가능개수
    private int walletSize;//지갑 사이즈(안정해줘서 임의로 정함)

    private float InputDelay;//키입력 딜레이 시간
    private int Moveblock = 2;//초당 움직일 블럭 개수
    private float MoveDelay;//키입력 딜레이 기준시간

    internal float VillaindEvict = 1f;//절도빌런 퇴치속도 배율 1이면 그대로

    //보여줄 오브젝트 랜더링
    public GameObject Trash;
    public GameObject Money;
    public GameObject Food;

    private void Start()
    {

        walletSize = InvenSize * 10;//오브젝트간 소지량 차이가있어 임으로 맞춰줌
        MoveDelay = 1f/Moveblock;//1초에 몇블럭을 움직일지
        //플레이어를 매니저에 넣어줌
        GameManager.Instance.player = this;
    }

    private void Update()
    {
        PlayerMove();

        if (Inventory.ContainsValue(0)) Inventory.Clear();
        ObjectRender();
    }

    //소지하고있는 오브젝트 타입따라서 랜더링
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
    //플레이어 움직임 매커니즘 딜레이마다 1칸씩 이동
    public void PlayerMove()
    {
        if (Time.time - InputDelay < MoveDelay) return;
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
    //인벤토리추가 오버로딩
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
    //금고에 플레이어가 가진 돈을 넣기
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
    /// <summary>
    /// 인벤토리가 몇칸더 증가하고싶은지 업그레이드 반영(기존값 +)
    /// </summary>
    /// <param name="Upsize"></param>
    public void InvenSizeUp(int Upsize)
    {
        InvenSize = InvenSize + Upsize;
        walletSize = InvenSize * 10;
    }
    /// <summary>
    /// 초당 몇블럭을 더 움직이고싶은지 업그레이드 반영(기존값 +)
    /// </summary>
    /// <param name="block"></param>
    public void MovespeedUp(int block)
    {
        Moveblock = Moveblock + block;
        MoveDelay = 1f / Moveblock;
    }
    /// <summary>
    ///    절도빌런 퇴치 배속 scale배 만큼 빨라짐 ex) scale = 3  3배 빨라진 0.3f 적용
    /// </summary>
    /// <param name="scale"></param>
    public void VillaindEvictUp(int scale)
    {
        VillaindEvict = 1f / scale;
    }

}
