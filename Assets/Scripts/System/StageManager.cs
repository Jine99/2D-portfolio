using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManger : MonoBehaviour
{
    private static StageManger instance;
    public static StageManger Instance => instance;

    private int GameLevel = 1;//현재 스테이지 레벨
    private const int MaxGameLevel = 13;//최대 스테이지 레벨

    private float[] CustomerSpwan = { 100f, 100f, 90f, 90, 90f, 80f, 80f, 80f, 60f, 60f, 40f, 40f, 20f };//스테이지별 확률

    internal float CistomerChance;//현재 스테이지 손님 유형 스폰확률

    public TextMeshProUGUI StageLevel;//표시할 스테이지 레벨 텍스트

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
        StageLevel.text = $"{GameLevel}"; 
    }

    public void StageLevelUp()
    {
        GameLevel++;
        if (GameLevel > MaxGameLevel)
        {
            GameLevel = MaxGameLevel;

        }
        StageLevel.text = $"{GameLevel}";
        CistomerChance = CustomerSpwan[GameLevel - 1];
    }
}
