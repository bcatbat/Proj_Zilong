using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {
    private AsyncOperation asyncOp;
    private float progress = 0f;
    private float time;
    private float fps = 10f;
    private void Start()
    {
        StartCoroutine(LoadScene(GlobalInfo.nextScene));
    }

    IEnumerator LoadScene(string loading)
    {
        asyncOp = SceneManager.LoadSceneAsync(loading);
        asyncOp.allowSceneActivation = false;
        yield return new WaitForSeconds(2); // for test
        asyncOp.allowSceneActivation = true;
        yield return asyncOp;
    }

    private void Update()
    {
     

    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 180, 300, 60), "Loading..." + progress);
    }

    private void DrawAnimation(Texture2D tex)
    {
        time += Time.deltaTime;
        if(time>= 1.0 / fps)
        {

        }
        GUI.DrawTexture(new Rect(100, 100, 40, 60), tex);

        GUI.Label(new Rect(100, 180, 300, 60), "Loading..." + progress);
    }

    public void RefreshSlider()
    {
        progress = asyncOp.progress * 100;
    }
}
