using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: �̱��� �Ŵ��� ���� �Ŵ��� ���� �ϳ��� ��ӹ���
public class UIManager : MonoBehaviour//UI�Ŵ���
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
