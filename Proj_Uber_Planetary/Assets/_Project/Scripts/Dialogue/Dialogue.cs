﻿using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace UberPlanetary.Dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 10)]
        public string line;
        public int lineTime;

        public AudioClip voiceOver;
        //public ulong playBuffer;

        public string characterName;
        public Sprite characterSprite;
    }
}