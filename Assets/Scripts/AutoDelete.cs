using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    [SerializeField] private float secondsToVanish;
    void Start()
    {
        if (secondsToVanish == 0)
            return;

        Invoke(nameof(Vanish), secondsToVanish);
    }

    public void Vanish()
    {
        Destroy(gameObject);
    }
}
