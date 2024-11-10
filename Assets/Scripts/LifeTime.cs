using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float lifeTime = 5f;
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
