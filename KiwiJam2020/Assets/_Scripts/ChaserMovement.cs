﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
