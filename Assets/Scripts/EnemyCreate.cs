using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    //预设体
    public GameObject EnemyPre;
    public float timer;
    public float CD = 2f;
    private PlayerControl player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.Hp <=0)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > CD)
        {
            CD = Random.Range(1, 4);//每次刷新敌人后随机生成时间
            timer = 0;
            Instantiate(EnemyPre, transform.position, Quaternion.identity);
        }
    }
}
