﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColloderDetect : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        Debug.Log(gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
