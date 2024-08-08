using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool isCriminal;
    public string crimeDetail;
    public string[] reportAgency;

    public void SetReportAgencies(string[] agencies)
    {
        reportAgency = agencies;
    }
}