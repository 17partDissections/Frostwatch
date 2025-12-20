using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Q17pD.Frostwatch
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private int _sceneID;
        [SerializeField] private float _delay;
        public void LoadScene() { StartCoroutine(LoadSceneCoroutine()); }
        private IEnumerator LoadSceneCoroutine()
        {
            yield return new WaitForSeconds(_delay);
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneID);
        }
    }
}
