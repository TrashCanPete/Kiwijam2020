using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private int destroyTimer;
    private int xSpin;
    private int ySpin;
    private int zSpin;
    [SerializeField] private float spinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(xSpin, ySpin, zSpin);
    }

    // Update is called once per frame
    void Update()
    {
        xSpin = Random.Range(0, 360);
        ySpin = Random.Range(0, 360);
        zSpin = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(xSpin, ySpin, zSpin);

        Invoke("DestroyOBJ", destroyTimer);
    }

    void DestroyOBJ()
    {
        Destroy(this.gameObject);
    }
}
