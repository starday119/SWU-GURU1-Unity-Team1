using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    EnemyState m_State;

    public float findDistance = 8f;

    Transform player;

    public float attackDistance = 2f;

    public float moveSpeed = 5f;

    CharacterController cc;

    float currentTime =0;
    float attackDelay = 2f;

    public int attackPower = 3;
    
    Vector3 originPos;
    Quaternion originRot;

    public float moveDistance = 20f;
    public int hp = 20;
    int maxHp=15;

    public Slider hpSlider;
    Animator anim;
    NavMeshAgent smith;

    Transform zombie;
    


    // Start is called before the first frame update
    void Start()
    {
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;

        cc= GetComponent<CharacterController>();

        originPos = transform.position;
        originRot = transform.rotation;
        anim = transform.GetComponentInChildren<Animator>();
        smith = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;   
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
            
            
            
            
        }
        // hpSlider.value = (float)hp / (float)maxHp;
    
    }
    void Idle()
    {
        if(Vector3.Distance(transform.position, player.position)<findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move");
            anim.SetTrigger("IdleToMove");
        }
    }
    void Move()
    {
        if(Vector3.Distance(transform.position, originPos)>moveDistance)
        {
            m_State = EnemyState.Return;
            print("상태 전환: Move -> Return");
            
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // Vector3 dir = (player.position - transform.position).normalized;

            // cc.Move(dir *moveSpeed * Time.deltaTime);
            smith.isStopped = true;
            smith.ResetPath();

            // transform.forward = dir;
            smith.stoppingDistance = attackDistance;
            // 내비게이션의 목적지를 플레이어의 위치로 설정한다.
            smith.destination = player.position;
        }
        else
        {
            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attack");
            
            currentTime = attackDelay;
            anim.SetTrigger("MoveToAttackDelay");

        }
        
        
    }
    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position)<attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                
                

               
               
                print("공격");
                // player.GetComponent<PlayerMove>().DamageAction(attackPower);
                currentTime=0;

                anim.SetTrigger("StartAttack");
            }
        }
        else
        {
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move");
            currentTime =0;
            anim.SetTrigger("AttackToMove");
        }
    }
    // public void AttackAction()
    // {
    //     player.GetComponent<PlayerMove>().DamageAction(attackPower);
    // }
    void Return()
    {
        if(Vector3.Distance(transform.position, originPos)>0.1f)
        {
            // Vector3 dir = (originPos - transform.position).normalized;
            // cc.Move(dir * moveSpeed * Time.deltaTime);
            // transform.forward = dir; 
            smith.destination = originPos;
            smith.stoppingDistance = 0;


        }
        else
        {
            smith.isStopped = true;
            smith.ResetPath();
            
            transform.position = originPos;
            transform.rotation = originRot;
            hp= maxHp;
            m_State = EnemyState.Idle;
            print("상태 전환: Return -> Idle");
            anim.SetTrigger("MoveToIdle");
        }

    }
    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }
    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(1.0f);

        m_State = EnemyState.Move;
        print("상태 전환 : Damaged-> Move");
    }
    
    public void HitEnemy(int hitPower)
    {   
        hp -= hitPower;
        
        if(m_State==EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }
        
        smith.isStopped = true;
        smith.ResetPath();


        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("상태 전환: Any state -> Damaged");
            print(hp);

            anim.SetTrigger("Damaged");

            Damaged();
        }
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환: Any state -> Die");
            anim.SetTrigger("Die");
            Die();
        }
            
    }
    void Die()
    {
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2f);
        print("소멸!");
        Destroy(gameObject);

    }
    public void DamageAction(int damage)
    {
        hp-=damage;
    }
}
