using System;

namespace UberPlanetary.Core
{
    /// Exposes events for the button states
    public class ButtonEvent
    {
        private event Action onDown;
        private event Action onHeld;
        private event Action onUp;

        public Action OnDown
        {
            get => onDown;
            set => onDown = value;
        }

        public Action OnHeld
        {
            get => onHeld;
            set => onHeld = value;
        }

        public Action OnUp
        {
            get => onUp;
            set => onUp = value;
        }
    }
}