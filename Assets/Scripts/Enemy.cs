using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "player")
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.TakeDamagePlayer();
        }
    }
    public void TakeDamage() 
    {
        health--;
    }
}
    
