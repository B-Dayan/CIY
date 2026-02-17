using UnityEngine;
using UnityEngine.UI;

public class ButtonSubscriber : MonoBehaviour
{
    [Tooltip("The index of the level that this button will load.")]
    public int levelIndex;
    [Tooltip("The index of the scene that this button will load.")]
    public int sceneIndex;
    private Button _btn;
    void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener( () => GameInfo.LoadLevel(levelIndex, sceneIndex) );
    }
}
