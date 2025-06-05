using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FPSCounter : MonoBehaviour
{
    private TMP_Text textElementType;

    private const int deltasSize = 50;
    private int deltasPointer = 0;
    private float[] deltas = new float[deltasSize];

    public int FPS { get; private set; }
    

    void Start()
    {
        textElementType = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateFPS();
        textElementType.text = FPS.ToString();
    }

    private void CalculateFPS()
    {
        deltasPointer = deltasPointer % deltasSize;
        deltas[deltasPointer++] = Time.deltaTime;
        float sum = deltas.Sum();
        FPS = Convert.ToInt32(1f / (sum / deltasSize));
    }
}
