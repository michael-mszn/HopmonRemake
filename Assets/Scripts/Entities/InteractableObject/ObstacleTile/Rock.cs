using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OnCollisionEnter(Collision entity)
    {
        if (entity.gameObject.tag.Equals("EnergyBall"))
        {
            gameObject.transform.parent.gameObject.GetComponent<ObstacleTile>().DestroyObstacle();
            entity.gameObject.SetActive(false);
        }
    }
}
