using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievement")]
public class AchievementData : ScriptableObject
{
    [System.Serializable]
    public class Achievement
    {
        public string achievementName;
        public string description;
        public bool isUnlocked;
        public int requiredScore;
    }

    public List<Achievement> achievements;
    public Achievement totalAchievement;
}