using System;
using System.Linq;
using Entities.Monster;
using Entities.Monster.AI;
using Entities.Monster.AI.RandomPathing;
using UnityEngine;

public class RandomPathing : AI
{

    public override void Move()
    {
        if (!gameObject.tag.Equals("Falling"))
        {
            if (!isCurrentlyMoving)
            {
                DetermineNeighbourTiles();
                if (neighbouringTiles.Any())
                {
                    DetermineDestination();
                }
            }
            else
            {
                if (transform.position != destination)
                {
                    Step();
                }
                else
                {
                    isCurrentlyMoving = false;
                }
            }
        }
        else
        {
            StartFalling();
        }
    }
    
    /*
     * Only tiles in a plus-shape around the monster are considered neighbours
     */
    protected override void DetermineNeighbourTiles()
    {
        neighbouringTiles.Clear();
        frontTile = null;
        backTile = null;
        leftTile = null;
        rightTile = null;
        foreach (GameObject tile in LevelManager.AllTiles)
        {
            if (!Enum.IsDefined(typeof(RPTileIgnoreList), tile.gameObject.tag))
            {
                ScanIfNeighbourTile(tile);
            }
        }
    }

    protected override void DetermineDestination()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 1.0f);

        //1 Neighbour
        if (neighbouringTiles.Count == 1)
        {
            destinationTile = neighbouringTiles[0];
        }
            
        //2 Neighbours
        else if (neighbouringTiles.Count == 2)
        {
            /* Layout:
            *   N
            * □   □
            *   N
            */
            if (frontTile is null && backTile is null)
            {
                if (randomNumber <= 0.5f)
                {
                    destinationTile = rightTile;
                }
                else
                {
                    destinationTile = leftTile;
                }
            }

            /* Layout:
            *     □         □
            *   N   □     □   N
            *     N         N
            */
            else if (backTile is null && frontTile is not null)
            {
                if (randomNumber <= 0.66f)
                {
                    destinationTile = frontTile;
                }
                else if (leftTile is not null)
                {
                    destinationTile = leftTile;
                }
                else
                {
                    destinationTile = rightTile;
                }
            }
                
            /* Layout:
            *     N         N
            *   N   □     □   N
            *     □         □
            */
            else if (backTile is not null && frontTile is null)
            {
                if (leftTile is null)
                {
                    destinationTile = rightTile;
                }
                else
                {
                    destinationTile = leftTile;
                }
            }
                
            /* Layout:
            *   □
            * N   N
            *   □
            */
            else if (leftTile is null && rightTile is null)
            {
                destinationTile = frontTile;
            }
        }
            
        //3 Neighbours
        else if (neighbouringTiles.Count == 3)
        {
            /* Layout:
            *   N
            * □   □
            *   □
            */
            if (frontTile is null)
            {
                if (randomNumber <= 0.5f)
                {
                    destinationTile = rightTile;
                }
                else
                {
                    destinationTile = leftTile;
                }
            }
                
            /* Layout:
            *   □
            * □   □
            *   N
            */
            else if (backTile is null)
            {
                if (randomNumber <= 0.33f)
                {
                    destinationTile = rightTile;
                }
                else if (randomNumber > 0.33f && randomNumber <= 0.66f)
                {
                    destinationTile = frontTile;
                }
                    
                else
                {
                    destinationTile = leftTile;
                }
            }
                
            /* Layout:
            *   □
            * □   N
            *   □
            */
            else if (rightTile is null)
            {
                if (randomNumber <= 0.5f)
                {
                    destinationTile = frontTile;
                }
                else
                {
                    destinationTile = leftTile; ;
                }
            }
                
            /* Layout:
            *   □
            * N   □
            *   □
            */
            else if (leftTile is null)
            {
                if (randomNumber <= 0.5f)
                {
                    destinationTile = frontTile;
                }
                else
                {
                    destinationTile = rightTile;
                }
            }
        }
            
        //4 Neighbours
        else if (neighbouringTiles.Count == 4)
        {
            if (randomNumber <= 0.33f)
            {
                destinationTile = frontTile;
            }
            else if (0.33f < randomNumber && randomNumber <= 0.66f)
            {
                destinationTile = leftTile;
            }
                
            else 
            {
                destinationTile = rightTile;
            }
        }
        
        SetDestination();
    }    
}
