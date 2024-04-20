using System;
using System.Collections;
using System.Collections.Generic;
using Entities.InteractableObject.EnergyBall;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float projectileSpeed;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, projectileSpeed * Time.deltaTime);
        if (transform.position == destination)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartTravelling(Vector3 _destination)
    {
        destination = _destination;
    }

        public void OnCollisionEnter(Collision entity)
        {
            foreach (string nonPenetrable in Enum.GetNames(typeof(NonPenetrables)))
            {
                if (entity.gameObject.tag.Equals(nonPenetrable))
                {
                    gameObject.SetActive(false);
                }
            }
        }
}
