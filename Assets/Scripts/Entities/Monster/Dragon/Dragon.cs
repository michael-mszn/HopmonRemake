using System.Collections;
using System.Collections.Generic;
using Entities.Monster;
using UnityEngine;

public class Dragon : Monster
{
    public float rageSpeed;
    public float rageDuration;
    public Material hurtMaterial;
    private bool isInRageMode;
    private float rageChannelTimer;
    private float normalSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        ai = gameObject.AddComponent<RandomPathing>();
        normalSpeed = speed;
        isInRageMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInRageMode)
        {
            ai.Move();
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
            entity.gameObject.SetActive(false);
        }

        if (entity.gameObject.tag.Equals("Character"))
        {
            CharacterManager.Instance.TakeDamage();
        }
    }

    private void ChangeColor()
    {
        //transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", hurtTexture);
        SkinnedMeshRenderer meshRenderer = transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();
        meshRenderer.material = hurtMaterial;
    }
}
