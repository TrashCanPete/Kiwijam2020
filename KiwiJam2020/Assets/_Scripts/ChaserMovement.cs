using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;
    [SerializeField] private float chaserSpeed;

    [SerializeField] private Vector3 baseVelocity;
    public GameObject player;
    private Vector3 movementPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movementPosition = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, 0.2f), Mathf.Lerp(transform.position.y, player.transform.position.y, 0.2f), transform.position.z);

        chaserSpeed = baseVelocity.magnitude;
        baseVelocity = (rb.transform.forward * speed);

    }
    void MoveChaser()
    {

    }
    private void FixedUpdate()
    {
        rb.velocity = baseVelocity;
        transform.position = movementPosition;
    }


}
