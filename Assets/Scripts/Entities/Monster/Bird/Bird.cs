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
    
    public void OnCollisionStay(Collision entity)
    {
        if (entity.gameObject.tag.Equals("EnergyBall"))
        {
            hp -= 1;
            if (hp == 0)
            {
                Destroy(gameObject);
            }
            entity.gameObject.SetActive(false);
        }

        if (entity.gameObject.tag.Equals("Character"))
        {
            CharacterManager.Instance.TakeDamage(damage);
        }
    }
    
}
