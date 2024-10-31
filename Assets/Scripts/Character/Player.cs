using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Player : MonoBehaviour
{ 

    internal Dictionary<Enum, int> Inventory = new Dictionary<Enum, int>();//������ �ִ� ������Ʈ ����;
    internal bool Inventoryempty = true;//�÷��̾� �κ��丮�� ����ִ��� �ƴ���

    private int InvenSize = 4;//�ִ� �������ɰ���



    private void Start()
    {
        //�÷��̾ �Ŵ����� �־���
        PlayerManeger.Instance.player = this;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(x, y);
        transform.Translate(dir * Time.deltaTime);


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
    //�κ��丮�� ���� üũ
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
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Food))
        {
            Inventory[Objecttype.Food]=Inventory[Objecttype.Food]-100;
            if(Inventory[Objecttype.Food]<=0) Inventory.Clear();
            print("������ ���ȴ�");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Monmey))
        {
            Inventory[Objecttype.Monmey] = Inventory[Objecttype.Monmey] - 100;
            if (Inventory[Objecttype.Monmey] <= 0) Inventory.Clear();
            print("���� ���ȴ�");
            return;
        }
        if (Inventory.ContainsKey(Objecttype.Trash))
        {
            Inventory[Objecttype.Trash] = Inventory[Objecttype.Trash] - 100;
            if (Inventory[Objecttype.Trash] <= 0) Inventory.Clear();
            print("�����⸦ ���ȴ�");
            return;
        }
    }
    
    
    
}
