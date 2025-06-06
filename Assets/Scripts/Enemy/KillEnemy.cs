using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    public void Kill()
    {
        Destroy(enemyObject);
    }
}
