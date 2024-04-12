using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager Instance;
    public float speed;
    private Transform baseTile;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Properly centers the playable character on the tile grid
     */
    public void SetSpawnPoint(GameObject character)
    {
        baseTile = GameObject.FindWithTag("Map").transform.Find("BaseTile").gameObject.GetComponent<Transform>();
        character.transform.position = new Vector3(baseTile.transform.position.x, baseTile.transform.position.y + 6,
            baseTile.transform.position.z);
    }

    public float getSpeed()
    {
        return speed;
    }
}
