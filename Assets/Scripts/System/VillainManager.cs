using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//TODO: 싱글톤 매니저 만들어서 상속받기
public class VillainManager : MonoBehaviour
{
    private static VillainManager instance;
    public static VillainManager Instance => instance;

    public villain villain;//빌런 프리펩 받고

    internal List<villain> villainList = new List<villain>();//빌런 리스트
    internal bool Villaincheck = true;//빌런 리스트에 빌런여부


    private float villainDelay1;//무전취식 스폰 기준 딜레이
    private float villainDelay2;//절도 스폰 기준 딜레이

    internal float Delay1;//무전취식 딜레이
    internal float Delay2;//절도 딜레이

    internal bool villainswitch = false;//첫번째 빌런 삭제후 스위치 온

    public GameObject villainWarning;//빌런 출현 경고



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
        villainWarning.SetActive(false);
    }

    private void Update()
    {
        if (villainswitch)
        {
            Delay1 += Time.deltaTime;
            Delay2 += Time.deltaTime;
        }
        if (villainList.Count >= 1)
        {
            Villaincheck = false;
        }
        else if (villainList.Count <= 0)
        {
            Villaincheck = true;
        }


    }
    public void villainSpawn(Vector2 position)
    {
        if (villainswitch)
        {
            if (!Villaincheck) { print("일반 빌런 소환실패"); return; }
            if (villainList.Count >= 1) { print("빌런 꽉참 빌런 소환실패"); return; }
            Collider2D[] collider = Physics2D.OverlapBoxAll (position, Vector2.one * 0.6f, 0);
            foreach (Collider2D collider2d in collider)
            {
                if (collider2d.CompareTag("Customer") || collider2d.CompareTag("Villain") ||
                    collider2d.CompareTag("Player")) { print("자리에 누군가있다."); return; }
            }
            villainDelay1 = Random.Range(30f, 45f);
            if (villainDelay1 < Delay1)
            {
                Instantiate(villain, position, Quaternion.identity);
                villainWarning.SetActive(true);
            }
            else
            {
                print("빌런 생성 딜레이 돌아가는중");
            }
        }

    }
    public void theftvillainSpawn(Vector2 position)
    {
        if (villainswitch)
        {
            if (!Villaincheck) { print("절도빌런 소환실패"); return; }
            if (villainList.Count >= 1) { print(" 빌런꽉참 절도 빌런  소환실패"); return; }
            Collider2D[] collider = Physics2D.OverlapBoxAll(position, Vector2.one * 0.6f, 0);
            foreach (Collider2D collider2d in collider)
            {
                if (collider2d.CompareTag("Customer") || collider2d.CompareTag("Villain") ||
                    collider2d.CompareTag("Player")) { print("자리에 누군가있다."); return; }
            }
            villainDelay2 = Random.Range(45f, 60f);
            if (villainDelay2 < Delay1)
            {
                villain vill = Instantiate(villain, position, Quaternion.identity);
                vill.theftVillain = true;
                villainWarning.SetActive(true);
            }
            else
            {
                print("빌런 생성 딜레이 돌아가는중");
            }
        }
    }
    public void sleepvillainSpawn(Vector2 position, Customer customer)
    {
        Destroy(customer.gameObject);
        if (!Villaincheck) { print("꿀잠 빌런 소환실패"); return; }
        if (villainList.Count >= 1) { print("빌런 꽉참 꿀잠 빌런 소환실패"); return; }
        Collider2D[] collider = Physics2D.OverlapBoxAll(position, Vector2.one * 0.6f, 0);
        foreach (Collider2D collider2d in collider)
        {
            if (collider2d.CompareTag("Customer") || collider2d.CompareTag("Villain") ||
                collider2d.CompareTag("Player")) { print("자리에 누군가있다."); return; }
        }
        float probability = 20f;//빌런 출현 확률
        float villainprobability = Random.Range(0f, 100f);
        if (villainprobability < probability)
        {
            Instantiate(villain, position, Quaternion.identity);
            villainWarning.SetActive(true);
        }
        else
        {
            print("빌런 생성 확률 돌아가는중");
        }

    }

    public IEnumerator FirstSpawn(Vector2 vector2)
    {
        yield return new WaitForSeconds(Random.Range(60f,60.9f));
        print("첫빌런 소환시도");
        if (!Villaincheck) { print("첫 빌런 소환 실패1"); yield break; }
        if (villainList.Count >= 1) { print("첫 빌런 소환 실패2"); yield break; }
        Collider2D[] collider = Physics2D.OverlapBoxAll(vector2, Vector2.one * 0.6f, 0);
        foreach (Collider2D collider2d in collider)
        {
            if (collider2d.CompareTag("Customer") || collider2d.CompareTag("Villain") ||
                collider2d.CompareTag("Player")) { print("자리에 누군가있다."); yield break; }
        }
        Instantiate(villain, vector2, Quaternion.identity);
        villainWarning.SetActive(true);

    }

    public void DestroyVillain(villain vill)
    {
        villainList.Add(vill);
        if (villainList.Count >= 2)
        {
            print("이미 빌런있음 삭제함");
            villainList.Remove(vill);
            Destroy(vill.gameObject);
        }
        else
        {
            return;
        }
    }
    public void Die(villain vill)
    {
       villainList.Remove(vill);
        if (!villainswitch)villainswitch = true;
        Delay1 = 0;
        Delay2 = 0;
        Destroy(vill.gameObject);
        villainWarning.SetActive(false);
    }
}
