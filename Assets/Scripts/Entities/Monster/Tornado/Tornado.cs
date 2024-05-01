using System.Collections.Generic;
using UnityEngine;

namespace Entities.Monster.Tornado
{
    public class Tornado : Monster
    {
        private List<GameObject> modelComponents; 
        
        void Start()
        {
            ai = gameObject.AddComponent<TrackPlayerPathing>();
            modelComponents = new();

            foreach (Transform child in gameObject.transform)
            {
                modelComponents.Add(child.gameObject);
            }
        }

        void Update()
        {
            ai.Move();
            Spin();
        }
        
        private void Spin()
        {
            modelComponents[0].transform.Rotate(0, 150 * Time.deltaTime, 0, Space.Self);
            modelComponents[1].transform.Rotate(0, 300 * Time.deltaTime, 0, Space.Self);
            modelComponents[2].transform.Rotate(0, 600 * Time.deltaTime, 0, Space.Self);
            modelComponents[4].transform.RotateAround(gameObject.transform.position, Vector3.up, 100 * Time.deltaTime);
            modelComponents[4].transform.Rotate(150 * Time.deltaTime, 100 * Time.deltaTime, 0, Space.Self);
            modelComponents[5].transform.RotateAround(gameObject.transform.position, Vector3.up, 175 * Time.deltaTime);
            modelComponents[5].transform.Rotate(0, 200 * Time.deltaTime, 75 * Time.deltaTime, Space.Self);
            modelComponents[6].transform.RotateAround(gameObject.transform.position, Vector3.up, 275 * Time.deltaTime);
            modelComponents[6].transform.Rotate(50 * Time.deltaTime, 0, 150 * Time.deltaTime, Space.Self);
            modelComponents[7].transform.RotateAround(gameObject.transform.position, Vector3.up, 300 * Time.deltaTime);
            modelComponents[7].transform.Rotate(125 * Time.deltaTime, 0 , 210 * Time.deltaTime, Space.Self);
            modelComponents[8].transform.RotateAround(gameObject.transform.position, Vector3.up, 130 * Time.deltaTime);
            modelComponents[8].transform.Rotate(50 * Time.deltaTime, 0 , 75 * Time.deltaTime, Space.Self);
        }
        
        public void OnCollisionEnter(Collision entity)
        {
        
            if (entity.gameObject.tag.Equals("Character"))
            {
                //UIManager.Instance.ShowGameOver();
                gameObject.SetActive(false);
            }
        }
        
    }
}