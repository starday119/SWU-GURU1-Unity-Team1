using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class NPCtalk : MonoBehaviour
{
    public GameObject dialogpanel;
    public Text NPCtext;
    public string[] dialog;
    
    public int index;
    public static float wordSpeed=0.1f;
    public bool playereIsClose;
    public GameObject contButton;
    public GameObject NPCTalkCamera;
    public GameObject maincamera;
    // public GameObject playerMove;
   

    public static Action target;

    public string charactername ="";
   
    public Text objectname;
    private void Awake()
    {
        target =()=>
        {
            Typing();
        };
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        dialogpanel.SetActive(false);
        maincamera.SetActive(true);
        NPCTalkCamera.SetActive(false);
        // Button contbutton=GetComponent<Button>();
        // contbutton.onClick.AddListener(NextLine);
       
        
        
    }// Update is called once per frame
    void Update()
    {
        
        
    }
        
    
    public void zeroText()
    {
        
        NPCtext.text="";
        index =0; 
        dialogpanel.SetActive(false);
        NPCTalkCamera.SetActive(false);
        maincamera.SetActive(true);
        StopAllCoroutines();
    }
    IEnumerator Typing()
    {
        
        foreach (char letter in dialog[index].ToCharArray())
        {
            NPCtext.text += letter;
            contButton.SetActive(false);
            yield return new WaitForSeconds(wordSpeed);
            
            
        } 
        contButton.SetActive(true);
        if(index >=dialog.Length-1)
        {
            contButton.SetActive(false);
        }
    } 
    public void NextLine()
    {
      
       
        StopCoroutine(Typing());
        if(index <dialog.Length-1)
        {
            
            index++;
            NPCtext.text="";
            StartCoroutine(Typing());
            NPCTalkCamera.SetActive(false);
            maincamera.SetActive(true);
            
        }
        else
        {
            
            zeroText();
            contButton.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            NPCTalkCamera.SetActive(true);
            maincamera.SetActive(false);
            dialogpanel.SetActive(true);
            playereIsClose=true;
            StartCoroutine(Typing());
            objectname.text=charactername;
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playereIsClose=false;
            zeroText();
            NPCTalkCamera.SetActive(false);
            maincamera.SetActive(true);
            StopAllCoroutines();
        }
    }
    
}
