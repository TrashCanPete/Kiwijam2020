using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private int destroyTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DestroyOBJ", destroyTimer);
    }

    void DestroyOBJ()
    {
        Destroy(this.gameObject);
    }
}
