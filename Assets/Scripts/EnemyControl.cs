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
    //����Ƶ�ʼ�ʱ��
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        ani=GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();//����Ϸ���������ʽ Ч�ʵ�
        playerT = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Hp < 0)
        {
            return;
        }
        //�����ûѪ
        if (player.Hp<0)
        {
            ani.SetBool("IsRun", false);
            //ֹͣ����
            agent.isStopped = true;
            return;
        }
        //��ȡ������Լ��ľ���
        float dis = Vector3.Distance(transform.position,player.transform.position);
        //float dis = Vector3.Distance(transform.position,playerT.position);
        Debug.Log(dis);
        //��ʱ������
        timer += Time.deltaTime;
        if(dis < 1.5f)
        {
            if(timer > 1)//����
            {
                //����1��һ��
                timer = 0;
                ani.SetBool("IsRun", false);
                ani.SetTrigger("Atk01");

            }
            //ֹͣ�ƶ�
            agent.isStopped = true;
        }
        else
        {
            //�ƶ�
            ani.SetBool("IsRun", true);
            agent.isStopped = false;
            //������ƶ�
            agent.SetDestination(player.transform.position);
        }
    }

    public void Attack()
    {
        player.GetHit(20);

    }

    //�ܵ�����
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
