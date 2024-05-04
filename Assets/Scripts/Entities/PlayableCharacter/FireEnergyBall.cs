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
            switch ( cooldown <= 0 ? "100%" :
                    cooldown <= 1 ? "80%" :
                    cooldown <= 2 ? "60%" :
                    cooldown <= 3 ? "40%" :
                    cooldown <= 4 ? "20%" : "0%")
            {
                case "100%":
                    UIManager.Instance.SetEnergySquareLoaded(0);
                    break;
                case "80%":
                    UIManager.Instance.SetEnergySquareLoaded(1);
                    break;
                case "60%":
                    UIManager.Instance.SetEnergySquareLoaded(2);
                    break;
                case "40%":
                    UIManager.Instance.SetEnergySquareLoaded(3);
                    break;
                case "20%":
                    UIManager.Instance.SetEnergySquareLoaded(4);
                    break;
                case "0%":
                    break;
            }
        }
        else
        {
            cooldown = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.R) && cooldown == 0 && !GameManager.isPaused)
        {
            Vector3 destination = SetDestination();
            firedEnergyBall.transform.position = characterTransform.position;
            firedEnergyBall.SetActive(true);
            energyBallScript.StartTravelling(destination);
            for (int i = 0; i < 5; i++)
            {
                UIManager.Instance.SetEnergySquareUnloaded(i);
            }
            cooldown = CharacterManager.Instance.fireCooldown;
        }
        UIManager.Instance.UpdateFireCooldownText(cooldown);
    }

    private Vector3 SetDestination()
    {
        Vector3 direction = characterTransform.forward;
        Vector3 destination = direction * maximumTravelDistance;
        destination += characterTransform.transform.position;
        return destination;
    }
    
}
