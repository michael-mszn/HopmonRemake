using System.Collections;
using System.Collections.Generic;
using Entities.Monster;
using UnityEngine;

public class Cookiemonster : Monster
{
    public float rageSpeed;
    public float rageDuration;
    public Material hurtMaterial;
    private bool isInRageMode;
    private float rageChannelTimer;
    private float normalSpeed;
    private float bounceTimer;
    private List<GameObject> modelComponents;
    
    // Start is called before the first frame update
    void Start()
    {
        ai = gameObject.AddComponent<RandomPathing>();
        normalSpeed = speed;
        isInRageMode = false;
        bounceTimer = 0.5f;
        modelComponents = new();
        foreach (Transform child in gameObject.transform)
        {
            modelComponents.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bounceTimer <= 0)
        {
            Bounce();
        }
        
        if (!isInRageMode)
        {
            ai.Move();
            bounceTimer -= Time.deltaTime;
        }
        else
        {
            rageChannelTimer -= Time.deltaTime;
            if (rageChannelTimer > 0)
            {
                speed = rageSpeed;
            }
            else
            {
                if (rageDuration > 0)
                {
                    ai.Move();
                    rageDuration -= Time.deltaTime;
                }
                else
                {
                    isInRageMode = false;
                    speed = normalSpeed;
                }
            }
        }
    }
    
    public void OnCollisionStay(Collision entity)
    {
        if (entity.gameObject.tag.Equals("EnergyBall"))
        {
            hp -= 1;
            if (hp == 0)
            {
                Destroy(gameObject);
            }
            isInRageMode = true;
            speed = 0;
            rageChannelTimer = 2;
            ChangeColor();
            Rage();
            entity.gameObject.SetActive(false);
        }

        if (entity.gameObject.tag.Equals("Character"))
        {
            CharacterManager.Instance.TakeDamage(damage);
        }
    }

    private void Bounce()
    {
        if (modelComponents[0].activeSelf)
            modelComponents[0].SetActive(false);
        else modelComponents[0].SetActive(true);
        
        if (modelComponents[1].activeSelf)
            modelComponents[1].SetActive(false);
        else modelComponents[1].SetActive(true);
        
        bounceTimer = 0.5f;
    }

    private void Rage()
    {
        StartCoroutine(RageAnimation());
    }
    
    
    IEnumerator RageAnimation()
    {
        bounceTimer = 0.5f;
        modelComponents[0].SetActive(true);
        modelComponents[1].SetActive(false);
        yield return new WaitForSeconds(rageChannelTimer + rageDuration);
    }
    
    private void ChangeColor()
    {
        //transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", hurtTexture);
        MeshRenderer meshRenderer  = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.SetColor("_Color", hurtMaterial.color);
        SkinnedMeshRenderer skinnedMeshRenderer = transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.material.SetColor("_Color", hurtMaterial.color);
    }
}
