using System.Collections;
using System.Collections.Generic;
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
        if (!Villaincheck) return;
        if (villainList.Count >= 1) return;
        Collider2D collider = Physics2D.OverlapBox(position, Vector2.one * 0.6f, 0);
        if (collider.CompareTag("Customer") || collider.CompareTag("Villain") ||
            collider.CompareTag("Player")) return;
        villainDelay1 = Random.Range(30f, 45f);
        if (villainDelay1 < Delay1)
        {
            Instantiate(villain, position, Quaternion.identity);
        }


    }
    public void theftvillainSpawn(Vector2 position)
    {
        if (!Villaincheck) return;
        if (villainList.Count >= 1) return;
        Collider2D collider = Physics2D.OverlapBox(position, Vector2.one * 0.6f, 0);
        if (collider.CompareTag("Customer") || collider.CompareTag("Villain") ||
            collider.CompareTag("Player")) return;
        villainDelay2 = Random.Range(45f, 60f);
        if (villainDelay2 < Delay1)
        {
            villain vill = Instantiate(villain, position, Quaternion.identity);
            vill.theftVillain = true;

        }

    }
    public void sleepvillainSpawn(Vector2 position)
    {
        if (!Villaincheck) return;
        if (villainList.Count >= 1) return;
        Collider2D collider = Physics2D.OverlapBox(position, Vector2.one * 0.6f, 0);
        if (collider.CompareTag("Customer") || collider.CompareTag("Villain") ||
            collider.CompareTag("Player")) return;
        float probability = 20f;//���� ���� Ȯ��
        float villainprobability = Random.Range(0f, 100f);
        if (villainprobability > probability)
        {
            Instantiate(villain, position, Quaternion.identity);
        }
    }

    public IEnumerator FirstSpawn(Vector2 vector2)
    {
        yield return new WaitForSeconds(60f);
        if (!Villaincheck) yield return null;
        if (villainList.Count >= 1) yield return null;
        Collider2D collider = Physics2D.OverlapBox(vector2, Vector2.one * 0.6f, 0);
        if (collider.CompareTag("Customer") || collider.CompareTag("Villain") ||
            collider.CompareTag("Player")) yield return null;
        Instantiate(villain, vector2, Quaternion.identity);
        
    }

    public void DestroyVillain(villain vill)
    {
        if (villainList.Count >= 1)
        {
            DestroyImmediate(vill.gameObject);
        }
    }

}
