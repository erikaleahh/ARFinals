using UnityEngine;

public class CellNavigationManager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The CameraOrbitController on the AR Camera")]
    public CameraOrbitController cameraController;

    [Header("Cell Part Targets")]
    [Tooltip("Add all cell parts here with their camera positions")]
    public CellPartTarget[] cellParts;

    // Track the currently selected cell part
    private int currentIndex = -1; // -1 means default/overview

    /// <summary>
    /// Navigate to a specific cell part by index.
    /// Hook this up to each UI Button's OnClick event.
    /// </summary>
    public void NavigateToCellPart(int index)
    {
        if (index < 0 || index >= cellParts.Length)
        {
            Debug.LogWarning($"CellNavigationManager: Invalid index {index}");
            return;
        }

        currentIndex = index;
        cameraController.NavigateTo(cellParts[index]);

        Debug.Log($"Navigated to: {cellParts[index].partName}");
    }

    // =====================================================
    // SHORTCUT METHODS - One per cell part for easy button hookup
    // These are easier to assign in Unity's Button OnClick
    // =====================================================

    public void GoToNucleus()
    {
        NavigateToCellPartByName("Nucleus");
    }

    public void GoToMitochondria()
    {
        NavigateToCellPartByName("Mitochondria");
    }

    public void GoToRibosomes()
    {
        NavigateToCellPartByName("Ribosomes");
    }

    public void GoToGolgiBody()
    {
        NavigateToCellPartByName("Golgi Body");
    }

    public void GoToCellMembrane()
    {
        NavigateToCellPartByName("Cell Membrane");
    }

    public void GoToEndoplasmicReticulum()
    {
        NavigateToCellPartByName("Endoplasmic Reticulum");
    }

    // =====================================================
    // NAVIGATION CONTROLS
    // =====================================================

    /// <summary>
    /// Go to the next cell part. Good for "Next" button.
    /// </summary>
    public void NextCellPart()
    {
        if (cellParts.Length == 0) return;
        currentIndex = (currentIndex + 1) % cellParts.Length;
        cameraController.NavigateTo(cellParts[currentIndex]);
    }

    /// <summary>
    /// Go to the previous cell part. Good for "Previous" button.
    /// </summary>
    public void PreviousCellPart()
    {
        if (cellParts.Length == 0) return;
        currentIndex--;
        if (currentIndex < 0) currentIndex = cellParts.Length - 1;
        cameraController.NavigateTo(cellParts[currentIndex]);
    }

    /// <summary>
    /// Reset camera to default overview.
    /// </summary>
    public void ResetView()
    {
        currentIndex = -1;
        cameraController.ResetToDefault();
    }

    /// <summary>
    /// Get the currently selected cell part name.
    /// </summary>
    public string GetCurrentPartName()
    {
        if (currentIndex < 0 || currentIndex >= cellParts.Length)
            return "Overview";
        return cellParts[currentIndex].partName;
    }

    /// <summary>
    /// Get the currently selected cell part description.
    /// </summary>
    public string GetCurrentPartDescription()
    {
        if (currentIndex < 0 || currentIndex >= cellParts.Length)
            return "Tap a cell part to explore.";
        return cellParts[currentIndex].description;
    }

    // =====================================================
    // PRIVATE HELPERS
    // =====================================================

    private void NavigateToCellPartByName(string partName)
    {
        for (int i = 0; i < cellParts.Length; i++)
        {
            if (cellParts[i].partName == partName)
            {
                currentIndex = i;
                cameraController.NavigateTo(cellParts[i]);
                return;
            }
        }
        Debug.LogWarning($"CellNavigationManager: Cell part '{partName}' not found!");
    }
}
