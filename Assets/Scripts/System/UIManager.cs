using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: 싱글톤 매니저 만들어서 매니저 전부 하나로 상속받자
public class UIManager : MonoBehaviour//UI매니저
{
    private UIManager instance;
    public UIManager Instance => instance;

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




}
