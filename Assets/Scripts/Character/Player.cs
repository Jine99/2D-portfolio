using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Player : MonoBehaviour
{
    internal Dictionary<string, int> Inventory = new Dictionary<string, int>();//������ �ִ� ������Ʈ ����;
    internal bool Inventoryempty = true;//�÷��̾� �κ��丮�� ����ִ��� �ƴ���

    private int InvenSize = 4;//�ִ� �������ɰ���



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
    //�κ��丮�� ���� üũ
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

    //�κ��丮�� ���� ������ �������� �ִ��� Ȯ��
    public bool Invenadd(string Key)
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
}
