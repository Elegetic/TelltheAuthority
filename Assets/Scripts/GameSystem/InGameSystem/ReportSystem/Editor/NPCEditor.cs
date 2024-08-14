using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(NPC))]
public class NPCEditor : Editor
{
    private AgencySystem agencySystem;

    private void OnEnable()
    {
        string[] guids = AssetDatabase.FindAssets("t:AgencySystem", new[] { "Assets/Prefabs/System_Prefabs" });
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            agencySystem = AssetDatabase.LoadAssetAtPath<AgencySystem>(path);
        }
    }

    public override void OnInspectorGUI()
    {
        NPC npc = (NPC)target;

        if (agencySystem != null)
        {
            EditorGUILayout.LabelField("NPC Settings", EditorStyles.boldLabel);

            npc.isCriminal = EditorGUILayout.Toggle("Is Criminal", npc.isCriminal);

            EditorGUILayout.LabelField("Report Agencies", EditorStyles.boldLabel);

            var agencyNames = agencySystem.agencies.Select(a => a.agencyName).ToArray();

            if (npc.reportAgency == null || npc.reportAgency.Length == 0)
            {
                npc.reportAgency = new string[1];
            }

            for (int i = 0; i < npc.reportAgency.Length; i++)
            {
                int selectedIndex = System.Array.IndexOf(agencyNames, npc.reportAgency[i]);
                if (selectedIndex < 0) selectedIndex = 0;

                selectedIndex = EditorGUILayout.Popup("Agency " + (i + 1), selectedIndex, agencyNames);
                npc.reportAgency[i] = agencyNames[selectedIndex];
            }

            if (GUILayout.Button("Add Agency"))
            {
                ArrayUtility.Add(ref npc.reportAgency, agencySystem.agencies[0].agencyName);
            }

            if (GUILayout.Button("Remove Last Agency") && npc.reportAgency.Length > 1)
            {
                ArrayUtility.RemoveAt(ref npc.reportAgency, npc.reportAgency.Length - 1);
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(npc);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("AgencySystem not found. Please make sure you have an AgencySystem asset in the Assets/Prefabs/System_Prefabs folder.", MessageType.Warning);
        }
    }
}