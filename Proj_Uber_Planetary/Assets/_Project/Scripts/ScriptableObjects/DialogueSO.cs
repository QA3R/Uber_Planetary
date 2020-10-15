using UnityEngine;

namespace UberPlanetary.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Create Dialogue", order = 1)]
    public class DialogueSO : ScriptableObject
    {
        public Dialogue.Dialogue[] dialogueLines;
    }
}
