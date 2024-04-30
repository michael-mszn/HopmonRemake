using UnityEngine;

namespace Entities.Monster.Tornado
{
    public class Tornado : Monster
    {
        void Start()
        {
            ai = gameObject.AddComponent<TrackPlayerPathing>();
        }

        void Update()
        {
            ai.Move();
            Spin();
        }
        
        private void Spin()
        {
            transform.GetChild(0).transform.Rotate(0, 150 * Time.deltaTime, 0, Space.Self);
            transform.GetChild(1).transform.Rotate(0, 300 * Time.deltaTime, 0, Space.Self);
            transform.GetChild(2).transform.Rotate(0, 600 * Time.deltaTime, 0, Space.Self);
            transform.GetChild(4).transform.RotateAround(gameObject.transform.position, Vector3.up, 100 * Time.deltaTime);
            transform.GetChild(5).transform.RotateAround(gameObject.transform.position, Vector3.up, 200 * Time.deltaTime);
            transform.GetChild(6).transform.RotateAround(gameObject.transform.position, Vector3.up, 350 * Time.deltaTime);
            transform.GetChild(7).transform.RotateAround(gameObject.transform.position, Vector3.up, 450 * Time.deltaTime);
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