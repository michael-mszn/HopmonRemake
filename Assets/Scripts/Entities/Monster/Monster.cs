using System;
using Entities.Monster.AI;
using UnityEngine;

namespace Entities.Monster
{
    public abstract class Monster : MonoBehaviour
    {
        public int hp;
        public float speed;
        protected AI.AI ai;

        /*
         * Override this to customize collision (i.e. ignore EnergyBalls) or to
         * extend it.
         */
        public virtual void OnCollisionEnter(Collision entity)
        {
            if (entity.gameObject.tag.Equals("EnergyBall"))
            {
                hp -= 1;
                if (hp == 0)
                {
                    Destroy(gameObject);
                    entity.gameObject.SetActive(false);
                }
            }

            if (entity.gameObject.tag.Equals("Character"))
            {
                CharacterManager.Instance.TakeDamage();
            }
        }
    }
}