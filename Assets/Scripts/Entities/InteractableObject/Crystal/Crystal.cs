using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OnCollisionEnter(Collision entity)
    {
        if (entity.gameObject.tag.Equals("Character"))
        {
            CharacterManager.Instance.AddCrystal();
            Destroy(gameObject);
        }
    }
}
