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

    [Header("Buttons")]
    public Button nucleusButton;
    public Button mitochondriaButton;
    public Button ribosomesButton;
    public Button golgiBodyButton;
    public Button cellMembraneButton;
    public Button resetButton;
    public Button exploreButton;

    void Start()
    {
        // Hook up button listeners
        if (nucleusButton != null)
            nucleusButton.onClick.AddListener(() => OnCellPartClicked("Nucleus"));

        if (mitochondriaButton != null)
            mitochondriaButton.onClick.AddListener(() => OnCellPartClicked("Mitochondria"));

        if (ribosomesButton != null)
            ribosomesButton.onClick.AddListener(() => OnCellPartClicked("Ribosomes"));

        if (golgiBodyButton != null)
            golgiBodyButton.onClick.AddListener(() => OnCellPartClicked("Golgi Body"));

        if (cellMembraneButton != null)
            cellMembraneButton.onClick.AddListener(() => OnCellPartClicked("Cell Membrane"));

        if (resetButton != null)
            resetButton.onClick.AddListener(OnResetClicked);

        if (exploreButton != null)
            exploreButton.onClick.AddListener(OnExploreClicked);

        // Hide info panel at start
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    void OnCellPartClicked(string partName)
    {
        // Call the navigation manager's shortcut method
        switch (partName)
        {
            case "Nucleus":
                navigationManager.GoToNucleus();
                break;
            case "Mitochondria":
                navigationManager.GoToMitochondria();
                break;
            case "Ribosomes":
                navigationManager.GoToRibosomes();
                break;
            case "Golgi Body":
                navigationManager.GoToGolgiBody();
                break;
            case "Cell Membrane":
                navigationManager.GoToCellMembrane();
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

    void OnExploreClicked()
    {
        // Start exploring - go to the first cell part
        navigationManager.NavigateToCellPart(0);
        UpdateInfoPanel();
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
