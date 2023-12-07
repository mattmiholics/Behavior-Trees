using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 1000f;

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        // Destroy the bullet after a certain time or distance
        Destroy(gameObject, 3f);
    }
    
    
}