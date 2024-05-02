using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGen
{
    /*
     * The level generator reads from this class to determine for which acronym which
     * prefab should be generated. Passengers and Tiles are managed in separate dictionaries.
     *
     * TILE acronyms do NOT need to be distinct from PASSENGER acronyms, however
     * TILE acronyms have to be distinct from other TILE acronyms (and PASSENGERS for PASSENGERS)
     */
    public class LevelGenDictionary : MonoBehaviour
    {
        [Header("Tiles")]
        public GameObject baseTilePrefab;
        public GameObject floorTilePrefab;
        public GameObject bridgeTilePrefab;
        public GameObject spikeTilePrefab;
        public GameObject switchTilePrefab;
        public GameObject cyanSwitchTilePrefab;
        public GameObject orangeSwitchTilePrefab;
        public GameObject obstacleTilePrefab;
        public GameObject onewayTilePrefab;
        public GameObject wallTilePrefab;
        public GameObject largeWallTilePrefab;
        public GameObject voidPrefab;
        
        [Header("Passengers")]
        public Passenger crystalPrefab;
        public Passenger bonusHeartPrefab;
        public Passenger dragonPrefab;
        public Passenger cookieMonsterPrefab;
        public Passenger spikePrefab;
        public Passenger tornadoPrefab;
        public Passenger noPassengerPrefab;

        
        private Dictionary<int, Tuple<string, GameObject>> tileMap = new();

        private Dictionary<int, Tuple<string, Passenger>> passengerMap = new();

        public void InitTileMap()
        {
            tileMap.Add(0, new Tuple<string, GameObject>("BS", baseTilePrefab));
            tileMap.Add(1, new Tuple<string, GameObject>("FL", floorTilePrefab));
            tileMap.Add(2, new Tuple<string, GameObject>("BR", bridgeTilePrefab));
            tileMap.Add(3, new Tuple<string, GameObject>("SP", spikeTilePrefab));
            tileMap.Add(4, new Tuple<string, GameObject>("SW", switchTilePrefab));
            tileMap.Add(5, new Tuple<string, GameObject>("CS", cyanSwitchTilePrefab));
            tileMap.Add(6, new Tuple<string, GameObject>("OS", orangeSwitchTilePrefab));
            tileMap.Add(7, new Tuple<string, GameObject>("OB", obstacleTilePrefab));
            tileMap.Add(8, new Tuple<string, GameObject>("OW", onewayTilePrefab));
            tileMap.Add(9, new Tuple<string, GameObject>("MWALL", wallTilePrefab));
            tileMap.Add(10, new Tuple<string, GameObject>("LWALL", largeWallTilePrefab));
            tileMap.Add(11, new Tuple<string, GameObject>("-----", voidPrefab));
        }

        public void InitPassengerMap()
        {
            passengerMap.Add(0, new Tuple<string, Passenger>("CR", crystalPrefab));
            passengerMap.Add(1, new Tuple<string, Passenger>("BH", bonusHeartPrefab));
            passengerMap.Add(2, new Tuple<string, Passenger>("DR", dragonPrefab));
            passengerMap.Add(3, new Tuple<string, Passenger>("CM", cookieMonsterPrefab));
            passengerMap.Add(4, new Tuple<string, Passenger>("SP", spikePrefab));
            passengerMap.Add(5, new Tuple<string, Passenger>("TO", tornadoPrefab));
            passengerMap.Add(6, new Tuple<string, Passenger>("NP", noPassengerPrefab));
        }

        public Dictionary<int, Tuple<string, GameObject>> GetTileMap()
        {
            return tileMap;
        }
        
        public Dictionary<int, Tuple<string, Passenger>> GetPassengerMap()
        {
            return passengerMap;
        }
    }
}