using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private Animator door;
    [SerializeField] private bool isOpenTrigger;
    [SerializeField] private bool isCloseTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isOpenTrigger)
            {
                door.Play("DoorOpen");
                //gameObject.SetActive(false);
            }

            else if (isCloseTrigger)
            {
                door.Play("DoorClose");
                //gameObject.SetActive(false);
            }
        }
    }
}
