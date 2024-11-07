using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//TODO: �̱��� �Ŵ��� ���� ��ӹޱ�
public class VillainManager : MonoBehaviour
{
    private static VillainManager instance;
    public static VillainManager Instance => instance;

    public villain villain;//���� ������ �ް�

    internal List<villain> villainList = new List<villain>();//���� ����Ʈ
    internal bool Villaincheck = true;//���� ����Ʈ�� ��������


    private float villainDelay1;//������� ���� ���� ������
    private float villainDelay2;//���� ���� ���� ������

    internal float Delay1;//������� ������
    internal float Delay2;//���� ������

    internal bool villainswitch = false;//ù��° ���� ������ ����ġ ��

    public GameObject villainWarning;//���� ���� ���



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
            if (!Villaincheck) { print("�Ϲ� ���� ��ȯ����"); return; }
            if (villainList.Count >= 1) { print("���� ���� ���� ��ȯ����"); return; }
            Collider2D[] collider = Physics2D.OverlapBoxAll (position, Vector2.one * 0.6f, 0);
            foreach (Collider2D collider2d in collider)
            {
                if (collider2d.CompareTag("Customer") || collider2d.CompareTag("Villain") ||
                    collider2d.CompareTag("Player")) { print("�ڸ��� �������ִ�."); return; }
            }
            villainDelay1 = Random.Range(30f, 45f);
            if (villainDelay1 < Delay1)
            {
                Instantiate(villain, position, Quaternion.identity);
                villainWarning.SetActive(true);
            }
            else
            {
                print("���� ���� ������ ���ư�����");
            }
        }

    }
    public void theftvillainSpawn(Vector2 position)
    {
        if (villainswitch)
        {
            if (!Villaincheck) { print("�������� ��ȯ����"); return; }
            if (villainList.Count >= 1) { print(" �������� ���� ����  ��ȯ����"); return; }
            Collider2D[] collider = Physics2D.OverlapBoxAll(position, Vector2.one * 0.6f, 0);
            foreach (Collider2D collider2d in collider)
            {
                if (collider2d.CompareTag("Customer") || collider2d.CompareTag("Villain") ||
                    collider2d.CompareTag("Player")) { print("�ڸ��� �������ִ�."); return; }
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
                print("���� ���� ������ ���ư�����");
            }
        }
    }
    public void sleepvillainSpawn(Vector2 position, Customer customer)
    {
        Destroy(customer.gameObject);
        if (!Villaincheck) { print("���� ���� ��ȯ����"); return; }
        if (villainList.Count >= 1) { print("���� ���� ���� ���� ��ȯ����"); return; }
        Collider2D[] collider = Physics2D.OverlapBoxAll(position, Vector2.one * 0.6f, 0);
        foreach (Collider2D collider2d in collider)
        {
            if (collider2d.CompareTag("Customer") || collider2d.CompareTag("Villain") ||
                collider2d.CompareTag("Player")) { print("�ڸ��� �������ִ�."); return; }
        }
        float probability = 20f;//���� ���� Ȯ��
        float villainprobability = Random.Range(0f, 100f);
        if (villainprobability < probability)
        {
            Instantiate(villain, position, Quaternion.identity);
            villainWarning.SetActive(true);
        }
        else
        {
            print("���� ���� Ȯ�� ���ư�����");
        }

    }

    public IEnumerator FirstSpawn(Vector2 vector2)
    {
        yield return new WaitForSeconds(Random.Range(60f,60.9f));
        print("ù���� ��ȯ�õ�");
        if (!Villaincheck) { print("ù ���� ��ȯ ����1"); yield break; }
        if (villainList.Count >= 1) { print("ù ���� ��ȯ ����2"); yield break; }
        Collider2D[] collider = Physics2D.OverlapBoxAll(vector2, Vector2.one * 0.6f, 0);
        foreach (Collider2D collider2d in collider)
        {
            if (collider2d.CompareTag("Customer") || collider2d.CompareTag("Villain") ||
                collider2d.CompareTag("Player")) { print("�ڸ��� �������ִ�."); yield break; }
        }
        Instantiate(villain, vector2, Quaternion.identity);
        villainWarning.SetActive(true);

    }

    public void DestroyVillain(villain vill)
    {
        villainList.Add(vill);
        if (villainList.Count >= 2)
        {
            print("�̹� �������� ������");
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
