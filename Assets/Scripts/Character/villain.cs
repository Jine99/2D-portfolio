using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villain : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Objects>(out Objects objects))
        {
            print("������ ���� ����");
            objects.ObjectSituation = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Objects>(out Objects objects))
        {
            objects.ObjectSituation = true;
        }
    }
}
