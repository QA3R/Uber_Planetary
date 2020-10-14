using UnityEngine;

namespace UberPlanetary.Dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 10)]
        public string line;

        public string characterName;
        public Sprite characterSpeaking;
    }
}