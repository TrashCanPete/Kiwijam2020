using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float camFollowDist;
    [SerializeField] private float camHeightDist;

    [SerializeField] GameObject[] camAnchor;
    public int anchorNumber;
    void Start()
    {
        anchorNumber = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anchorNumber = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anchorNumber = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anchorNumber = 0;
        }
    }
    private void FixedUpdate()
    {
        //Camera.main.fieldOfView = Mathf.Abs(playerSpeed / offset + cameraSpeedOffset);
        //Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minFOV, maxFOV);
        Vector3 moveCamTo = camAnchor[anchorNumber].transform.position + transform.forward * camFollowDist + Vector3.up * (camHeightDist);
        Vector3 CamCurrentPos = Camera.main.transform.position;
        Camera.main.transform.position = Vector3.Lerp(CamCurrentPos, moveCamTo, 2 * Time.deltaTime);
        Camera.main.transform.LookAt(player.transform.position);

    }
}
