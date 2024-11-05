using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//��
public class Customer : MonoBehaviour
{
    internal int CustomerFood = 0;//�������ִ� ����

    internal int MaxFood = 4;//�� ���� �ִ밳��

    private Table Table;//�̵��ؾ��� ���̺� ��ġ

    private float CustomerDelay;//������ �ð�
    private float Delay = 0.5f;// ������ ���ؽð�

    private bool move = true;//�÷��̾� ������ ���ɿ���

    private Vector3 Vec;

    private void Start()
    {

    }
    private void Update()
    {
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

    public void CustomerFoodSell()
    {
        CustomerFood++;
        print($"�� ���� ���� : {CustomerFood}");

    }
    public void Foodeat()
    {
        CustomerFood--;
        if (CustomerFood <= 0)
        {
            Table.TableReserved = true;
            VillainManager.Instance.sleepvillainSpawn(transform.position);
            Destroy(gameObject);
        }
    }
    public void Customermove()
    {
        print("������ �غ���");
        if (Table == null) return;
        if (transform.position.y == Table.transform.position.y + 1f) return;
        while (transform.position.y != Table.transform.position.y + 2f)
        {
            print("������ 1��");
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
            print("������ 2��");
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
            print("������ 3��");
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
        print("����!");
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
        Debug.Log($"���̺� ã�� {Table.name}");
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
                print("������Ʈ ã��");
            }

        }


    }
    private void OnDrawGizmos()
    {
        if (Vec == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(Vec.x, Vec.y), 0.35f);
    }
}
