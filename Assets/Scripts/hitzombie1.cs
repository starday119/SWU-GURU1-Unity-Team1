using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitzombie1 : MonoBehaviour
{
    public int hp =15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageAction(int damage)
    {
        hp-=damage;
        print(hp);
    }
}
