using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        private string levelNumber { get; set; }

        public TextMeshProUGUI levelText;

        public void SelectLevel()
        {
            levelNumber = levelText.text;
            SceneHandler.selectedLevel = levelNumber;
            if (Int32.Parse(SceneHandler.selectedLevel) <= SceneHandler.highestLevelUnlocked)
            {
                gameObject.AddComponent<SceneHandler>().LoadLevel();
            }
        }

        public void SetLevelText(string s)
        {
            levelText.text = s;
        }
    }
}