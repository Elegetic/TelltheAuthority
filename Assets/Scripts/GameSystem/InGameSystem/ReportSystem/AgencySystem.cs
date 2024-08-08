using UnityEngine;

[CreateAssetMenu(fileName = "AgencySystem", menuName = "AgencySystem")]
public class AgencySystem : ScriptableObject
{
    [System.Serializable]
    public class AgencyData
    {
        public string agencyName;
        public string agencyDescription;
        public string abbreviation;
    }

    public AgencyData[] agencies;
}