using UnityEngine;

namespace Entities.Monster.Tornado
{
    public class Tornado : Monster
    {
        private AI.AI trackPlayerPathingai;
        private bool hasFoundPlayer;
        void Start()
        {
            ai = gameObject.AddComponent<RandomPathing>();
            trackPlayerPathingai = gameObject.AddComponent<TrackPlayerPathing>();
            TrackPlayerPathing.PlayerFound += OnPlayerFound;

        }

        void Update()
        {
            trackPlayerPathingai.Move();
            Spin();
            if (!hasFoundPlayer)
            {
                ai.Move();
            }

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
                Destroy(gameObject);
                UIManager.Instance.ShowGameOver();
            }
        }

        private void OnPlayerFound()
        {
            hasFoundPlayer = true;
            print("Switched AI");
        }

        private void OnDestroy()
        {
            TrackPlayerPathing.PlayerFound -= OnPlayerFound;
        }
    }
}