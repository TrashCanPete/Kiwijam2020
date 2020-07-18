using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //camera states
    //Engine Exploding
    //Colliding with Asteroidss
    public bool engineAlive = true;
    [SerializeField] private float speed;
    public GameObject endText;
    

    [SerializeField] private float rotationSpeed;
    private Vector3 baseVelocity;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private Vector3 minVelocity, maxVelocity;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxSpeedValue;

    [SerializeField] private float boostSpeed;
    [SerializeField] private float boostSpeedValue;
    private Rigidbody rb;
    [SerializeField] private float offset;


    private Vector3 rot;
    private float pitch;
    private float yaw;

    [SerializeField] private bool isBoosting;



    public Transform target;
    public Slider health;
    public float engineHealth;
    [SerializeField] private float maxDamage;
    [SerializeField] private float repairRate;
    [SerializeField] private float damageRate;

    public ChaserMovement chaserScript;
    public ParticleManager particleManager;
    public CamFollow camF;

    public GameObject engineCritical;
    public GameObject engineDanger;
    [SerializeField] private float criticalValue;
    [SerializeField] private float DangerValue;

    [SerializeField] private Vector3 addForce;


    // Start is called before the first frame update
    void Start()
    {

        minVelocity = transform.forward * 26;
        maxVelocity = transform.forward * 50;
        particleManager = GetComponentInChildren<ParticleManager>();
        engineCritical.SetActive(false);
        engineDanger.SetActive(false);
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

        baseVelocity = (rb.transform.forward * speed);
        playerSpeed = baseVelocity.magnitude;

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
            if (health.value >= criticalValue)
            {
                engineCritical.SetActive(true);
                engineDanger.SetActive(false);
            }
            else if (health.value < criticalValue && health.value > DangerValue)
            {
                engineCritical.SetActive(false);
                engineDanger.SetActive(true);
            }
            else if (health.value < DangerValue)
            {
                engineDanger.SetActive(false);
            }
        }
        
        if(engineHealth >= maxDamage)
        {
            particleManager.explode = true;
            engineAlive = false;
            rb.velocity = Vector3.zero;
            isBoosting = false;
            chaserScript.chaserSideSpeed = 25;
            chaserScript.speed = 36;
            engineDanger.SetActive(false);
            engineCritical.SetActive(false);
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
        rb.AddForce(baseVelocity + addForce);
        //rb.velocity = baseVelocity;
        rb.velocity -= transform.forward * 1;
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
            //rb.velocity += transform.forward * boostSpeedValue;
            addForce = transform.forward * boostSpeed;
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chaser")
        {
            endText.SetActive(true);
            Invoke("BackToMenu", 1);
        }
        if (other.tag == "Anchor1")
        {
            camF.anchorNumber = 0;
        }
        if (other.tag == "Anchor2")
        {
            camF.anchorNumber = 1;
        }
        if(other.tag == "Anchor3")
        {
            camF.anchorNumber = 2;
        }
    }
    void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
