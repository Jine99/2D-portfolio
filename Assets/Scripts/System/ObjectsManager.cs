using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    private static ObjectsManager instance;
    public static ObjectsManager Instance => instance;

    internal List<Objects> Objects = new List<Objects>();

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else
        {
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    //���̺� ��ȭ
    /// <summary>
    /// ���̺��� �ʴ� �󸶳� ������ ������(������ +)
    /// </summary>
    /// <param name="speed"></param>
    public void UpgradeTable(int speed)
    {
        List<Table> tables = Objects.OfType<Table>().ToList();

        if (tables.Count > 0)
        {
            foreach (Table table in tables)
            {
                table.UpgradeTable(speed);
            }
        }
    }

    //������ ��ȭ
    /// <summary>
    /// 1�� ����µ� �ɸ��� �ð�(��ü),�ִ� ������ ����(������ +)
    /// </summary>
    /// <param name="CoroutineSpeed"></param>
    /// <param name="maxFood"></param>
    public void UpgradeCountertop(float Speed, int maxFood)
    {
        List<Countertop> countertops = Objects.OfType<Countertop>().ToList();

        if(countertops.Count > 0)
        {
            foreach(Countertop countertop in countertops)
            {
                countertop.UpgradeCountertop(Speed, maxFood);
            }
        }

    }

    // TODO: ��ȭ �����ؾ���. �� ������Ʈ ������� ���� �ʿ�
    /// <summary>
    /// �ʴ� �Ǹ��� ���� (��ü)
    /// </summary>
    /// <param name="slaespeed"></param>
    public void UpgradeCounter(int speed)
    {
        List<Counter> counters = Objects.OfType<Counter>().ToList();

        if (counters.Count > 0)
        {
            foreach (Counter counter in counters)
            {
                counter.UpgradeCounter(speed);
            }
        }

    }
 }
