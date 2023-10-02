using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public int Hp = 100;
    private Animator ani;
    private NavMeshAgent agent;
    private Transform playerT;
    private PlayerControl player;
    //攻击频率计时器
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        ani=GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();//大游戏少用这个方式 效率低
        playerT = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Hp < 0)
        {
            return;
        }
        //当玩家没血
        if (player.Hp<0)
        {
            ani.SetBool("IsRun", false);
            //停止导航
            agent.isStopped = true;
            return;
        }
        //获取玩家与自己的距离
        float dis = Vector3.Distance(transform.position,player.transform.position);
        //float dis = Vector3.Distance(transform.position,playerT.position);
        Debug.Log(dis);
        //计时器增加
        timer += Time.deltaTime;
        if(dis < 1.5f)
        {
            if(timer > 1)//攻速
            {
                //攻击1秒一次
                timer = 0;
                ani.SetBool("IsRun", false);
                ani.SetTrigger("Atk01");

            }
            //停止移动
            agent.isStopped = true;
        }
        else
        {
            //移动
            ani.SetBool("IsRun", true);
            agent.isStopped = false;
            //朝玩家移动
            agent.SetDestination(player.transform.position);
        }
    }

    public void Attack()
    {
        player.GetHit(20);

    }

    //受到攻击
    public void GetHit(int Atk)
    {
        if (Hp <= 0)
        {
            return;
        }
        Hp -= Atk;
        if (Hp <= 0)
        {
            ani.SetTrigger("Die");

        }
    }
}
