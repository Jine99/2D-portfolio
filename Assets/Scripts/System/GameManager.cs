using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    internal Player player;

    public TextMeshProUGUI MoneyText;//가지고있는 돈 텍스트

    internal int Gamemoney = 0;//가게 전체재정 현황



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


    private void Start()
    {
        MoneyText.text = $"$ {Gamemoney}";
    }

    public void Deposit(int Money)
    {
        Gamemoney += Money;
        MoneyText.text = $"$ {Gamemoney} ";

    }
    public bool withdrawal(int Money)
    {
        if (Gamemoney > Money)
        {
            Gamemoney -= Money;
            MoneyText.text = $"$ {Gamemoney}";
            return true;
        }
        MoneyText.text = $"$ {Gamemoney} ";
        return false;
    }

}
