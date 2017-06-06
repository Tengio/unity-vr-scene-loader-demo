using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tengio {
    [RequireComponent(typeof(Image))]
    public class FadeScreen : MonoBehaviour {

        [SerializeField]
        private float duration = 0.2F;

        private Image image;
        private Coroutine fadeOutCoroutine;
        private Coroutine fadeInCoroutine;


        private void Awake() {
            image = GetComponent<Image>();
        }

        public void FadeOut(Action callback = null) {
            CancelPendingFades();
            fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(callback));
        }

        public void FadeIn(Action callback = null) {
            CancelPendingFades();
            fadeInCoroutine = StartCoroutine(FadeInCoroutine(callback));
        }

        private IEnumerator FadeInCoroutine(Action callback) {
            Color color = image.color;
            color.a = 1F;
            float startTime = Time.unscaledTime;
            while (Time.unscaledTime - startTime <= duration) {
                color.a -= Time.unscaledDeltaTime / duration;
                color.a = Mathf.Clamp01(color.a);
                image.color = color;
                yield return null;
            }
            color.a = 0F;
            image.color = color;
            if (callback != null) {
                callback();
            }
        }

        private IEnumerator FadeOutCoroutine(Action callback) {
            Color color = image.color;
            color.a = 0F;
            float startTime = Time.unscaledTime;
            while (Time.unscaledTime - startTime <= duration) {
                color.a += Time.unscaledDeltaTime / duration;
                color.a = Mathf.Clamp01(color.a);
                image.color = color;
                yield return null;
            }
            color.a = 1F;
            image.color = color;

            if (callback != null) {
                callback();
            }
        }

        private void CancelPendingFades() {
            if (fadeOutCoroutine != null) {
                StopCoroutine(fadeOutCoroutine);
            }
            if (fadeInCoroutine != null) {
                StopCoroutine(fadeInCoroutine);
            }
        }
    }
}