using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UberPlanetary.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Create Dialogue", order = 1)]
    public class DialogueSO : ScriptableObject
    {
        public Dialogue[] dialogueLines;

    }

    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 10)]
        public string line;

        public string characterName;
        public Sprite characterSpeaking;
    }
}
