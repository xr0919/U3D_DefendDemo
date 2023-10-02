using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int Hp = 100;
    public float Speed = 3;
    public int Atk = 40;
    private Animator ani;
    //����������
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
        //�ƶ�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        if(dir != Vector3.zero)
        {
            ani.SetBool("IsRun", true);
            //ת��
            transform.rotation = Quaternion.LookRotation(dir);//��������
            //��ǰ��
            //transform.Translate(dir*Speed*Time.deltaTime);
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);//3m/֡ ����ʱ��֮���� 3m/��
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
    //��ֹ��ײ��һֱ����
    public void AttackEnd()
    {
        AttackTrigger.enabled = false;
    }
    //�ܵ�����
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

    //���˽�����ҹ�������
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyControl>().GetHit(Atk);
        }
    }
}
