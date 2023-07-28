// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public class NpcFSM1 : MonoBehaviour
// {
//     public enum NPCState { Nome =-1, Idle =0, Wander,Pursuit,Attack, }
//     // Start is called before the first frame update
//     [Header("Pusrsuit")]
//     [SerializeField]
//     private float targetRecognitionRange = 8;
//     [SerializeField]
//     private float pusrsuitLimitRange=10;
//     [Header("Attack")]
//     [SerializeField]
//     private GameObject projectilePrefab;
//     [SerializeField]
//     private float attackRange=5;
//     [SerializeField]
//     private float attackRate =1;
//     private NPCState EnemyState = EnemyState.None;
//     private float lastAttackTime =0;

//     private Status status;
//     private UnityEngine.AI.NavMeshAgent smith;
//     private Transform target;

//     public void Setup(Transform target);

//     private void OnEnable();
//     private void OnDisble();
//     public void ChangeState(EnemyState newState);
//     private IEnumerator Idle();
//     private IEnumerator AutoChanteFromIdleToWander();
//     private IEnumerator wander();
//     private Vector3 CalculateWanderPosition();
//     private Vector3 SetAngle(float radius, int angle);

//     private IEnumerator Attack()
//     {
//         smith.ResetPath();
//         while (true)
//         {
//             LookRotationToTarget();
//             CalculateDistanceToTargetAndSelectState();

//             if (Time.time - lastAttackTime>attackRate)
//             {
//                 lastAttackTime = Time.time;
//                 GameObject clone = Instantiate(projectilePrefab,projectileSpawnPoint.position, projectileSpawnPoint.rotation);
//                 clone.GetComponent<npcattack>().Setup(target.position);
//             }
//             yield return null;
//         }
//     }
//     private void LookRotationToTarget();

//     private void CalculateDistanceToTargetAndSelectState()
//     {
//         if(target==null) return;

//         float distance = Vector3.Distance(target.position, transform.position);
//         if(distance<= attackRangee)
//         {
//             ChangeState(NPCState.Attack);
//         }
//         else if(distance<= targetRecognitionRange)
//         {
//             ChangeState(NPCState.Pursuit);
//         }
//         elseif(distanc>= pusrsuitLimitRange);
//         {
//             ChangeState(NPCState.Wander);
//         }
//     }
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
