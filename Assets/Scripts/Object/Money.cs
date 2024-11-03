using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int cost;//돈이 만들어질때 한번에 가지고있을 돈의 양
    

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
                print("플레이어 다른 오브젝트 소지중");
            }
        }
    }
}
