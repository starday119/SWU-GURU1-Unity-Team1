using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float wordSpeed;
    public float moveSpeed = 7f;
    CharacterController cc;
    float gravity =-20f;
    Animator anim;
    public float yVelocity =0;
   
    public GameObject NPCTalkCamera;

    // Start is called before the first frame update
    void Start()
    {
        cc= GetComponent<CharacterController>();
        anim=GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h,0,v);
        dir = dir.normalized;

        if(Camera.main !=null)
        {
            dir = Camera.main.transform.TransformDirection(dir);
            transform.position += dir * moveSpeed* Time.deltaTime;
            yVelocity += gravity * Time.deltaTime;
            dir.y = yVelocity;

            cc.Move(dir*moveSpeed*Time.deltaTime);
        
        }
        
        


    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     NPCtalk.target()=false;
    //     if (other.tag == "Friend"&& NPCtalk.target()=true)
    //     {
    //        StartCoroutine(StopMoving()); 
    //        StopAllCoroutines();
          
    //     }
    //     StopAllCoroutines();
    // }
    // }
    // IEnumerator StopMoving()
    // {
        
        
    //     yield return new WaitForSeconds(wordSpeed);
    //     enabled = false ;
          
    // } 
    // private void OnTriggerEnter(Collider other)
    
    // {
    //     float h = Input.GetAxis("Horizontal");
    //     float v = Input.GetAxis("Vertical");

    //     Vector3 dir = new Vector3(h,0,v);
    //     dir = dir.normalized;
    //     if (other.tag == "Friend")
    //     {
    //        dir = NPCTalkCamera.transform.TransformDirection(dir);
           
          
    //     }
    // }
}
