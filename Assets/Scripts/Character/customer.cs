using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//��
public class Customer : MonoBehaviour
{
    internal int CustomerFood;//�������ִ� ����

    private int MaxFood;//�� ���� �ִ밳��

    private const  int RandomCustomertype = 2;//�� ���� ������
    private int Customertype;//���� �� Ÿ��

    private void Start()
    {
        GameManager.Instance.customers.Add(this);
    }



}
