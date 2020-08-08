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

        /// <summary>
        /// Expects a value between 0 to 1 and sets volume's weight
        /// </summary>
        /// <param name="amount"></param>
        public void SetVolume(float amount)
        {
            _volume.weight = amount;
        }
    }
}
