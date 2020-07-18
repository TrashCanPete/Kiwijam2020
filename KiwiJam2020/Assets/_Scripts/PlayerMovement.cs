using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //player moving forward
    //standard speeed, max boost speed, slowest braking speed
    //health goes down from boost but slowly goes back up
    public bool engineAlive = true;
    [SerializeField] private float speed;
    public GameObject endText;

    [SerializeField] private float rotationSpeed;
    private Vector3 baseVelocity;
    private Vector3 playerVelocity;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxSpeedValue;

    [SerializeField] private float boostSpeed;
    [SerializeField] private float boostSpeedValue;
    private Rigidbody rb;
    [SerializeField] private float offset;
    [SerializeField] private float camFollowDist;
    [SerializeField] private float camHeightDist;

    private Vector3 rot;
    private float pitch;
    private float yaw;

    [SerializeField] private bool isBoosting;

    [SerializeField] GameObject camAnchor;

    public Transform target;
    public Slider health;
    public float engineHealth;
    [SerializeField] private float maxDamage;
    [SerializeField] private float repairRate;
    [SerializeField] private float damageRate;

    public ChaserMovement chaserScript;

    // Start is called before the first frame update
    void Start()
    {
        engineAlive = true;
        isBoosting = false;
        rot = transform.eulerAngles;
        rb = GetComponent<Rigidbody>();
        endText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        health.value = engineHealth;
        health.value = Mathf.Clamp(health.value, 0, 1);
        engineHealth = Mathf.Clamp(health.value, 0, 1);
        CamFollow();

        playerSpeed = baseVelocity.magnitude;
        baseVelocity = (rb.transform.forward * speed);
        if (engineAlive)
        {
            pitch = rotationSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            yaw = rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isBoosting = true;

            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isBoosting = false;

            }
        }
        if(engineHealth >= maxDamage)
        {
            engineAlive = false;
            rb.velocity = Vector3.zero;
            isBoosting = false;
            chaserScript.chaserSideSpeed = 25;
            chaserScript.speed = 36;
        }
    }
    private void FixedUpdate()
    {
        playerVelocity = rb.velocity;
        Movement();
        PlayerRotation();
    }

    public void PlayerRotation()
    {
        rot.x += pitch;
        rot.x = Mathf.Clamp(rot.x, -45, 45);
        rot.y += yaw;
        rot.y = Mathf.Clamp(rot.y, -45, 45);
        rb.transform.rotation = Quaternion.Euler(rot);
    }
    public void Movement()
    {
        rb.velocity = baseVelocity;
        if (isBoosting == false)
        {
            engineHealth -= repairRate * Time.deltaTime;
            rb.velocity = transform.forward * speed;

            if(playerSpeed > speed)
            {
                Debug.Log("bigger than speed");
                baseVelocity.z -= 1;
                rb.velocity -= transform.forward * 1;
            }
            
        }
        else if (isBoosting == true)
        {
            engineHealth += damageRate * Time.deltaTime;
            StartCoroutine(BoostingLoop());

        }

    }

    IEnumerator BoostingLoop()
    {
        while (true)
        {
            rb.velocity += transform.forward * boostSpeedValue;
            yield return new WaitForSeconds(0.25f);
        }
    }
    IEnumerator ReducingSpeed()
    {
        while (playerSpeed > speed)
        {
            rb.velocity -= transform.forward * 5;
            yield return new WaitForSeconds(1);
        }
        StopCoroutine(ReducingSpeed());
    }
    public void ReduceSpeed()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chaser")
        {
            endText.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void CamFollow()
    {
        //Camera.main.fieldOfView = Mathf.Abs(playerSpeed / offset + cameraSpeedOffset);
        //Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minFOV, maxFOV);
        Vector3 moveCamTo = camAnchor.transform.position + transform.forward * camFollowDist + Vector3.up * (camHeightDist);
        Camera.main.transform.position = moveCamTo;
        Camera.main.transform.LookAt(transform.position);
    }
}
