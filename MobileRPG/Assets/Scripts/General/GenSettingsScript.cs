using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GenSettingsScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public GameObject player;
    public TMP_Text progressTxt;
    private void Awake() {
        Application.targetFrameRate = 60;
    }

    public void LoadScene(string sceneName) {
        // if (player != null) {
        //     player.GetComponent<PlayerHandler>().SavePlayer();
        // }
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously (string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            
            progressTxt.text = "Loading... " + progress * 100f + "%";
            slider.value = progress;

            yield return null;
        }
    }
}
