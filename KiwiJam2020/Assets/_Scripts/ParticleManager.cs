using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private ParticleSystem particleManager;
    public bool explode = false;
    // Start is called before the first frame update
    void Start()
    {
        particleManager = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (explode)
        {
            particleManager.Emit(300);
            explode = false;
        }
    }
}
