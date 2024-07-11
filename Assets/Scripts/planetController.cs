using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public Transform player; // Oyuncunun transform referans�
    public float distanceInFrontOfPlayer = 50f; // Oyuncunun �n�nde sabit kalmas� gereken mesafe
    public float verticalOffset = -10f; // Gezegenin a�a��da durmas� gereken y�kseklik (pozitif/negatif de�er ile ayarlay�n)

    private Vector3 initialPosition;

    void Start()
    {
        // Gezegenin ba�lang�� konumunu kaydet
        initialPosition = transform.position;
    }

    void Update()
    {
        if (player != null)
        {
            // Gezegenin sadece z ekseninde oyuncunun �n�nde kalmas�n� sa�la
            Vector3 newPosition = new Vector3(initialPosition.x, verticalOffset, player.position.z + distanceInFrontOfPlayer);
            transform.position = newPosition;

            // Gezegenin kendi ekseni etraf�nda d�nmesini sa�lar
            transform.Rotate(Vector3.up, 10f * Time.deltaTime);
        }
    }
}
