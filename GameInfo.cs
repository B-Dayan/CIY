using UnityEngine;
using UnityEngine.Events;

public class GameInfo : ScriptableObject
{
    public static int level;
    public static int scene;
    public static readonly int[] levelsInEachScene = {4, 1};
    private static readonly Vector2 _abovePosition =  new Vector2(0, 4.5f);
    public static bool isLevelObjectiveMet = true;
    public static UnityEvent UpdateLevel = new UnityEvent();
    public static UnityEvent PlayerDies = new UnityEvent();

    public static Vector2 GetResetPos()
    {
        if (scene == 0)
        {
            if (level != 0)
            {
                return _abovePosition;
            }
        }
        else
        {
            //
        }

        return Vector2.zero;
    }
    public static void LoadNextLevel()
    {
        if (level < levelsInEachScene[scene] - 1)
        {
            level++;
        }
        else
        {
            scene++;
            level = 0;
        }

        Debug.Log($"Level is {level}, scene is {scene}");
        LoadLevel(level, scene);
    }

    public static void LoadLevel(int lvl, int scn)
    {
        if (scene >= levelsInEachScene.Length)
        {
            CompleteGame();
            return;
        }

        level = lvl;
        scene = scn;

        isLevelObjectiveMet = false;
        UpdateLevel.Invoke();
    }

    public static void OnDeath()
    {
        isLevelObjectiveMet = false;
    }

    public static void CompleteGame()
    {
        Debug.Log("Completed!");
        MenuManager.OnExit();
    }
}

/*
    private static readonly Dictonary<string, Vector2> _restartPositions = new Dictonary<string, Vector2>
    {
        {center, Vector2.zero},
        {above, new Vector2(0, 4.5f)}
    };

    public static Vector2 GetResetPos()
    {
        if ( _restartPositions.TryGetValue(controlString, out var pos))
        {
            return pos;
        }
        else
        {
            Debug.LogError($"Error: \"{controlString}\" does not exist in the dictionary.");
        }
    }
*/