using UnityEngine;

namespace UberPlanetary
{
    public class ProxySceneLoader : MonoBehaviour
    {
        public void LoadSceneAdditive (int sceneIndex)
        {
            SceneLoader.instance.LoadSceneAdditive(sceneIndex);
        }
        public void LoadSceneSingle (int sceneIndex)
        {
            SceneLoader.instance.LoadSceneSingle(sceneIndex);
        }
        public void QuitGame()
        {
            SceneLoader.instance.QuitGame();
        }
    }
}
