using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int cost;//���� ��������� �ѹ��� ���������� ���� ��
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if(collision.TryGetComponent<Player>(out Player player))
        {
            if (GameManager.Instance.player.Invenadd(Objecttype.Money, cost))
            {
                Destroy(gameObject);
            }
            else 
            {
                print("�÷��̾� �ٸ� ������Ʈ ������");
            }
        }
    }
}
