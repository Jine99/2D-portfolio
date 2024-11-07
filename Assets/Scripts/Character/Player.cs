using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.WSA;

//����
public class Player : MonoBehaviour
{

    internal Dictionary<Enum, int> Inventory = new Dictionary<Enum, int>();//������ �ִ� ������Ʈ ����;
    internal bool Inventoryempty = true;//�÷��̾� �κ��丮�� ����ִ��� �ƴ���

    private int InvenSize = 4;//�ִ� �������ɰ���
    private int walletSize = 40;//���� ������(�������༭ ���Ƿ� ����)

    private float InputDelay;//Ű�Է� ������ �ð�
    private float Delay = 0.5f;//Ű�Է� ������ ���ؽð�

    //������ ������Ʈ ������
    public GameObject Trash;
    public GameObject Money;
    public GameObject Food;

    private void Start()
    {
        //�÷��̾ �Ŵ����� �־���
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
                    print("������Ʈ ã��");
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
    //�κ��丮�� �߰�
    public bool Invenadd(Enum Key)
    {
        if (Invencheck(Key)) return false;
        if (!Inventory.ContainsKey(Key))
        {
            Inventory.Clear();
            print($"�÷��̾� {Key} ����");
            Inventory.Add(Key, 1);
            return true;
        }
        else if (Inventory.ContainsKey(Key))
        {
            print($"�÷��̾� {Key}���߰�");
            Inventory[Key]++;
            print($"�÷��̾� ������ : {Inventory[Key]}");
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
            print($"�÷��̾� {Key} ����");
            Inventory.Add(Key, value);
            return true;
        }
        else if (Inventory.ContainsKey(Key))
        {
            print($"�÷��̾� {Key}���߰�");
            Inventory[Key] += value;
            print($"�÷��̾� ������ : {Inventory[Key]}");
            return true;

        }
        return false;
    }
    //�κ��丮 ��á���� üũ
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
                    print("���� ������");
                    return true;
                }
            }
            if (Inventory[Key] < InvenSize)
            {
                return false;
            }
            else
            {
                print($"�÷��̾� �κ��丮 ������");
                return true;
            }
        }
        if (!Inventory.ContainsKey(Key))
        {
            return true;
        }
        return false;
    }
    //�κ��丮 üũ�� �������뿡 ������ ������
    public void InvenTrash()
    {
        if (Inventory.Count <= 0)
        {
            print("���� ������Ʈ�� ����");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Food))
        {
            Inventory[Objecttype.Food] -= 100;
            if (Inventory[Objecttype.Food] <= 0) Inventory.Clear();
            print("������ ���ȴ�");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Money))
        {
            Inventory[Objecttype.Money] -= 100;
            if (Inventory[Objecttype.Money] <= 0) Inventory.Clear();
            print("���� ���ȴ�");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Trash))
        {
            Inventory[Objecttype.Trash] -= 10;
            if (Inventory[Objecttype.Trash] <= 0) Inventory.Clear();
            print("�����⸦ ���ȴ�");
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
