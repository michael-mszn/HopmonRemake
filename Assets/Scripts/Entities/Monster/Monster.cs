using System;
using System.Collections.Generic;
using Entities.Monster.AI;
using UnityEngine;

namespace Entities.Monster
{
    public abstract class Monster : MonoBehaviour
    {
        public int hp;
        public float speed;
        protected AI.AI ai;
    }
}