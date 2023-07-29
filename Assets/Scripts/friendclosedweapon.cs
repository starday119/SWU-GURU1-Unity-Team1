using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class friendclosedweapon : MonoBehaviour
{
    enum ClosedNPCState
    {
        Idle,
        Move,
        Attack,
        Die,
       
    }
    ClosedNPCState n_State;

    public float findDistance = 8f;

    Transform Zombie;

    public float attackDistance = 2f;

    public float moveSpeed = 5f;

    CharacterController cc;

    float currentTime =0;
    float attackDelay = 2f;

    public int attackPower = 3;
    
    Vector3 originPos;
    Quaternion originRot;

    public float moveDistance = 20f;
   
    private float lastAttackTime =0;

    private float attackRate =1;
    Animator anim;
    NavMeshAgent smith;
    
    
    public int npchp;

    Enemy enemy;
  
    
    public GameObject zombie;

    public int attacknum;

    // Start is called before the first frame update
    void Start()
    {
        n_State = ClosedNPCState.Idle;
        Zombie = GameObject.Find("enemy").transform;
       
        cc= GetComponent<CharacterController>();

        originPos = transform.position;
        originRot = transform.rotation;
        anim = transform.GetComponentInChildren<Animator>();
        smith = GetComponent<NavMeshAgent>();

        enemy = GameObject.Find("enemy").GetComponent<Enemy>();
        attacknum=0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (n_State)
        {
            case ClosedNPCState.Idle:
                Idle();
                break;
            case ClosedNPCState.Move:
                Move();
                break;
            case ClosedNPCState.Attack:
                Attack();
                break;
            case ClosedNPCState.Die:
                Die();
                break;
            
            
            
            
        }
       
    }
    void Idle()
    {
        if(Vector3.Distance(transform.position, Zombie.position)<findDistance)
        {
            n_State = ClosedNPCState.Move;
            print("상태 전환: Idle -> Move");
            anim.SetTrigger("IdleToMove");
        }
    }
    void Move()
    {
        if (Vector3.Distance(transform.position, Zombie.position) > attackDistance)
        {
            // Vector3 dir = (player.position - transform.position).normalized;

            // cc.Move(dir *moveSpeed * Time.deltaTime);
            smith.isStopped = true;
            smith.ResetPath();

            // transform.forward = dir;
            smith.stoppingDistance = attackDistance;
            // 내비게이션의 목적지를 플레이어의 위치로 설정한다.
            smith.destination = Zombie.position;
        }
        else
        {
            n_State = ClosedNPCState.Attack;
            print("상태 전환: Move -> Attack");
            
            currentTime = attackDelay;
            anim.SetTrigger("MoveToAttackDelay");

        }
        
        
    }
    public void Attack()
    {
        
         // smith.ResetPath();
        if (Vector3.Distance(transform.position, Zombie.position)<attackDistance)
        {
            
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                print("공격");
                currentTime=0;
               
                // gun.SetActive(true);
                if (Time.time - lastAttackTime>attackRate)
                {
                    Zombie.GetComponent<hitzombie1>().DamageAction(attackPower);
                    lastAttackTime = Time.time;
                    
                    anim.SetTrigger("StartAttack");
                    attacknum+=1;
                    if(attacknum>10)
                    {
                        n_State = ClosedNPCState.Die;
                        Die();
                        anim.SetTrigger("Die");
                    }
                    
                }
                
                print("attacknum:"+attacknum);
            }
                    
                
               
               
                
            
        }
        else
        {
            n_State = ClosedNPCState.Move;
            print("상태 전환: Attack -> Move");
            currentTime =0;
            anim.SetTrigger("AttackToMove");
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
}

            
    

