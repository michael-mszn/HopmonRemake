using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{

    public static event Action Switch;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Character"))
        {
            Switch?.Invoke();
            //print("Triggered Switch Tile");
        }
    }
}
