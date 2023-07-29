using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCmove : MonoBehaviour
{
    public float StopDistance=1.5f;
    Transform shelter;
    Animator anim;
    UnityEngine.AI.NavMeshAgent smith;
    
    void Start()
    {
        shelter = GameObject.Find("Shelter").transform;
        anim = transform.GetComponentInChildren<Animator>();
        smith = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
            // Vector3 dir = (player.position - transform.position).normalized;

            // cc.Move(dir *moveSpeed * Time.deltaTime);
            smith.isStopped = true;
            smith.ResetPath();

            // transform.forward = dir;
            smith.stoppingDistance = StopDistance;
            // 내비게이션의 목적지를 플레이어의 위치로 설정한다.
            smith.destination = shelter.position;
        }
        
    }
}
