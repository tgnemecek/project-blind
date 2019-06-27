using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditRenderQueue : MonoBehaviour
{
    [Range(1, 5000)]
    public int renderQueue = 3000;

    [SerializeField]
    bool isParticleSystem = false;

    Material material;

    void Start()
    {
        if (isParticleSystem)
        {
            material = gameObject.GetComponent<ParticleSystemRenderer>().material;
        }
        else
        {
            material = gameObject.GetComponent<Renderer>().material;
        }
        material.renderQueue = renderQueue;
    }
}