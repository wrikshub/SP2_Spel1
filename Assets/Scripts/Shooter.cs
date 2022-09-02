using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Gun gun;

    private void Awake()
    {
        transform.GetChild(0).GetComponent<Gun>();
    }
}
