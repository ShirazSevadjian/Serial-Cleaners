using UnityEngine;

public class MopCleaner : MonoBehaviour
{
    [SerializeField] private Texture2D brush;



    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Blood"))
        //{
        //    Destroy(other.gameObject);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.up * 5.0f);
    }
}
