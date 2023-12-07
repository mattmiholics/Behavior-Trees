using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement: MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 500f;
    public float shootCooldown = 0.5f; // Cooldown between shots
    private float shootCooldownTimer = 0f;

    public GameObject bulletPrefab;
    public Transform shootPoint;

    public int health = 10;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Move horizontally and vertically
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Rotate the character to face the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Shoot
        if (Input.GetButtonDown("Fire1") && shootCooldownTimer <= 0f)
        {
            Shoot();
            shootCooldownTimer = shootCooldown;
        }

        // Update shoot cooldown timer
        shootCooldownTimer = Mathf.Max(0f, shootCooldownTimer - Time.deltaTime);
    }

    void Shoot()
    {
        // Instantiate a bullet at the shoot point
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Get the BulletController script from the instantiated bullet and set its speed
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.bulletSpeed = 50f; // Adjust the speed as needed
        }
    }
    public void TakeDamagePlayer()
    {
        health--;
    }
}