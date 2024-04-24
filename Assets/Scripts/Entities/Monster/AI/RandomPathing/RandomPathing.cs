using System.Linq;
using Entities.Monster;
using Entities.Monster.AI;
using UnityEngine;

public class RandomPathing : AI
{
    // Start is called before the first frame update
    void Start()
    {
        monsterScript = gameObject.GetComponent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Move()
    {
        if (!isCurrentlyMoving)
        {
            determineNeighbourTiles();
            if (neighbouringTiles.Any())
            {
                determineDestination();
            }
        }
        else
        {
            if (transform.position != destination && !isCurrentlyFalling)
            {
                Step();
            }
            else if(transform.position == destination && !isCurrentlyFalling)
            {
                isCurrentlyMoving = false;
            }
            else if (isCurrentlyFalling)
            {
                Fall();
            }
        }
    }
    
    
}
