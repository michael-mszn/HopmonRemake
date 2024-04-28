using System.Collections;
using System.Collections.Generic;
using Entities.Monster;
using Entities.Monster.AI.FixedPathing;
using UnityEngine;

public class Spike : Monster
{
    public GameObject pathTile;
    private List<GameObject> allSpikeTiles = new();
    
    // Start is called before the first frame update
    void Start()
    {
        ai = gameObject.AddComponent<FixedPathing>();
        ai.GetComponent<FixedPathing>().SetPathTile(pathTile);
        ai.GetComponent<FixedPathing>().SetPathTileList(allSpikeTiles);
        LevelManager.Instance.UpdateTileCoordinates(pathTile.tag, allSpikeTiles);
    }

    // Update is called once per frame
    void Update()
    {
        ai.Move();
        Spin();
    }
    
    public void OnCollisionStay(Collision entity)
    {
        if (entity.gameObject.tag.Equals("EnergyBall"))
        {
            entity.gameObject.SetActive(false);
        }
        
        
        if (entity.gameObject.tag.Equals("Character"))
        {
            CharacterManager.Instance.TakeDamage();
        }
    }


    private void Spin()
    {
        transform.GetChild(0).transform.Rotate(500 * Time.deltaTime, 0, 0, Space.Self);
    }
}
