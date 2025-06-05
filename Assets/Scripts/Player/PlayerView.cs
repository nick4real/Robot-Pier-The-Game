using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerView : MonoBehaviour
{
    [SerializeField, Range(1f, 100f)] private float sensivity = 5f;
    [SerializeField] private Transform orientation;
    private float xRotation;
    private float yRotation;

    public void Look(Vector2 input)
    {
        var temp = input * Time.deltaTime * sensivity;
        yRotation += temp.x;
        xRotation -= temp.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}