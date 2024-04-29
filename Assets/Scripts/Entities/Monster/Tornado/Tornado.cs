using UnityEngine;

namespace Entities.Monster.Tornado
{
    public class Tornado : Monster
    {
        private TrackPlayerPathing trackPlayerPathingai;
        private bool hasFoundPlayer;
        void Start()
        {
            ai = gameObject.AddComponent<RandomPathing>();
            gameObject.AddComponent<TrackPlayerPathing>();
            trackPlayerPathingai = gameObject.GetComponent<TrackPlayerPathing>();

        }

        void Update()
        {
            if (!trackPlayerPathingai.HasFoundPlayer())
            {
                ai.Move();
            }
            trackPlayerPathingai.Move();
            Spin();

        }
        
        private void Spin()
        {
            transform.GetChild(0).transform.Rotate(0, 150 * Time.deltaTime, 0, Space.Self);
            transform.GetChild(1).transform.Rotate(0, 300 * Time.deltaTime, 0, Space.Self);
            transform.GetChild(2).transform.Rotate(0, 600 * Time.deltaTime, 0, Space.Self);
        }
        
        public void OnCollisionEnter(Collision entity)
        {
        
            if (entity.gameObject.tag.Equals("Character"))
            {
                CharacterManager.Instance.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        
    }
}