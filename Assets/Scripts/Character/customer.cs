using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//고객
public class Customer : MonoBehaviour
{
    internal int CustomerFood = 0;//가지고있는 음식

    internal int MaxFood = 4;//고객 음식 최대개수

    private Table Table;//이동해야할 테이블 위치

    private float CustomerDelay;//딜레이 시간
    private float Delay = 0.5f;// 딜레이 기준시간

    private bool move = true;//플레이어 움직임 가능여부

    public GameObject[] Foods;//플레이어 음식 랜더링
    private bool FoodRendererswitch = true;//랜더 스위치

    private Vector3 Vec;

    private void Start()
    {
        foreach(GameObject Food in Foods)
        {
            Food.SetActive(false);
        }
    }
    private void Update()
    {
        FoodRender();
        FindTable();
        if (CustomerFood == MaxFood)
        {
            if (Table?.ObjectSituation == false)
            {
                Table = null;
            }
            if (Table?.Trash > 0)
            {
                Table = null;
            }
            Customermove();
        }

    }
    //음식 랜더링
    private void FoodRender()
    {
        if (CustomerFood > 0)
        {
            if (FoodRendererswitch)
            {
                int select = Random.Range(0, Foods.Length);
                Foods[select].SetActive(true);
                FoodRendererswitch = false;
            }
            return;
        }
        else
        {
            foreach (GameObject Food in Foods)
            {
                Food.SetActive(false);
            }
            FoodRendererswitch = true;
        }

    }


    public void CustomerFoodSell()
    {
        CustomerFood++;
        print($"고객 현재 음식 : {CustomerFood}");

    }
    public void Foodeat()
    {
        CustomerFood--;
        if (CustomerFood <= 0)
        {
            Table.TableReserved = true;

            VillainManager.Instance.sleepvillainSpawn(transform.position,this);
            
        }
    }
    public void Customermove()
    {
        print("움직일 준비중");
        if (Table == null) return;
        if (transform.position.y == Table.transform.position.y + 1f) return;
        while (transform.position.y != Table.transform.position.y + 2f)
        {
            print("움직임 1번");
            if (Time.time - CustomerDelay < Delay) break;
            Vector3 direction = new Vector3(transform.position.x, Table.transform.position.y + 2f);
            Vec = direction;
            movecheck(direction);
            if (move)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Table.transform.position.y + 2f), 1f);
            }
            CustomerDelay = Time.time;
        }
        while (transform.position.x != Table.transform.position.x)
        {
            print("움직임 2번");
            if (Time.time - CustomerDelay < Delay) break;
            Vector3 direction = new Vector3(Table.transform.position.x, transform.position.y);
            Vec = direction;
            movecheck(direction);
            if (move)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Table.transform.position.x, transform.position.y), 1f);
            }
            CustomerDelay = Time.time;
        }
        while (transform.position.y != Table.transform.position.y + 1f)
        {
            print("움직임 3번");
            if (Time.time - CustomerDelay < Delay) break;
            Vector3 direction = new Vector3(transform.position.x, Table.transform.position.y + 1f);
            Vec = direction;
            movecheck(direction);
            if (move)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Table.transform.position.y + 1f), 1f);
            }
            CustomerDelay = Time.time;
            return;
        }
        print("도착!");
    }
    public void FindTable()
    {
        if (Table != null) return;
        Table[] tables = FindObjectsOfType<Table>();
        Table Tabletrans = null;
        float mindistance = Mathf.Infinity;

        foreach (Table table in tables)
        {
            if (table.TableReserved && table.tablesituation() && table.Trash <= 0)
            {
                float distance = Vector3.Distance(transform.position, table.transform.position);
                if (distance < mindistance)
                {
                    mindistance = distance;
                    Tabletrans = table;
                }
            }
        }
        if (Tabletrans == null) return;
        if (Tabletrans != null)
        {
            Table = Tabletrans;
            Table.TableReserved = false;
        }
        Debug.Log($"테이블 찾음 {Table.name}");
    }


    private void movecheck(Vector3 direction)
    {
        move = true;
        Vector3 vector3 = transform.position - (transform.position - new Vector3(direction.x, direction.y)).normalized;
        Vec = vector3;
        Collider2D[] collision2D = Physics2D.OverlapCircleAll(vector3, 0.35f);
        foreach (Collider2D collision in collision2D)
        {
            if (collision.CompareTag("Object") || collision.CompareTag("Villain") || collision.CompareTag("Player") || collision.CompareTag("Customer"))
            {
                move = false;
                print("오브젝트 찾음");
            }

        }


    }

}
