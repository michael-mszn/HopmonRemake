using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnergyBall : MonoBehaviour
{
    private Transform characterTransform;
    private float cooldown;
    public GameObject energyBall;
    public int maximumTravelDistance;
    private GameObject firedEnergyBall;
    private EnergyBall energyBallScript;
    
    // Start is called before the first frame update
    void Start()
    {
        characterTransform = gameObject.GetComponent<Transform>();
        firedEnergyBall = Instantiate(energyBall, characterTransform.transform.position, characterTransform.transform.rotation);
        energyBallScript = firedEnergyBall.GetComponent<EnergyBall>();
        firedEnergyBall.SetActive(false);
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.R) && cooldown == 0 && !PauseMenu.isPaused)
        {
            Vector3 destination = SetDestination();
            firedEnergyBall.transform.position = characterTransform.position;
            firedEnergyBall.SetActive(true);
            energyBallScript.StartTravelling(destination);
            cooldown = CharacterManager.Instance.fireCooldown;
        }
    }

    private Vector3 SetDestination()
    {
        Vector3 direction = characterTransform.forward;
        Vector3 destination = direction * maximumTravelDistance;
        destination += characterTransform.transform.position;
        return destination;
    }
    
}
