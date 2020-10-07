using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quest", order = 2)]
    public class QuestSO : ScriptableObject
    {
        [SerializeField] private string questName;
        [SerializeField] private string questDiscretion;
        [SerializeField] private int questReward;
        
        [Tooltip("Optional, leave empty and the quest manager will find a random location for it")]
        [SerializeField] private GameObject questEndLocationHolder, questStartLocationHolder;

        private ILandmark _questStartLandmark;
        private ILandmark _questEndLandmark;

        public int QuestReward => questReward;

        public ILandmark QuestEndLandmark
        {
            get
            {
                if (_questEndLandmark == null)
                {
                    _questEndLandmark = questEndLocationHolder.GetComponent<ILandmark>();
                }
                return _questEndLandmark;
            }
            set => _questEndLandmark = value;
        }
        public ILandmark QuestStartLandmark
        {
            get
            {
                if (_questStartLandmark == null)
                {
                    _questStartLandmark = questStartLocationHolder.GetComponent<ILandmark>();
                }
                return _questStartLandmark;
            }
            set => _questStartLandmark = value;
        }
    }
}
