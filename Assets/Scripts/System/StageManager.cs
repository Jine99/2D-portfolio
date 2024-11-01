using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManger : MonoBehaviour
{
    private static StageManger instance;
    public static StageManger Instance => instance;

    private int GameLevel = 1;//���� �������� ����
    private const int MaxGameLevel = 13;//�ִ� �������� ����

    private float[] CustomerSpwan = { 100f, 100f, 90f, 90, 90f, 80f, 80f, 80f, 60f, 60f, 40f, 40f, 20f };//���������� Ȯ��

    public float CistomerChance;//���� �������� �մ� ���� ����Ȯ��

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


    void Start()
    {
        CistomerChance = CustomerSpwan[GameLevel - 1];
    }

    public void StageLevelUp()
    {
        GameLevel++;
        if (GameLevel > MaxGameLevel)
        {
            GameLevel = MaxGameLevel;
        }
        CistomerChance = CustomerSpwan[GameLevel - 1];
    }
}
