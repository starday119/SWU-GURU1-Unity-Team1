using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcattack : MonoBehaviour
{
    private npc movement;
    private float projectileDistance =30;
    
    Transform Zombie;
    public void Setup(Vector3 position)
    {
        movement = GetComponent<npc>();

        StartCoroutine("OnMove", position);

    }
    private IEnumerator OnMove(Vector3 ZombiePosition)
    {
        Zombie = GameObject.Find("enemy").transform;
        Vector3 start= transform.position;
        movement.MoveTo((ZombiePosition-transform.position).normalized);

        while(true)
        {
            if(Vector3.Distance(transform.position, start)>= projectileDistance)
            {
                Destroy(gameObject);
                yield break;
            }
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Zombie Hit");
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
