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
        Stop,
       
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
   
   

   
    Animator anim;
    NavMeshAgent smith;
    
    
    public int npchp;

    Enemy enemy;
  
    
    public GameObject zombie;

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
            // case ClosedNPCState.Stop:
            //     Stop();
            //     break;
            
            
            
            
        }
        // hpSlider.value = (float)hp / (float)maxHp;
    
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
    void Attack()
    {
        if (Vector3.Distance(transform.position,Zombie.position)<attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                
                

               
                Zombie.GetComponent<hitzombie1>().DamageAction(attackPower);
                print("공격");
                // player.GetComponent<PlayerMove>().DamageAction(attackPower);
                currentTime=0;

                anim.SetTrigger("doSwing");
                



            }
            if(enemy.hp <0)
            {
                n_State = ClosedNPCState.Idle;
                print("상태전환 stop");
                anim.SetTrigger("AttackToStop");
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
    // public void AttackAction()
    // {
    //     player.GetComponent<PlayerMove>().DamageAction(attackPower);
    // }
//    void Stop()
//    {
//         if(enemy.hp <=0)
//         {
//            n_State = ClosedNPCState.Idle;
//            print("상태전환 stop");
//            anim.SetTrigger("AttackToStop");
//         }
//    }
            
    
   
    
    
}
