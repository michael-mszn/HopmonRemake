using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities.Monster.AI.FixedPathing
{
    public class FixedPathing : AI
    {
        /*
         * The tile the monster will exclusively travel on.
         */
        private GameObject pathTile;
        
        private List<GameObject> pathTileList = new();

        public override void Move()
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

        protected override void DetermineNeighbourTiles()
        {
            neighbouringTiles.Clear();
            frontTile = null;
            backTile = null;
            leftTile = null;
            rightTile = null;
            foreach (GameObject tile in pathTileList)
            {
                if (pathTile.gameObject.tag.Equals(tile.gameObject.tag))
                {
                    ScanIfNeighbourTile(tile);
                }
            }
        }
        
        /*
         * A monster with FixedPathing AI is not allowed to have more than 2 neighbouring tiles that
         * correspond with its pathTile.
         * Additionally, it only travels back if it ran into a dead end.
         */
        protected override void DetermineDestination()
        {
            //1 Neighbour
            if (neighbouringTiles.Count == 1)
            {
                destinationTile = neighbouringTiles[0];
            }

            //2 Neighbours
            else
            {
                if (frontTile is not null)
                {
                    destinationTile = frontTile;
                }
                
                else if (rightTile is not null)
                {
                    destinationTile = rightTile;
                }
                
                if (leftTile is not null)
                {
                    destinationTile = leftTile;
                }
            }
            
            SetDestination();
        }
        
        public void SetPathTile(GameObject prefab)
        {
            pathTile = prefab;
        }
        
        public void SetPathTileList(List<GameObject> list)
        {
            pathTileList = list;
        }
    }
}