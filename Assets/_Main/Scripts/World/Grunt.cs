using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour
{
    private int health;
    
    // Start is called before the first frame update
    void Start()
    {
        health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
