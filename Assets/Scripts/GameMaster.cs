using UnityEngine;

namespace Tengio {
	public class GameMaster : MonoBehaviour {

        [SerializeField]
        private SceneLoader sceneLoader;

        [SerializeField]
        private FadeScreen fadeScreen;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.A) || GvrController.ClickButtonDown) {
                sceneLoader.LoadScene("Scene1");
            }
            if (Input.GetKeyDown(KeyCode.B) || GvrController.AppButtonDown) {
                sceneLoader.LoadScene("Scene2");
            }

            if (Input.GetKeyDown(KeyCode.O)) {
                fadeScreen.FadeOut();
            }
            if (Input.GetKeyDown(KeyCode.I)) {
                fadeScreen.FadeIn();
            }
        }
    }
}
