using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Monster;
using UnityEngine;

public class Bird : Monster
{
    
    // Start is called before the first frame update
    void Start()
    {
        ai = gameObject.AddComponent<RandomPathing>(); 
    }

    // Update is called once per frame
    void Update()
    {
        ai.Move();
    }
    
}
