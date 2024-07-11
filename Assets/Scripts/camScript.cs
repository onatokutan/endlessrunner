using UnityEngine;


public class camScript : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,player.transform.position.z -5f);
    }
}
