using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    static void Save(int currentSceneId)
    {
        PlayerPrefs.SetInt("level", currentSceneId);
    }

    static void Load()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level"));
    }

}
