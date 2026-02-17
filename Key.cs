using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Key : MonoBehaviour
{
    private void ResetKey()
    {
        GameInfo.isLevelObjectiveMet = false;
        this.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D()
    {
        GameInfo.isLevelObjectiveMet = true;
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameInfo.PlayerDies.AddListener( ResetKey );
    }

    private void OnDisable()
    {
        GameInfo.PlayerDies.RemoveListener( ResetKey );
    }
}
