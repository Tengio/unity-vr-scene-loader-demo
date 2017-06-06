using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tengio {
    public class SceneLoader : MonoBehaviour {

        [SerializeField]
        private FadeScreen fadeScreen;

        private bool sceneLoading;
        private Action callback;

        private void OnEnable() {
            SceneManager.activeSceneChanged += OnSceneLoaded;
        }

        private void OnDisable() {
            SceneManager.activeSceneChanged -= OnSceneLoaded;
        }

        public void LoadScene(int sceneIndex, Action callback = null, bool noFadeOut = false) {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
            if (!scene.IsValid()) {
                Debug.LogError("Can't load scene: Invalid scene index = " + sceneIndex);
                return;
            }
            LoadScene(scene.name, callback, noFadeOut);
        }

        public void LoadScene(string sceneName, Action callback = null, bool noFadeOut = false) {
            if (!sceneLoading) {
                this.callback = callback;
                sceneLoading = true;
                if (noFadeOut) {
                    SceneManager.LoadScene(sceneName);
                } else {
                    fadeScreen.FadeOut(() => {
                        SceneManager.LoadScene(sceneName);
                    });
                }
            }
        }

        private void OnSceneLoaded(Scene unused, Scene unused2) {
            if (sceneLoading) {
                if (callback != null) {
                    callback();
                    callback = null;
                }
                StartCoroutine(WaitAndFadeIn());
            }
        }

        private IEnumerator WaitAndFadeIn() {
            // Wait a few frames to avoid freeze (caused by Awake calls?).
            yield return null;
            yield return null;
            yield return null;
            fadeScreen.FadeIn(() => {
                sceneLoading = false;
            });
        }
    }
}
