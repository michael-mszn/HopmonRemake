using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public static event Action CrystalSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        CrystalSpawn?.Invoke();
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
