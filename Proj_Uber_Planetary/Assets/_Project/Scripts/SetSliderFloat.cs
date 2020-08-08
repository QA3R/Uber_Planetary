using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary
{
    public class SetSliderFloat : MonoBehaviour
    {
        public UnityEvent<float> onValueChange;

        private PlayerController _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            onValueChange?.Invoke(_player.ShipSpeed);
        }
    }
}
