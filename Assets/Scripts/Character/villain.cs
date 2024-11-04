using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villain : MonoBehaviour
{
    private float Delay;//ºô·± ±âÁØ½Ã°£ µô·¹ÀÌ
    private float VillainDelay=1.5f;//ºô·± µô·¹ÀÌ


    void Start()
    {
        
    }

    void Update()
    {
        Playercheck();
        
    }
    
    //TODO: ºô·± ¸®½ºÆù ¸¸µé¾î¾ßÇÔ
    private void Playercheck()
    {
        Collider2D[] Players = Physics2D.OverlapBoxAll(transform.position, Vector2.one * 2.8f, 0);
        if (Players == null) return;
        foreach (Collider2D player in Players)
        {
            if (player.CompareTag("Player"))
            {
                Delay += 1f * Time.deltaTime;
                print(Delay);
                if (Delay > VillainDelay)
                {
                    Destroy(gameObject);
                }
            }
            
        }

    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector2.one * 2.8f);
    }


}
