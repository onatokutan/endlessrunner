using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public float speed; // Oyuncunun ileri hareket hýzý
    public float sideSpeed; // Yanlara hareket hýzý
    public float jumpForce = 10f; // Zýplama kuvveti
    public Transform centerPos; // Ortadaki pozisyon
    public Transform leftPos; // Sol pozisyon
    public Transform rightPos; // Sað pozisyon
    public Transform shootingPoint; // Mermilerin çýkýþ noktasý
    public GameObject projectilePrefab; // Mermi prefab'ý
    public float fireRate = 0.5f; // Ateþ etme aralýðý

    bool alive = true; // Oyuncunun canlý olup olmadýðýný kontrol eder
    private int currentPos; // Oyuncunun þu anki pozisyonunu tutar (0 = merkez, 1 = sol, 2 = sað)
    private float nextFireTime = 0f; // Bir sonraki ateþ zamaný

    private Rigidbody rb;
    private bool isGrounded; // Yerde olup olmadýðýný tutar

    private float timer;
    private float speedIncreaseInterval = 5f; // Hýzý artýrma aralýðý

    [SerializeField] LayerMask groundMask;

    void Start()
    {
        currentPos = 0; // Oyuncu baþlangýçta merkezde
        rb = GetComponent<Rigidbody>();
        GameManager.inst.decHealth(-3);
    }

    void Update()
    {
        if (!alive) return; // Oyuncu ölü ise hiçbir þey yapma

        // Yerde olup olmadýðýný kontrol et
        CheckGrounded();
        timer += Time.deltaTime;

        // Hýzý zamanla artýrma
        if (timer >= speedIncreaseInterval)
        {
            speed += 5;
            Debug.Log("Speed:" + speed);
            timer = 0; // Timer'ý sýfýrlayýn
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

        // Oyuncunun saða hareketi
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

        // Zýplama iþlemi (sadece yerdeyken ve UpArrow tuþuna basýldýðýnda)
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        // Zýplama iþlemi (havada ve DownArrow tuþuna basýldýðýnda)
        if (!isGrounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            Down();
        }

        // Oyuncunun ateþ etmesi
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

    // Yerde olup olmadýðýný kontrol eden fonksiyon
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
            Vector3 shootPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2f); // 1f ileri doðru bir ofset ekledik
            GameObject projectileInstance = Instantiate(projectilePrefab, shootPosition, Quaternion.identity);

            // Projectile script'ine eriþerek hýz ayarýný yapýn
            Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.IncreaseSpeed(speed); // Karakterin hýzýný projectile'a aktar
            }

            //GameManager.inst.score -= 1000;
        }
    }

    public void die()
    {
        alive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Oyuncu ölünce sahneyi yeniden baþlatýr
    }
}