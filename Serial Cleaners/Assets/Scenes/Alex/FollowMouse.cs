using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private LayerMask layermask;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, layermask))
        {
            transform.position = hitInfo.point;
        }

        transform.position = new Vector3(transform.position.x, 0.20f, transform.position.z);
    }
}
