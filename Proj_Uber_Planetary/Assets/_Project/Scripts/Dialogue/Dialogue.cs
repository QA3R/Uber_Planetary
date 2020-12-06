using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace UberPlanetary.Dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 10)]
        public string line;

        public AudioClip voiceOver;
        public AudioClip textAudio;
        //public ulong playBuffer;

        public string characterName;
        public Sprite characterSprite;
    }
}