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


    // Start is called before the first frame update
    void Start()
    {
        endText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate (Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chaser")
        {
            endText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
