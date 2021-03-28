using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Monster[] _allMonsters;

    private void OnEnable()
    {
        _allMonsters = FindObjectsOfType<Monster>();
    }

    private void Update()
    {
        if (AllMonstersAreDead())
        {
            GoToNextLevel();
        }
    }

    private bool AllMonstersAreDead()
    {
        foreach (Monster monster in _allMonsters)
        {
            if (monster.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private void GoToNextLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex + 1);
    }
}
