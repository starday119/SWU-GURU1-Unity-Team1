using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator  Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled =true;
        trailEffect.enabled=true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        
    }
}
