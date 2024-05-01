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
          * It only travels back if it ran into a dead end.
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
                
                else if (leftTile is not null)
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