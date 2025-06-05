using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyOrientation : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    void Update()
    {
        transform.rotation = orientation.transform.rotation;
    }
}
