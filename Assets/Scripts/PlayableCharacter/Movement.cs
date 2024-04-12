using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterManager characterManager;

    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        characterManager = CharacterManager.Instance;
        characterManager.SetSpawnPoint(gameObject);
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position == destination)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                destination = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                destination = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination,  characterManager.getSpeed() * Time.deltaTime);
        }
    }
    
}
