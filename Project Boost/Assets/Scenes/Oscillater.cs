using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillater : MonoBehaviour
{
    [SerializeField] Vector3 movmentVector = new Vector3(10f, 10f, 10f);
    [Range(0, 1)] [SerializeField] float movmentFactor;

    Vector3 pos;

    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period == 0) { return; }    
        Vector3 offset = movmentVector * movmentFactor;
        transform.position = pos + offset;
        float cycle = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(tau * cycle);
        movmentFactor = rawSinWave / 2f + 0.5f;
    }
}
