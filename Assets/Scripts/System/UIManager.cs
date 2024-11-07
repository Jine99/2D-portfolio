using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
//TODO: 싱글톤 매니저 만들어서 매니저 전부 하나로 상속받자
public class UIManager : MonoBehaviour//UI매니저
{
    private UIManager instance;
    public UIManager Instance => instance;


    private bool Escapebool = false;//esc를 눌렀으면 true

    public GameObject PausePanel;//퍼즈하면 나오는 패널

    public RectTransform Border;//테두리가 될녀석

    public RectTransform[] Pausetargets;//퍼즈 이미지 위치



    private int point=0;//현재 선택중인커서 위치

    //private RectTransform Bordertarget1;
    //private RectTransform Bordertarget2;


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
        //GameObject text1 = GameObject.Find("Gamestart");
        //GameObject text2 = GameObject.Find("Gameexit");
        
        //Bordertarget1 = text1.GetComponent<RectTransform>();
        //Bordertarget2 = text2.GetComponent<RectTransform>();

        Border.gameObject.SetActive(false);


        //Position(Border, Bordertarget2);

        //람다로 찾는법
        //FindObjectsOfType<TextMeshProUGUI>(true).ToList().Find(item => (item.name == "Gameexit"));

        //꺼져도 찾는법

        //TextMeshProUGUI[] objs = FindObjectsOfType<TextMeshProUGUI>(true);
        //for (int i = 0; i < objs.Length; i++)
        //{
        //    if (objs[i].name == "Gameexit")
        //    {
        //        text2 = objs[i].gameObject;
        //    }
        //}
        PausePanel.SetActive(false);
    }

    private void Position(RectTransform my,RectTransform target)
    {
        Border.gameObject.SetActive(true);
        my.SetParent(target);
        my.sizeDelta=target.sizeDelta;
        my.pivot = target.pivot;
        my.transform.localPosition = Vector3.zero;
        //my.anchoredPosition=target.anchoredPosition;

    }

    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.Escape))
        {
            //그냥 사용
            //Escapebool = !Escapebool;
            //XOR연산자 사용
            //Escapebool ^= true;
            //삼항 연산자 사용
            Escapebool = Escapebool ? false : true;
            if (Escapebool)
            {
                Time.timeScale = 0f;
                point = 0;
                Position(Border, Pausetargets[point]);
                PausePanel.SetActive(true);
            }

        }
        if (Escapebool)
        {
            Pauseselect();
        }

    }

    private void Pauseselect()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

            point = Mathf.Max(point-1, 0);
            Position(Border, Pausetargets[point]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            point = Mathf.Min(point + 1, Pausetargets.Length-1);
            Position(Border, Pausetargets[point]);
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Border.SetParent(this.transform);
            callPausePanel();
        }
    }
    private void callPausePanel()
    {
        switch (point)
        {
            case 0:
                Time.timeScale = 1f;
                Border.gameObject.SetActive(false);
                PausePanel.SetActive(false);
                break;
            case 1:
                Application.Quit();
                break;
            default:
                break;
        }
    }

    public void UpgradePanel()
    {

    }



}
