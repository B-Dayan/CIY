using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string sceneNamePrefix = "Scn_";

    private void Awake()
    {
        UpdateLevel();
    }

    public void UpdateLevel()
    {
        var scn = GameInfo.scene;

        if (SceneManager.GetActiveScene().name == sceneNamePrefix + scn) return;

        SceneManager.LoadSceneAsync(sceneNamePrefix + scn, LoadSceneMode.Additive);

        if (scn > 0)
        {
            SceneManager.UnloadSceneAsync(sceneNamePrefix + (scn - 1));
        }
    }

    private void OnEnable()
    {
        GameInfo.UpdateLevel.AddListener( UpdateLevel );
    }

    private void OnDisable()
    {
        GameInfo.UpdateLevel.RemoveListener( UpdateLevel );
    }
}
