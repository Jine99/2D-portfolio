using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villain : MonoBehaviour
{
    private float Delay;//���� ���ؽð� ������
    private float VillainDelay = 3f;//���� ������


    internal bool theftVillain = false;//���� ���� true�� ��������

    void Start()
    {
        //���� �������� �Ŵ������� �־���
        VillainManager.Instance.DestroyVillain(this);

        VillainManager.Instance.villainList.Add(this);
        //���� �����̸� ���� üũ�ð� 1.5��
        if (theftVillain)
        {
            //TODO: ������ȭ �־����
            VillainDelay = 1.5f;

        }
    }

    void Update()
    {
        Playercheck();

    }


    private void Playercheck()//���� �ֺ��� �÷��̾� üũ
    {
        Collider2D[] Players = Physics2D.OverlapBoxAll(transform.position, Vector2.one * 2.8f, 0);
        if (Players == null) return;
        Player player1 = null;
        foreach (Collider2D player in Players)
        {
            if (player.TryGetComponent<Player>(out Player pp))
            {
                player1 = pp;
                Delay += 1f * Time.deltaTime;
                print(Delay);
                if (Delay > VillainDelay)
                {
                    Die();
                }
            }
            if (player1 == null)
            {
                Delay = 0;
            }
        }

    }

    public void Die()
    {
        VillainManager.Instance.villainList.Remove(this);
        if (!VillainManager.Instance.villainswitch) VillainManager.Instance.villainswitch = true;
        VillainManager.Instance.Delay1 = 0;
        VillainManager.Instance.Delay2 = 0;
        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector2.one * 2.8f);
    }


}
