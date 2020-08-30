using UnityEngine;
using UnityEngine.Rendering;

namespace UberPlanetary
{
    public class SetVolumeWeight : MonoBehaviour
    {
        private Volume _volume;
        
        private void Awake()
        {
            _volume = GetComponent<Volume>();
        }

        /// Expects a value between 0 to 1 and sets volume's weight
        public void SetWeight(float amount)
        {
            _volume.weight = amount;
        }
    }
}
