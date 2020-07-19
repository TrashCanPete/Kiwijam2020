using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator monsterAnim;
    public Animator healthHandleAnim;

    public bool mouthOpen;

    public bool engineAlive = true;
    [SerializeField] private float speed;
    public GameObject endText;

    public GameObject walls;

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

    [SerializeField] private bool hasEngine;

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

    public ParticleSystem jet;
    public ParticleSystem engineBlowUp;
    public ParticleSystem boostParticle;
    public ParticleSystem engineSparks;
    public ParticleSystem explosionParticle;

    public ParticleManager particleManager;
    public CamFollow camF;

    public GameObject engineCritical;
    public GameObject engineDanger;
    [SerializeField] private float criticalValue;
    [SerializeField] private float DangerValue;

    [SerializeField] private Vector3 addForce;

    public GameObject shipEngine;
    public GameObject ship;
    public GameObject shipBody;
    public GameObject enginePieces;
    public GameObject shipPieces;

    public AudioManager audio;


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

        engineSparks.Stop();
        explosionParticle.Stop();
        boostParticle.Stop();
        engineBlowUp.Stop();

        mouthOpen = true;
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                healthHandleAnim.SetBool("HandleShake", true); 
                isBoosting = true;
                engineSparks.Play();
                boostParticle.Play();

            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                healthHandleAnim.SetBool("HandleShake", false);
                isBoosting = false;
                boostParticle.Stop();


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

            //engine
            var tempPos = new Vector3(shipEngine.transform.position.x, shipEngine.transform.position.y, shipEngine.transform.position.z);
            Destroy(shipEngine.gameObject);
            Instantiate(shipPieces, tempPos, Quaternion.identity);
            hasEngine = false;
            engineSparks.Play();
            engineBlowUp.Play();
            jet.Stop();
            boostParticle.Stop();

            rb.velocity = transform.forward * 24;

        }

        //closed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            monsterAnim.SetBool("Close Mouth", true);

            monsterAnim.SetBool("Half Closed", false);
            monsterAnim.SetBool("Open Mouth", false);
        }
        //half open
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mouthOpen = true;

            monsterAnim.SetBool("Half Closed", true);

            monsterAnim.SetBool("Open Mouth", false);
            monsterAnim.SetBool("Close Mouth", false);
        }
        //open
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mouthOpen = false;

            monsterAnim.SetBool("Open Mouth", true);

            monsterAnim.SetBool("Half Closed", false);
            monsterAnim.SetBool("Close Mouth", false);

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
            camF.anchorNumber = 3;
            //endText.SetActive(true);
            closed();
            Invoke("BackToMenu", 2.5f);
        }
        //inside
        if (other.tag == "Anchor1")
        {
            camF.anchorNumber = 0;
            //monsterAnim.SetTrigger("Close Mouth");
            
        }
        //Entrance of mouth
        if (other.tag == "Anchor2")
        {
            camF.anchorNumber = 1;
            HalfClosed();
            audio.PlayAudio("Fog");
        }
        //Outside
        if(other.tag == "Anchor3")
        {
            
            camF.anchorNumber = 2;
            MouthOpen();
        }

    }

    void MouthOpen()
    {
        mouthOpen = false;

        monsterAnim.SetBool("Open Mouth", true);

        monsterAnim.SetBool("Half Closed", false);
        monsterAnim.SetBool("Close Mouth", false);
    }

    void HalfClosed()
    {
        mouthOpen = true;

        monsterAnim.SetBool("Half Closed", true);

        monsterAnim.SetBool("Open Mouth", false);
        monsterAnim.SetBool("Close Mouth", false);
    }

    void closed()
    {
        monsterAnim.SetBool("Close Mouth", true);

        monsterAnim.SetBool("Half Closed", false);
        monsterAnim.SetBool("Open Mouth", false);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            if(hasEngine == true)
            {
                Destroy(ship.gameObject);
                var bodyTempPos = new Vector3(shipEngine.transform.position.x, shipEngine.transform.position.y, shipEngine.transform.position.z);
                Instantiate(enginePieces, bodyTempPos, Quaternion.identity);
                explosionParticle.Play();

                var engineTempPos = new Vector3(shipEngine.transform.position.x, shipEngine.transform.position.y, shipEngine.transform.position.z);
                Instantiate(shipPieces, engineTempPos, Quaternion.identity);
                engineAlive = false;
                engineBlowUp.Play();
                jet.Stop();
                boostParticle.Stop();

            }
            else if (hasEngine == false)
            {
                Destroy(ship.gameObject);
                var tempPos = new Vector3(shipEngine.transform.position.x, shipEngine.transform.position.y, shipEngine.transform.position.z);
                Instantiate(shipPieces, tempPos, Quaternion.identity);
                engineAlive = false;
                explosionParticle.Play();
                jet.Stop();
                boostParticle.Stop();
            }
        }
    }

}
