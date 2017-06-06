using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tengio {
	public class Boot : MonoBehaviour {

        // Load permanent objects only once (prevent GVR from screaming it sees double...).
        // Boot scene should never be loaded again after.
		private void Start () {
            SceneManager.LoadScene(1);
		}
	}
}
