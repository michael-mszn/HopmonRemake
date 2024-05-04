using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Setup
{
    /*
     * Use this class when you need to instantiate and hide GameObjects repeatedly 
     */
    public class ObjectPool
    {
        private List<GameObject> pool;
        private int poolSize;
        private GameObject itemPrefab;

        public ObjectPool(GameObject itemPrefab, int poolSize)
        {
            this.itemPrefab = itemPrefab;
            this.poolSize = poolSize;
            pool = new List<GameObject>();
            GeneratePool();
        }

        private void GeneratePool()
        {
            for(int i  = 0; i < poolSize; i++)
            {
                Console.WriteLine("Generating PoolItem " + i);
                GameObject poolItem = MonoBehaviour.Instantiate(itemPrefab); 
                poolItem.transform.rotation = Quaternion.identity;
                pool.Add(poolItem);
                poolItem.SetActive(false);
            }
        }

        public GameObject GetPoolItem()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeSelf)
                {
                    pool[i].SetActive(true);
                    return pool[i];
                }
            }

            return null;
        }

        public List<GameObject> GetPool()
        {
            return pool;
        }
        
    }
}