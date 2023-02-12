using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    float seconds=10f;
    void Start()
    {
        Destroy(gameObject, seconds);
    }

   
}
