using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//고객
public class Customer : MonoBehaviour
{
    internal int CustomerFood;//가지고있는 음식

    private int MaxFood;//고객 음식 최대개수

    private const  int RandomCustomertype = 2;//고객 종류 가지수
    private int Customertype;//현재 고객 타입

    private void Start()
    {
        GameManager.Instance.customers.Add(this);
    }



}
