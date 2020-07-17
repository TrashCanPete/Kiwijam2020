using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;
    [SerializeField] private float chaserSpeed;

    [SerializeField] private Vector3 baseVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        chaserSpeed = baseVelocity.magnitude;
        baseVelocity = (rb.transform.forward * speed);
    }
    private void FixedUpdate()
    {
        rb.velocity = baseVelocity;
    }


}
