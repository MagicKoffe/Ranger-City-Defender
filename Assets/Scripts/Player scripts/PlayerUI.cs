using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{
    private Camera mainCam;
    private bool gameIsPaused; 

    [SerializeField] private GameObject mouseReticle;

    private void OnEnable()
    {
        PauseMenuManager.togglePauseEvent += togglePause;
    }

    private void OnDisable()
    {
        PauseMenuManager.togglePauseEvent -= togglePause;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        positionReticle(GetMouseWorldPostion());
    }

    private void positionReticle(Vector3 mousePos)
    {
        mouseReticle.transform.position = mousePos;
    }

    private Vector3 GetMouseWorldPostion()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        positionReticle(mousePos);
        Ray ray = mainCam.ScreenPointToRay(mousePos);
        Vector3 mouseWorldPos = Vector3.zero;

        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit))
        {
            mouseWorldPos = hit.point;
        }

        return mouseWorldPos;
    }

    private void togglePause(bool isPaused)
    {
        if (gameIsPaused)
            mouseReticle.SetActive(false);
        else
            mouseReticle.SetActive(true);
    }
}
