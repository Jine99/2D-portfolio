using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Objects
{

    private new void Start()
    {

    }

    private new void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - ObjectDelay < Delay) return;
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.player.InvenTrash();
            ObjectDelay = Time.time;
        }
    }
}
