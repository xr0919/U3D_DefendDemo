using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;
    //向量
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //相机和玩家位置的差值，只要这个值不变就可以实现相机跟随
        dir = transform.position -player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + dir;
    }
}
