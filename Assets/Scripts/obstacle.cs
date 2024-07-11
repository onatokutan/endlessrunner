
using UnityEngine;

public class obstacle : MonoBehaviour
{
    playerController playerMovement;

    int i;
    void Start()
    {
        playerMovement = FindObjectOfType<playerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.inst.health == 0)
            {
                playerMovement.die();
            }
            else 
            {
                //Destroy(gameObject); // Engeli yok et
                GameManager.inst.decHealth(1);
            }
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject); // Mermiyi yok et
            Destroy(gameObject); // Engeli yok et
            GameManager.inst.incScore(500);

        }
    }
}