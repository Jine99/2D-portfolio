using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//��
public class Customer : MonoBehaviour
{
    internal int CustomerFood=0;//�������ִ� ����

    internal int MaxFood=4;//�� ���� �ִ밳��

    private const  int RandomCustomertype = 2;//�� ���� ������
    private int Customertype;//���� �� Ÿ��

    private void Start()
    {

    }

    public void CustomerFoodSell()
    {
            CustomerFood++;
            print($"�� ���� ���� : {CustomerFood}");
        
    }


}
