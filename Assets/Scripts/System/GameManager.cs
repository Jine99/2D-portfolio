using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    internal Player player;

    internal int Gamemoney;//���� ��ü���� ��Ȳ

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
        
    }


    public void CutomerRemove()
    {

    }

}
