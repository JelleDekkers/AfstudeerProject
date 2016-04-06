using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Fade : MonoBehaviour {

    [SerializeField] private float fadeInModifier = 1;
    [SerializeField] private float fadeOutModifier = 1;

    public static Fade Instance;

    private bool fading;
    private CanvasGroup fadeGroup;
    private CanvasGroup gameOverGroup;

    private void Start() {
        if (Instance == null)
            Instance = this;
        fadeGroup = transform.GetChild(0).GetComponent<CanvasGroup>();
        gameOverGroup = fadeGroup.transform.GetChild(1).GetComponent<CanvasGroup>();
    }

    public void FadeIn() {
        if (fading == true)
            return;
        StartCoroutine(FadeInCoRoutine());
    }

    public void FadeOut(bool showGameOver) {
        if (fading == true)
            return;
        StartCoroutine(FadeOutCoRoutine(showGameOver));
        gameOverGroup.gameObject.SetActive(showGameOver);
    }

    private IEnumerator FadeInCoRoutine() {
        fading = true;
        fadeGroup.gameObject.SetActive(true);
        fadeGroup.alpha = 1;
        while (fadeGroup.alpha > 0) {
            fadeGroup.alpha -= Time.deltaTime * fadeInModifier;
            yield return new WaitForEndOfFrame();
        }
        fading = false;
        fadeGroup.gameObject.SetActive(false);
        yield return null;
    }

    private IEnumerator FadeOutCoRoutine(bool showGameOverEvent) {
        fading = true;
        fadeGroup.gameObject.SetActive(true);
        fadeGroup.alpha = 0;
        while (fadeGroup.alpha < 1) {
            fadeGroup.alpha += Time.deltaTime * fadeInModifier;
            yield return new WaitForEndOfFrame();
        }
        fading = false;
        if (showGameOverEvent)
            ScreenManager.GameOverFadeComplete = true;
        yield return null;
    }
}
