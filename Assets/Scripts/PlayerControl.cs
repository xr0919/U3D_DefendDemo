using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int Hp = 100;
    public float Speed = 3;
    public int Atk = 40;
    private Animator ani;
    //攻击触发器
    public Collider AttackTrigger;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            return;
        }
        //移动
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        if(dir != Vector3.zero)
        {
            ani.SetBool("IsRun", true);
            //转向
            transform.rotation = Quaternion.LookRotation(dir);//看向向量
            //向前走
            //transform.Translate(dir*Speed*Time.deltaTime);
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);//3m/帧 乘了时间之后变成 3m/秒
        }
        else
        {
            ani.SetBool("IsRun", false);

        }
        if(Input.GetMouseButtonDown(0) && dir == Vector3.zero)
        {
            ani.SetTrigger("Atk01");
        }
    }
    public void Attack()
    {
        Debug.Log("aaa");
        AttackTrigger.enabled = true;
        Invoke("AttackEnd", 01f);

    }
    //防止碰撞器一直开启
    public void AttackEnd()
    {
        AttackTrigger.enabled = false;
    }
    //受到攻击
    public void GetHit(int Atk)
    {
        if(Hp <= 0)
        {
            return;
        }
        Hp -= Atk;
        if(Hp <= 0)
        {
            ani.SetTrigger("Die");

        }
    }

    //敌人进入玩家攻击区域
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyControl>().GetHit(Atk);
        }
    }
}
