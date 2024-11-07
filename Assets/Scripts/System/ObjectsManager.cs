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

    //테이블 강화
    /// <summary>
    /// 테이블이 초당 얼마나 음식을 지울지(기존값 +)
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

    //조리대 강화
    /// <summary>
    /// 1개 만드는데 걸리는 시간(대체),최대 소지량 증가(기존값 +)
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

    // TODO: 강화 구현해야함. 돈 오브젝트 생성방식 보완 필요
    /// <summary>
    /// 초당 판매할 개수 (대체)
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
