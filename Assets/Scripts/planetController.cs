using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public Transform player; // Oyuncunun transform referansý
    public float distanceInFrontOfPlayer = 50f; // Oyuncunun önünde sabit kalmasý gereken mesafe
    public float verticalOffset = -10f; // Gezegenin aþaðýda durmasý gereken yükseklik (pozitif/negatif deðer ile ayarlayýn)

    private Vector3 initialPosition;

    void Start()
    {
        // Gezegenin baþlangýç konumunu kaydet
        initialPosition = transform.position;
    }

    void Update()
    {
        if (player != null)
        {
            // Gezegenin sadece z ekseninde oyuncunun önünde kalmasýný saðla
            Vector3 newPosition = new Vector3(initialPosition.x, verticalOffset, player.position.z + distanceInFrontOfPlayer);
            transform.position = newPosition;

            // Gezegenin kendi ekseni etrafýnda dönmesini saðlar
            transform.Rotate(Vector3.up, 10f * Time.deltaTime);
        }
    }
}
