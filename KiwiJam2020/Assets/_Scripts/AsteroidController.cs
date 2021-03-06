﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private int destroyTimer;
    private int xSpin;
    private int ySpin;
    private int zSpin;
    [SerializeField] private float spinSpeed;
    private Quaternion targetRotation;
    private Rigidbody rb;
    public GameObject mesh;

    public ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        explosion.Stop();
        xSpin = Random.Range(0, 360);
        ySpin = Random.Range(0, 360);
        zSpin = Random.Range(0, 360);
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(xSpin, ySpin, zSpin) / (spinSpeed * 50) * Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {



        Invoke("DestroyOBJ", destroyTimer);
    }

    void DestroyOBJ()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        if (other.tag == "Destroy")
        {
            
            Destroy(mesh);
            explosion.Play();
            Invoke("DestroyRest", 2);
        }
    }
    void DestroyRest()
    {
        Destroy(this.gameObject);
    }
}
