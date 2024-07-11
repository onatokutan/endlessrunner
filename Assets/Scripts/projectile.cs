using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 80f;
    public float lifetime = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;
        }
        Debug.Log("Projectile speed increased to: " + speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
    }
}