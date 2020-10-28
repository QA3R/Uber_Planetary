using UnityEngine;
using UnityEngine.Audio;

namespace UberPlanetary.Dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 10)]
        public string line;

        public AudioClip voiceOver;

        public string characterName;
        public Sprite characterSprite;
    }
}