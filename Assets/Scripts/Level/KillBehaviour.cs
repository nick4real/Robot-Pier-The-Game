using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KillBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("Player").transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
    }
}
