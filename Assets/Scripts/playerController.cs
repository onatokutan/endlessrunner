using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public float speed; // Oyuncunun ileri hareket h�z�
    public float sideSpeed; // Yanlara hareket h�z�
    public float jumpForce = 10f; // Z�plama kuvveti
    public Transform centerPos; // Ortadaki pozisyon
    public Transform leftPos; // Sol pozisyon
    public Transform rightPos; // Sa� pozisyon
    public Transform shootingPoint; // Mermilerin ��k�� noktas�
    public GameObject projectilePrefab; // Mermi prefab'�
    public float fireRate = 0.5f; // Ate� etme aral���

    bool alive = true; // Oyuncunun canl� olup olmad���n� kontrol eder
    private int currentPos; // Oyuncunun �u anki pozisyonunu tutar (0 = merkez, 1 = sol, 2 = sa�)
    private float nextFireTime = 0f; // Bir sonraki ate� zaman�

    private Rigidbody rb;
    private bool isGrounded; // Yerde olup olmad���n� tutar

    private float timer;
    private float speedIncreaseInterval = 5f; // H�z� art�rma aral���

    [SerializeField] LayerMask groundMask;

    void Start()
    {
        currentPos = 0; // Oyuncu ba�lang��ta merkezde
        rb = GetComponent<Rigidbody>();
        GameManager.inst.decHealth(-3);
    }

    void Update()
    {
        if (!alive) return; // Oyuncu �l� ise hi�bir �ey yapma

        // Yerde olup olmad���n� kontrol et
        CheckGrounded();
        timer += Time.deltaTime;

        // H�z� zamanla art�rma
        if (timer >= speedIncreaseInterval)
        {
            speed += 5;
            Debug.Log("Speed:" + speed);
            timer = 0; // Timer'� s�f�rlay�n
        }

        // Oyuncunun ileri hareketi
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
        GameManager.inst.incScore(1);

        // Oyuncunun yanlara hareketi
        if (currentPos == 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(centerPos.position.x, transform.position.y, transform.position.z), sideSpeed * Time.deltaTime);
        }
        else if (currentPos == 1)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(leftPos.position.x, transform.position.y, transform.position.z), sideSpeed * Time.deltaTime);
        }
        else if (currentPos == 2)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(rightPos.position.x, transform.position.y, transform.position.z), sideSpeed * Time.deltaTime);
        }

        // Oyuncunun sola hareketi
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentPos == 0)
            {
                currentPos = 1;
            }
            else if (currentPos == 2)
            {
                currentPos = 0;
            }
        }

        // Oyuncunun sa�a hareketi
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentPos == 0)
            {
                currentPos = 2;
            }
            else if (currentPos == 1)
            {
                currentPos = 0;
            }
        }

        // Z�plama i�lemi (sadece yerdeyken ve UpArrow tu�una bas�ld���nda)
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        // Z�plama i�lemi (havada ve DownArrow tu�una bas�ld���nda)
        if (!isGrounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            Down();
        }

        // Oyuncunun ate� etmesi
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void Down()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.down * jumpForce * 5, ForceMode.Impulse);
        }
    }

    // Yerde olup olmad���n� kontrol eden fonksiyon
    void CheckGrounded()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        if (Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask))
        {
            isGrounded = true;
            // Debug.Log("Yerde");
        }
        else
        {
            isGrounded = false;
            // Debug.Log("Havada");
        }
    }

    void Shoot()
    {
        if (GameManager.inst.score >= 1000)
        {
            Vector3 shootPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f); // 1f ileri do�ru bir ofset ekledik
            GameObject projectileInstance = Instantiate(projectilePrefab, shootPosition, Quaternion.identity);

            // Projectile script'ine eri�erek h�z ayar�n� yap�n
            Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.IncreaseSpeed(speed); // Karakterin h�z�n� projectile'a aktar
            }

            //GameManager.inst.score -= 1000;
        }
    }

    public void die()
    {
        alive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Oyuncu �l�nce sahneyi yeniden ba�lat�r
    }
}