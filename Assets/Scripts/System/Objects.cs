using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Objects : MonoBehaviour
{
    protected Dictionary<Enum, int> ObjectList = new Dictionary<Enum, int>();//������Ʈ ���� ��ųʸ�

    protected float Delay = 1f;//������ ����
    protected float ObjectDelay;//������Ʈ ��Ÿ��

    protected int maxObject = 6;//������Ʈ �ִ뺸����

    protected float CoroutineSpeed;//������Ʈ  �ڷ�ƾ�ӵ�

    protected bool ObjectStop = true;//���� �������� üũ

    //����ϴ� ��ŸƮ �ڷ�ƾ
    protected void Start()
    {
        StartCoroutine(ObjectCoroutine());
    }
    //�ڽĿ��� �ڷ�ƾ ������
    protected virtual IEnumerator ObjectCoroutine()
    {
        yield return null;
    }
    //���� ���˽� ���� ���ۼ���
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("villain"))
        {
            print("���� ������");
            ObjectStop = false;
            print(ObjectStop);
        }
    }
    //������ ������� ���� ���ۼ���
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("villain"))
        {
            print("������ ����");
            ObjectStop = true;
        }
    }
}
