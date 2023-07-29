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
        Die,
        
        
    }
    NPCState m_State;

    public float findDistance = 8f;

    Transform Zombie;

    public float attackDistance = 2f;

    public float moveSpeed; 

    CharacterController cc;

    float currentTime =0;
    float attackDelay = 2f;

    public int attackPower = 1;
    
    Vector3 originPos;
    Quaternion originRot;

    
    public int hp = 15;
    public int maxHp=15;

    public Slider hpSlider;
    Animator anim;
    NavMeshAgent smith;

    public float StopDistance=1.5f;
    Transform shelter;
    

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
        shelter = GameObject.Find("Shelter").transform;

        attacknum=0;
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
            case NPCState.Die:
                Die();
                break;
            
            
            
            
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
                    attacknum+=1;
                    if(attacknum>8)
                    {
                        m_State = NPCState.Die;
                        Die();
                        anim.SetTrigger("Die");
                    }
                    
                }
                
                print("attacknum"+attacknum);
            }
                    
                
               
               
                
            
        }
        else
        {
            m_State = NPCState.Move;
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
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            MoveToShelter();
        }
    }
    void MoveToShelter()
    {
        if (Vector3.Distance(transform.position, shelter.position) > StopDistance)
        {
           
            smith.isStopped = true;
            smith.ResetPath();

            // transform.forward = dir;
            smith.stoppingDistance = StopDistance;
           
            smith.destination = shelter.position;

            m_State = NPCState.Move;
            print("쉘터 찾아가기");
        }
        
    }
}
