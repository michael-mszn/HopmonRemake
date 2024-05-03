using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Persistence
{
    /*
     * Make the class static so only one single save file can always exist
     */
    public static class PersistPlayerData
    {
        private static string savePath = Application.persistentDataPath + "/playerdata.hop";
        
        public static void SaveProgress(List<LevelData> levelData, int highestLevelUnlocked)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Create);

            PlayerData data = new PlayerData(levelData, highestLevelUnlocked);
            
            formatter.Serialize(stream, data);
            stream.Close();
        }
        
        public static PlayerData LoadPlayer()
        {
            if (File.Exists(savePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(savePath, FileMode.Open);

                PlayerData data = (PlayerData) formatter.Deserialize(stream);
                stream.Close();

                return data;

            }
            else
            {
                Debug.LogError("Save file not found in" + savePath);
                return null;
            }
        }
    }
}