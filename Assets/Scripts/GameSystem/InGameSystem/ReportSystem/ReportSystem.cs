using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ReportSystem : MonoBehaviour
{
    public LevelSystem levelSystem;
    public AgencySystem agencySystem;

    public GameObject reportUIPanel;
    public GameObject reportText;
    public TMP_Text agencyNameText;
    public TMP_Text agencyDescriptionText;
    public Button[] agencyButtons;
    public Button reportButton;
    public Button cancelButton;

    private NPC currentNPC;
    private int selectedAgencyIndex = -1;
    private int currentLevelIndex;
    private List<AgencySystem.AgencyData> currentAgencies = new List<AgencySystem.AgencyData>();

    private LocalizedText localizedNameText;
    private LocalizedText localizedDescriptionText;

    void Start()
    {
        currentLevelIndex = GetCurrentLevelIndex();
        reportUIPanel.SetActive(false);

        reportButton.onClick.AddListener(HandleReportButtonClick);
        cancelButton.onClick.AddListener(CloseReportPanel);

        localizedNameText = agencyNameText.GetComponent<LocalizedText>();
        localizedDescriptionText = agencyDescriptionText.GetComponent<LocalizedText>();
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            CheckForNPC();
            HandleReportInput();
        }

        if (reportUIPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            HandleReportButtonClick();
        }
    }

    void CheckForNPC()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            NPC npc = hit.collider.GetComponent<NPC>();
            if (npc != null)
            {
                currentNPC = npc;
                reportText.SetActive(true);
                HighlightNPC(true);
            }
            else
            {
                ClearCurrentNPC();
            }
        }
        else
        {
            ClearCurrentNPC();
        }
    }

    void ClearCurrentNPC()
    {
        if (currentNPC != null)
        {
            HighlightNPC(false);
            currentNPC = null;
            reportText.SetActive(false);
        }
    }

    void HighlightNPC(bool highlight)
    {
        if (currentNPC != null)
        {
            Renderer npcRenderer = currentNPC.GetComponent<Renderer>();
            if (npcRenderer != null)
            {
                npcRenderer.material.color = highlight ? new Color32(174, 255, 254, 255) : Color.white;
            }
        }
    }

    void HandleReportInput()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 0;
            reportUIPanel.SetActive(true);
            SetupAgencyButtons();
        }

        if (reportUIPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseReportPanel();
        }
    }

    void SetupAgencyButtons()
    {
        currentAgencies.Clear();
        List<AgencySystem.AgencyData> possibleAgencies = new List<AgencySystem.AgencyData>(agencySystem.agencies);

        if (currentNPC.isCriminal)
        {
            string correctAgencyName = currentNPC.reportAgency[Random.Range(0, currentNPC.reportAgency.Length)];
            AgencySystem.AgencyData correctAgency = possibleAgencies.Find(a => a.agencyName == correctAgencyName);
            if (correctAgency != null)
            {
                currentAgencies.Add(correctAgency);
                possibleAgencies.Remove(correctAgency);
            }
        }

        while (currentAgencies.Count < 4 && possibleAgencies.Count > 0)
        {
            int randomIndex = Random.Range(0, possibleAgencies.Count);
            currentAgencies.Add(possibleAgencies[randomIndex]);
            possibleAgencies.RemoveAt(randomIndex);
        }

        for (int i = 0; i < currentAgencies.Count; i++)
        {
            AgencySystem.AgencyData temp = currentAgencies[i];
            int randomIndex = Random.Range(0, currentAgencies.Count);
            currentAgencies[i] = currentAgencies[randomIndex];
            currentAgencies[randomIndex] = temp;
        }

        for (int i = 0; i < agencyButtons.Length; i++)
        {
            if (i < currentAgencies.Count)
            {
                agencyButtons[i].gameObject.SetActive(true);
                agencyButtons[i].GetComponentInChildren<TMP_Text>().text = currentAgencies[i].abbreviation;
                int index = i;
                agencyButtons[i].onClick.RemoveAllListeners();
                agencyButtons[i].onClick.AddListener(() => SelectAgency(index));
                agencyButtons[i].onClick.AddListener(() => UpdateAgencyDetails(index));
            }
            else
            {
                agencyButtons[i].gameObject.SetActive(false);
            }
        }

        if (currentAgencies.Count > 0)
        {
            selectedAgencyIndex = 0;
            UpdateAgencyDetails(selectedAgencyIndex);
        }
        else
        {
            selectedAgencyIndex = -1;
            UpdateAgencyDetails(selectedAgencyIndex);
        }
    }

    void SelectAgency(int index)
    {
        selectedAgencyIndex = index;
        UpdateAgencyDetails(index);
    }

    void UpdateAgencyDetails(int index)
    {
        if (index != -1 && index < currentAgencies.Count)
        {
            agencyNameText.text = currentAgencies[index].agencyName;
            agencyDescriptionText.text = currentAgencies[index].agencyDescription;

            if (localizedNameText != null)
            {
                localizedNameText.key = currentAgencies[index].agencyName;
                localizedNameText.UpdateText();
            }

            if (localizedDescriptionText != null)
            {
                localizedDescriptionText.key = currentAgencies[index].agencyDescription;
                localizedDescriptionText.UpdateText();
            }

            //Debug.Log("Current Agency Name is: " + agencyNameText.text);
            //Debug.Log("Current Agency Description is: " + agencyDescriptionText.text);
        }
        else
        {
            agencyNameText.text = string.Empty;
            agencyDescriptionText.text = string.Empty;

            //Debug.Log("Cleared Agency Details.");
        }
    }

    void HandleReportButtonClick()
    {
        if (selectedAgencyIndex != -1)
        {
            ReportToAgency(currentAgencies[selectedAgencyIndex].agencyName);
        }
    }

    public void ReportToAgency(string agency)
    {
        if (currentNPC == null || string.IsNullOrEmpty(agency)) return;

        bool isCorrectAgency = System.Array.Exists(currentNPC.reportAgency, element => element == agency);
        bool isCriminal = currentNPC.isCriminal;

        if (!isCriminal)
        {
            levelSystem.currentTotalScore -= 2;
            levelSystem.levels[currentLevelIndex].currentLevelScore -= 2;
        }
        else if (isCriminal && isCorrectAgency)
        {
            levelSystem.currentTotalScore += 2;
            levelSystem.levels[currentLevelIndex].currentLevelScore += 2;
        }
        else
        {
            levelSystem.currentTotalScore += 1;
            levelSystem.levels[currentLevelIndex].currentLevelScore += 1;
        }

        currentNPC.gameObject.SetActive(false);
        CloseReportPanel();
    }

    private void CloseReportPanel()
    {
        reportUIPanel.SetActive(false);
        Time.timeScale = 1;
        ClearCurrentNPC();
        selectedAgencyIndex = -1;
    }

    private int GetCurrentLevelIndex()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        for (int i = 0; i < levelSystem.levels.Count; i++)
        {
            if (levelSystem.levels[i].sceneName == currentSceneName)
            {
                return i;
            }
        }
        Debug.LogError("Current level index not found!");
        return -1;
    }
}
