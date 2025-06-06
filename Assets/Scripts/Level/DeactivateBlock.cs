using UnityEngine;

public class DeactivateBlock : MonoBehaviour
{
    [SerializeField] private GameObject block;
    private void OnTriggerEnter(Collider other)
    {
        LevelGenerator.SharedInstance.BlockStep();
    }
}
