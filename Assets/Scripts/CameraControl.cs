using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;
    //����
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //��������λ�õĲ�ֵ��ֻҪ���ֵ����Ϳ���ʵ���������
        dir = transform.position -player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + dir;
    }
}
