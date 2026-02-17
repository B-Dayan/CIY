using UnityEngine;

// Activates and deactivates gameObjects when the level or scene is changed //

public class LevelObjectManager : MonoBehaviour
{
    [Tooltip("The first object corresponds to level 0.")]
    [SerializeField] private GameObject[] lvlGameObjects;

    [Tooltip("The index of the scene which these GameObjects belong to.")]
    [SerializeField] private int sceneIndex;


    private void Awake()
    {
        SetAllInactive();
    }

    public void UpdateLevel()
    {
        if (GameInfo.scene != sceneIndex)
        {
            SetAllInactive();
            return;
        }

        lvlGameObjects[GameInfo.level].SetActive(true);

        // Deactivate all levels other than the current one, so levels can be loaded in any order
        for (int i = 0; i < GameInfo.levelsInEachScene[GameInfo.scene]; i++)
        {
            if (i != GameInfo.level)
            {
                lvlGameObjects[i].SetActive(false);
            }
        }
    }

    private void SetAllInactive()
    {
        foreach (GameObject obj in lvlGameObjects)
        {
            obj.SetActive(false);
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
