using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    /// <summary>
    /// Load a scene by name. Assign this to button OnClick events.
    /// </summary>
    public void LoadScene(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Load the Game scene.
    /// </summary>
    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Load the About scene.
    /// </summary>
    public void GoToAbout()
    {
        SceneManager.LoadScene("About");
    }

    /// <summary>
    /// Load the Landing Page scene.
    /// </summary>
    public void GoToLandingPage()
    {
        SceneManager.LoadScene("LandingPage");
    }

    /// <summary>
    /// Quit the application.
    /// </summary>
    public void ExitApp()
    {
        Debug.Log("Exiting application...");

        #if UNITY_EDITOR
            // Stop play mode in Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Quit the actual built app
            Application.Quit();
        #endif
    }
}
