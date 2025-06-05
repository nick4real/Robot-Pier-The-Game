using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TrampolineBehavior : MonoBehaviour
{
    [Range(0f, 10000f)]
    [SerializeField] private float trampolineStrength;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * trampolineStrength, ForceMode.Impulse);
        }
    }
}
