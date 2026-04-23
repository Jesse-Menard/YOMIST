using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCalling : MonoBehaviour
{
    public void PushSceneByName(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
    }

    public void PopSceneByName(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }

    public void QuitApp()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
