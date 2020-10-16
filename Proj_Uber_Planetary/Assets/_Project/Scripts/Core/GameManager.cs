using UnityEngine;

namespace UberPlanetary.Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager instance { get { return _instance; } }
    
        void Awake ()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                //Debug.Log("Instance was found");
            }
            else
            {
                _instance = this;
                //Debug.Log("Instance was not found");
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}