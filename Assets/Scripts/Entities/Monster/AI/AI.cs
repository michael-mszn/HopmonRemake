using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities.Monster.AI
{
    /*
     * Library class that allows to build custom monster AIs
     */
    public abstract class AI : MonoBehaviour
    {
        protected List<GameObject> neighbouringTiles = new();
        protected bool isCurrentlyMoving;
        protected GameObject destinationTile;
        protected Vector3 destination;
        protected GameObject frontTile, backTile, leftTile, rightTile;
        private Monster monsterScript;
        
        void Awake()
        {
            monsterScript = gameObject.GetComponent<Monster>();
        }

        public virtual void Move()
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
        protected abstract void DetermineNeighbourTiles();
        protected abstract void DetermineDestination();

        protected void ScanIfNeighbourTile(GameObject tile)
        {
            Vector3 attemptedMove = transform.position + transform.right * 10;
            if (Math.Round(tile.transform.position.x) == Math.Round(attemptedMove.x) &&
                Math.Round(tile.transform.position.z) == Math.Round(attemptedMove.z))
            {
                neighbouringTiles.Add(tile);
                rightTile = tile;
            }
                    
            attemptedMove = transform.position - transform.right * 10;
            if (Math.Round(tile.transform.position.x) == Math.Round(attemptedMove.x) &&
                Math.Round(tile.transform.position.z) == Math.Round(attemptedMove.z))
            {
                neighbouringTiles.Add(tile);
                leftTile = tile;
            }
                    
            attemptedMove = transform.position + transform.forward * 10;
            if (Math.Round(tile.transform.position.x) == Math.Round(attemptedMove.x) &&
                Math.Round(tile.transform.position.z) == Math.Round(attemptedMove.z))
            {
                neighbouringTiles.Add(tile);
                frontTile = tile;
            }
                    
            attemptedMove = transform.position - transform.forward * 10;
            if (Math.Round(tile.transform.position.x) == Math.Round(attemptedMove.x) &&
                Math.Round(tile.transform.position.z) == Math.Round(attemptedMove.z))
            {
                neighbouringTiles.Add(tile);
                backTile = tile;
            }
        }

        protected void SetDestination()
        {
            Turn();
            destination = new Vector3(destinationTile.transform.position.x, gameObject.transform.position.y, destinationTile.transform.position.z);
            isCurrentlyMoving = true;
        }
        
        protected void Step()
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, monsterScript.speed * Time.deltaTime);
        }
        
        private void Turn()
        {
            transform.LookAt(destinationTile.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        
        IEnumerator Fall()
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, 40 * Time.deltaTime);
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

        protected void StartFalling()
        {
            Vector3 fallDestination = transform.position + 20 * transform.forward;
            destination = new Vector3(fallDestination.x, -200, fallDestination.z);
            StartCoroutine(Fall());
        }
    }
}