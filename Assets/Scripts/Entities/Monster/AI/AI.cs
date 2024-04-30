using System;
using System.Collections.Generic;
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
        protected bool isCurrentlyFalling;
        protected GameObject destinationTile;
        protected Vector3 destination;
        protected GameObject frontTile, backTile, leftTile, rightTile;
        private Monster monsterScript;
        
        void Awake()
        {
            SwitchButton.Switch += OnSwitch;
            monsterScript = gameObject.GetComponent<Monster>();
        }
        
        public abstract void Move();
        protected abstract void DetermineNeighbourTiles();
        protected abstract void DetermineDestination();

        protected void ScanIfNeighbourTile(GameObject tile)
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

        protected void SetDestination()
        {
            Turn();
            destination = new Vector3(destinationTile.transform.position.x, gameObject.transform.position.y, destinationTile.transform.position.z);
            isCurrentlyMoving = true;
        }
        
        protected void Fall()
        {
            Vector3 fallDestination = transform.position + monsterScript.speed * transform.forward;
            destination = new Vector3(fallDestination.x, -200, fallDestination.z);
            transform.position = Vector3.MoveTowards(transform.position, destination, 40 * Time.deltaTime);
            Destroy(gameObject, 3f);
        }
        
        protected void Step()
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, monsterScript.speed * Time.deltaTime);
        }
        
        protected void Turn()
        {
            transform.LookAt(destinationTile.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
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