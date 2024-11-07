using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
//TODO: 싱글톤 매니저 만들어서 매니저 전부 하나로 상속받자
public class UIManager : MonoBehaviour//UI매니저
{
    private static UIManager instance;
    public static UIManager Instance => instance;


    private bool Escapebool = false;//esc를 눌렀으면 true

    private bool[] ifupgrade = { true, true, true, true, true, true };//업그레이드여부 확인 

    public GameObject PausePanel;//퍼즈하면 나오는 패널
    public GameObject UpgradePanel;// 업그레이드 패널
    public GameObject Pause;//상단 이미지

    public RectTransform Border;//테두리가 될녀석

    public RectTransform[] Pausetargets;//퍼즈 이미지 위치


    public RectTransform[] UpgradePanelImages;//업그레이드 이미지 위치

    private int IndexKey=0;//현재 선택중인커서 위치

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
        Pause.SetActive(true);
        UpgradePanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    private void BorderPosition(RectTransform target)
    {
        Border.gameObject.SetActive(true);
        Border.SetParent(target);
        //Border.anchorMax = target.anchorMax;
        //Border.anchorMin = target.anchorMin;
        Border.sizeDelta=target.sizeDelta;
        Border.pivot = target.pivot;
        Border.transform.localPosition = Vector3.zero;
        //Border.anchoredPosition=target.anchoredPosition;

    }

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            OnPause();
        }
        if (PausePanel.activeSelf)
        {
            Pauseselect();
        }
        if (UpgradePanel.activeSelf)
        {
            Upgradeselect();
        }

    }
    private void OnPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
                IndexKey = 0;
                BorderPosition(Pausetargets[IndexKey]);
                PausePanel.SetActive(true);
            }

        }
    }

    private void Pauseselect()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

            IndexKey = Mathf.Max(IndexKey-1, 0);
            BorderPosition(Pausetargets[IndexKey]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            IndexKey = Mathf.Min(IndexKey + 1, Pausetargets.Length-1);
            BorderPosition(Pausetargets[IndexKey]);
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Border.SetParent(this.transform);
            CallPause();
        }
    }
    private void CallPause()
    {
        switch (IndexKey)
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

    public void OnUpgradePanel()
    {
        Time.timeScale = 0f;
        //Pause.SetActive(false);
        UpgradePanel.SetActive(true);
        IndexKey = 0;
        BorderPosition(UpgradePanelImages[IndexKey]);

    }

    private void Upgradeselect()
    {
        int MaxIndex=UpgradePanelImages.Length-1;


        
            if (Input.GetKeyDown(KeyCode.W))
            {
                if(IndexKey >= 3)
                {
                    IndexKey -= 3;
                    BorderPosition((UpgradePanelImages[IndexKey]));
                }
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                if((IndexKey+3<= MaxIndex))
                {
                    IndexKey += 3;
                    BorderPosition(UpgradePanelImages[(IndexKey)]);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                IndexKey = Mathf.Max(IndexKey - 1, 0);
                BorderPosition((UpgradePanelImages[IndexKey]));
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                IndexKey = Mathf.Min(IndexKey+1,MaxIndex);
                BorderPosition((UpgradePanelImages[IndexKey]));
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                CallUpgrade();
            }
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                UpgradePanel.SetActive(false );
                Pause.SetActive(true) ;
                Time.timeScale = 1f;
                return;
            }
            
        

    }
    private void CallUpgrade()
    {
        if (!ifupgrade[IndexKey]) return;
        switch (IndexKey)
        {
            case 0:
                if (GameManager.Instance.withdrawal(300))
                {                    
                    GameManager.Instance.player.MovespeedUp(2);
                    StageManger.Instance.StageLevelUp();
                    ifupgrade[IndexKey] = false;
                }
                break;
            case 1:
                if (GameManager.Instance.withdrawal(300))
                {                   
                    GameManager.Instance.player.InvenSizeUp(6);
                    StageManger.Instance.StageLevelUp();
                    ifupgrade[IndexKey] = false;
                }
                break;
            case 2:
                if (GameManager.Instance.withdrawal(300))
                {                   
                    GameManager.Instance.player.VillaindEvictUp(2);
                    StageManger.Instance.StageLevelUp();
                    ifupgrade[IndexKey] = false;
                }
                break;
            case 3:
                if (GameManager.Instance.withdrawal(200))
                {                   
                    ObjectsManager.Instance.UpgradeCountertop(2f, 4);
                    StageManger.Instance.StageLevelUp();
                    ifupgrade[IndexKey] = false;
                }
                break;
            case 4:
                if (GameManager.Instance.withdrawal(500))
                {                   
                    ObjectsManager.Instance.UpgradeCounter(10);
                    StageManger.Instance.StageLevelUp();
                    ifupgrade[IndexKey] = false;
                }
                break;
            case 5:
                if (GameManager.Instance.withdrawal(300))
                {                   
                    ObjectsManager.Instance.UpgradeTable(2);
                    StageManger.Instance.StageLevelUp();
                    ifupgrade[IndexKey] = false;
                }
                break;
        }

    }


}
