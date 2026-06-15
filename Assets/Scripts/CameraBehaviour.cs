using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset = new Vector3(5, 11, -5);

    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.position);

        transform.position = player.position + cameraOffset; 
    }
}
