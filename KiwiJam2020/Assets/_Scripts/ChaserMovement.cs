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
    public float chaserSideSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movementPosition = new Vector3(player.transform.position.x,player.transform.position.y, transform.position.z);

        chaserSpeed = baseVelocity.magnitude;
        baseVelocity = (rb.transform.forward * speed);

    }
    private void FixedUpdate()
    {
        rb.velocity = baseVelocity;
        transform.position = Vector3.Lerp(transform.position, movementPosition, chaserSideSpeed * Time.deltaTime);
    }


}
