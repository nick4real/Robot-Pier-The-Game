using UnityEngine;

public class DeactivateBlock : MonoBehaviour
{
    [SerializeField] private GameObject block;
    private void OnTriggerEnter(Collider other)
    {
        block.SetActive(false);
    }
}
