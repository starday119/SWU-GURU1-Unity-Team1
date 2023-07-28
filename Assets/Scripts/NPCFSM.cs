using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class NPCFSM : MonoBehaviour
{
    public int weaponPower =5;
    enum NPCState
    {
        Idle,
        Move,
        Attack,
        
        
    }
    NPCState m_State;

    public float findDistance = 8f;

    Transform Zombie;

    public float attackDistance = 2f;

    public float moveSpeed; 

    CharacterController cc;

    float currentTime =0;
    float attackDelay = 2f;

    public int attackPower = 3;
    
    Vector3 originPos;
    Quaternion originRot;

    
    public int hp = 15;
    public int maxHp=15;

    public Slider hpSlider;
    Animator anim;
    NavMeshAgent smith;

    private float lastAttackTime =0;
  
    [Header("Attack")]
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileSpawnPoint;
    // [SerializeField]
    // private float attackRange=5;
    [SerializeField]
    private float attackRate =1;
    public int attacknum;
    // public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        m_State = NPCState.Idle;
        Zombie = GameObject.Find("enemy").transform;
        // gun.SetActive(false);
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
            case NPCState.Idle:
                Idle();
                break;
            case NPCState.Move:
                Move();
                break;
            case NPCState.Attack:
                Attack();
                break;
            // case NPCState.Return:
            //     Return();
            //     break;   
            // case NPCState.Damaged:
            //     Damaged();
            //     break;
            // case NPCState.Die:
            //     Die();
            //     break;
            
            
            
            
        }
        // hpSlider.value = (float)hp / (float)maxHp;
    
    }
    void Idle()
    {
        if(Vector3.Distance(transform.position, Zombie.position)<findDistance)
        {
            m_State = NPCState.Move;
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
            m_State =NPCState.Attack;
            print("상태 전환: Move -> Attack");
            
            currentTime = attackDelay;
            anim.SetTrigger("MoveToAttackDelay");

        }
        
        
    }
    public void Attack()
    {
        attacknum=0;
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
                    GameObject clone = Instantiate(projectilePrefab,projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                    clone.GetComponent<npcattack>().Setup(Zombie.position);  
                    anim.SetTrigger("StartAttack");
                }
                attacknum ++;
            
            }
                    
                // LookRotationToZombie();
           
                
                
            
            
                // player.GetComponent<PlayerMove>().DamageAction(attackPower);
           

            // 
            // }
               
               
                
            
        }
        else
        {
            m_State = NPCState.Move;
            print("상태 전환: Attack -> Move");
            currentTime =0;
            anim.SetTrigger("AttackToMove");
        }
        

    }
    // private void CalculateDistanceToTargetAndSelectState()
    // {
    //     if(Zombie==null) return;

    //     float distance = Vector3.Distance(Zombieposition, transform.position);
    //     if(distance<= attackRangee)
    //     {
    //         ChangeState(NPCState.Attack);
    //     }
        
        
    // }
    // public void ChangeState(NPCState newState)
    // {
    //     if(m_State == newState) return;
    //     StopCoroutine(m_State.ToString());
    //     m_State = newState;
    //     StartCoroutine(m_State.ToString());
    // }
    // private void LookRotationToTarget()
    // {
    //     if (other.tag == "Zombie")
    //     {
    //         transform.LookAt(other.transform);
    //     }
        
    // }
    // public void AttackAction()
    // {
    //     player.GetComponent<PlayerMove>().DamageAction(attackPower);
    // }
    // void Return()
    // {
    //     if(Vector3.Distance(transform.position, originPos)>0.1f)
    //     {
    //         // Vector3 dir = (originPos - transform.position).normalized;
    //         // cc.Move(dir * moveSpeed * Time.deltaTime);
    //         // transform.forward = dir; 
    //         smith.destination = originPos;
    //         smith.stoppingDistance = 0;


    //     }
    //     else
    //     {
    //         smith.isStopped = true;
    //         smith.ResetPath();
            
    //         transform.position = originPos;
    //         transform.rotation = originRot;
    //         hp= maxHp;
    //         m_State =NPCState.Idle;
    //         print("상태 전환: Return -> Idle");
    //         anim.SetTrigger("MoveToIdle");
    //     }

    // }
    // void Damaged()
    // {
    //     StartCoroutine(DamageProcess());
    // }
    // IEnumerator DamageProcess()
    // {
    //     yield return new WaitForSeconds(1.0f);

    //     m_State = NPCState.Move;
    //     print("상태 전환 : Damaged-> Move");
    // }
    
    // public void HitEnemy(int hitPower)
    // {   
    //     hp -= hitPower;
        
    //     if(m_State==NPCState.Damaged || m_State == NPCState.Die || m_State == NPCState.Return)
    //     {
    //         return;
    //     }
        
    //     smith.isStopped = true;
    //     smith.ResetPath();


    //     if (hp > 0)
    //     {
    //         m_State = NPCState.Damaged;
    //         print("상태 전환: Any state -> Damaged");
    //         print(hp);

    //         anim.SetTrigger("Damaged");

    //         Damaged();
    //     }
    //     else
    //     {
    //         m_State = NPCState.Die;
    //         print("상태 전환: Any state -> Die");
    //         anim.SetTrigger("Die");
    //         Die();
    //     }
            
    // }
    // void Die()
    // {
    //     StopAllCoroutines();

    //     StartCoroutine(DieProcess());
    // }
    // IEnumerator DieProcess()
    // {
    //     cc.enabled = false;

    //     yield return new WaitForSeconds(2f);
    //     print("소멸!");
    //     Destroy(gameObject);

    // }
    // public void HitZombie(int hitPower)
    // {
    //     hp-= hitPower;
        
    // }
    // if(Physics.Raycast(ray,out hitInfo))
    // {
    //     if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Zombie"))
    //     {
    //         Zombie eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
    //         eFSM.HitEnemy(weaponPower);
    //     }
    // }
    
}
