using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Monster.AI
{
    /*
     * Library class that allows to build custom monster AIs
     */
    public class AI : MonoBehaviour
    {
        protected List<GameObject> neighbouringTiles = new();
        protected bool isCurrentlyMoving;
        protected bool isCurrentlyFalling;
        protected Vector3 destination;
        protected Monster monsterScript;
        
        private GameObject frontTile, backTile, leftTile, rightTile;
        private GameObject destinationTile;
        
        public virtual void Move() {}
        
        void Awake()
        {
            SwitchButton.Switch += OnSwitch;
        }

        /*
         * Only tiles in a plus-shape around the monster are considered neighbours
         */
        protected void determineNeighbourTiles()
        {
            neighbouringTiles.Clear();
            frontTile = null;
            backTile = null;
            leftTile = null;
            rightTile = null;
            foreach (GameObject tile in LevelManager.AllTiles)
            {
                if (!Enum.IsDefined(typeof(TileIgnoreList), tile.gameObject.tag))
                {
                    Vector3 attemptedMove = transform.position + transform.right * 10;
                    if (Math.Floor(tile.transform.position.x) == Math.Floor(attemptedMove.x) &&
                        Math.Floor(tile.transform.position.z) == Math.Floor(attemptedMove.z))
                    {
                        neighbouringTiles.Add(tile);
                        rightTile = tile;
                    }
                    
                    attemptedMove = transform.position - transform.right * 10;
                    if (Math.Floor(tile.transform.position.x) == Math.Floor(attemptedMove.x) &&
                        Math.Floor(tile.transform.position.z) == Math.Floor(attemptedMove.z))
                    {
                        neighbouringTiles.Add(tile);
                        leftTile = tile;
                    }
                    
                    attemptedMove = transform.position + transform.forward * 10;
                    if (Math.Floor(tile.transform.position.x) == Math.Floor(attemptedMove.x) &&
                        Math.Floor(tile.transform.position.z) == Math.Floor(attemptedMove.z))
                    {
                        neighbouringTiles.Add(tile);
                        frontTile = tile;
                    }
                    
                    attemptedMove = transform.position - transform.forward * 10;
                    if (Math.Floor(tile.transform.position.x) == Math.Floor(attemptedMove.x) &&
                        Math.Floor(tile.transform.position.z) == Math.Floor(attemptedMove.z))
                    {
                        neighbouringTiles.Add(tile);
                        backTile = tile;
                    }
                }
            }
        }
        
        protected void Turn()
        {
            transform.LookAt(destinationTile.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        protected void determineDestination()
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
            
            Turn();
            destination = new Vector3(destinationTile.transform.position.x, gameObject.transform.position.y, destinationTile.transform.position.z);
            isCurrentlyMoving = true;
        }

        protected void Fall()
        {
            Vector3 fallDestination = transform.position + monsterScript.speed * transform.forward;
            destination = new Vector3(fallDestination.x, -200, fallDestination.z);
            transform.position = Vector3.MoveTowards(transform.position, destination, 60 * Time.deltaTime);
            Destroy(gameObject, 3f);
        }

        protected void Step()
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, monsterScript.speed * Time.deltaTime);
        }
        
        private void OnSwitch()
        {
            if (destinationTile is not null && destinationTile.tag.Equals("DeactivatedTile"))
            {
                isCurrentlyFalling = true;
            }
        }

        private void OnDestroy()
        {
            SwitchButton.Switch -= OnSwitch;
        }
        
    }
}