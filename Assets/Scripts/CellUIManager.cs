using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellUIManager : MonoBehaviour
{
    [Header("References")]
    public CellNavigationManager navigationManager;

    [Header("Info Panel")]
    [Tooltip("The panel that shows cell part info")]
    public GameObject infoPanel;

    [Tooltip("Text that displays the cell part name")]
    public TextMeshProUGUI partNameText;

    [Tooltip("Text that displays the cell part description")]
    public TextMeshProUGUI partDescriptionText;

    [Header("Cell Part Buttons")]
    public Button nucleusButton;
    public Button nucleolusButton;
    public Button nuclearMembraneButton;
    public Button vacuoleMitochondriaButton;
    public Button lysosomeButton;
    public Button cellMembraneButton;
    public Button golgiComplexButton;
    public Button roughEndoplasmicReticulumButton;
    public Button ribosomesButton;

    [Header("Control Buttons (Optional)")]
    public Button resetButton;

    void Start()
    {
        // Hook up cell part button listeners
        if (nucleusButton != null)
            nucleusButton.onClick.AddListener(() => OnCellPartClicked("Nucleus"));

        if (nucleolusButton != null)
            nucleolusButton.onClick.AddListener(() => OnCellPartClicked("Nucleolus"));

        if (nuclearMembraneButton != null)
            nuclearMembraneButton.onClick.AddListener(() => OnCellPartClicked("Nuclear Membrane"));

        if (vacuoleMitochondriaButton != null)
            vacuoleMitochondriaButton.onClick.AddListener(() => OnCellPartClicked("Vacuole/Mitochondria"));

        if (lysosomeButton != null)
            lysosomeButton.onClick.AddListener(() => OnCellPartClicked("Lysosome"));

        if (cellMembraneButton != null)
            cellMembraneButton.onClick.AddListener(() => OnCellPartClicked("Cell Membrane"));

        if (golgiComplexButton != null)
            golgiComplexButton.onClick.AddListener(() => OnCellPartClicked("Golgi Complex"));

        if (roughEndoplasmicReticulumButton != null)
            roughEndoplasmicReticulumButton.onClick.AddListener(() => OnCellPartClicked("Rough Endoplasmic Reticulum"));

        if (ribosomesButton != null)
            ribosomesButton.onClick.AddListener(() => OnCellPartClicked("Ribosomes"));

        if (resetButton != null)
            resetButton.onClick.AddListener(OnResetClicked);

        // Hide info panel at start
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    void OnCellPartClicked(string partName)
    {
        // Navigate camera to the cell part
        switch (partName)
        {
            case "Nucleus":
                navigationManager.GoToNucleus();
                break;
            case "Nucleolus":
                navigationManager.GoToNucleolus();
                break;
            case "Nuclear Membrane":
                navigationManager.GoToNuclearMembrane();
                break;
            case "Vacuole/Mitochondria":
                navigationManager.GoToVacuoleMitochondria();
                break;
            case "Lysosome":
                navigationManager.GoToLysosome();
                break;
            case "Cell Membrane":
                navigationManager.GoToCellMembrane();
                break;
            case "Golgi Complex":
                navigationManager.GoToGolgiComplex();
                break;
            case "Rough Endoplasmic Reticulum":
                navigationManager.GoToRoughEndoplasmicReticulum();
                break;
            case "Ribosomes":
                navigationManager.GoToRibosomes();
                break;
        }

        // Update info panel
        UpdateInfoPanel();
    }

    void OnResetClicked()
    {
        navigationManager.ResetView();

        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    void UpdateInfoPanel()
    {
        if (infoPanel != null)
            infoPanel.SetActive(true);

        if (partNameText != null)
            partNameText.text = navigationManager.GetCurrentPartName();

        if (partDescriptionText != null)
            partDescriptionText.text = navigationManager.GetCurrentPartDescription();
    }
}
