using System;
using Entities.Monster;
using Entities.Monster.AI;
using UnityEngine;

public class TrackPlayerPathing : AI
{
    private bool hasFoundPlayer;

    private int playerTileLogIndex;

    private RandomPathing randomPathingAI;
    
    void Start()
    {
        gameObject.AddComponent<RandomPathing>();
        randomPathingAI = GetComponent<RandomPathing>();
    }

    public override void Move()
    {
        if (!hasFoundPlayer)
        {
            DetermineNeighbourTiles();
            randomPathingAI.Move();
        }
        else
        {
            if (!isCurrentlyMoving)
            {
                DetermineDestination();
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
    }

    protected override void DetermineNeighbourTiles()
    {
        Vector3 monsterPosition = new Vector3((float) Math.Floor(transform.position.x),
            0,
            (float) Math.Floor(transform.position.z));
        if (CharacterManager.Instance.GetPlayerTileLog().Contains(monsterPosition))
        {
            hasFoundPlayer = true;
            playerTileLogIndex = CharacterManager.Instance.GetPlayerTileLog().FindIndex(a => a.Equals(monsterPosition));
        }
    }

    protected override void DetermineDestination()
    {
        destination = CharacterManager.Instance.GetPlayerTileLog()[playerTileLogIndex];
        /*
         * Set y-level of destination to monsters y-level as it spawns with a customizable y-offset
         */
        destination = new Vector3(destination.x, gameObject.transform.position.y, destination.z);
        
        playerTileLogIndex++;
        isCurrentlyMoving = true;
    }

    public bool HasFoundPlayer()
    {
        return hasFoundPlayer;
    }
}
