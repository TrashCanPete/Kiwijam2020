using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //player moving forward
    //standard speeed, max boost speed, slowest braking speed
    //health goes down from boost but slowly goes back up
    [SerializeField] private float speed;
    public GameObject endText;

    [SerializeField] private float vertical;
    [SerializeField] private float horizontal;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 baseVelocity;
    [SerializeField] private float playerSpeed;
    private Rigidbody rb;
    [SerializeField] private float offset;
    [SerializeField] private float camFollowDist;
    [SerializeField] private float camHeightDist;

    [SerializeField] private Vector3 rot;
    [SerializeField] private float pitch;
    [SerializeField] private float yaw;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rot = transform.eulerAngles;
        rb = GetComponent<Rigidbody>();
        endText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CamFollow();
        PlayerInputs();
        //transform.Translate (Vector3.forward * speed * Time.deltaTime);
        playerSpeed = baseVelocity.magnitude;
        baseVelocity = (rb.transform.forward * speed);
        
        pitch = rotationSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        yaw = rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;


    }
    private void FixedUpdate()
    {
        Movement();
        PlayerRotation();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chaser")
        {
            endText.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void PlayerRotation()
    {
        rot.x += pitch;
        rot.x = Mathf.Clamp(rot.x, -45, 45);
        rot.y += yaw;
        rb.transform.rotation = Quaternion.Euler(rot);
    }
    public void Movement()
    {
        rb.velocity = baseVelocity;


    }
    public void PlayerInputs()
    {




    }
    public void CamFollow()
    {
        //Camera.main.fieldOfView = Mathf.Abs(playerSpeed / offset + cameraSpeedOffset);
        //Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minFOV, maxFOV);
        Vector3 moveCamTo = transform.position - transform.forward * camFollowDist + Vector3.up * (camHeightDist);
        Camera.main.transform.position = moveCamTo;
        Camera.main.transform.LookAt(transform.position);
    }
}
