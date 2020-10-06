using UberPlanetary.Navigation;
using UberPlanetary.Player.Movement;
using UberPlanetary.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace UberPlanetary.Quests
{
    public class QuestManager : MonoBehaviour
    {
        private NavigationManager _navigationManager;
        private PlayerController playerController;
        private QuestSO _currentQuest;
        private bool IsQuestActive => _currentQuest != null;

        [SerializeField] private UnityEvent<QuestSO> onQuestAccepted;
        [SerializeField] private float searchRadius;


        private void Awake()
        {
            onQuestAccepted.AddListener(QuestAccepted);
        }

        private void Start()
        {
            _navigationManager = FindObjectOfType<NavigationManager>();
            playerController = FindObjectOfType<PlayerController>();
        }

        public void AcceptQuest(QuestSO questSo)
        {
            if(IsQuestActive) return;

            _currentQuest = questSo;
            onQuestAccepted?.Invoke(_currentQuest);
        }

        private void QuestAccepted(QuestSO questSo)
        {
            if (questSo.QuestStartLandmark == null)
            {
                questSo.QuestStartLandmark = _navigationManager.GetRandomLandmarkWithinRadius(playerController.transform.position, searchRadius);
            }

            if (questSo.QuestStartLandmark == null)
            {
                questSo.QuestStartLandmark = _navigationManager.GetRandomLandmark();
            }

            if (questSo.QuestStartLandmark == null)
            {
                Debug.LogError("No quest landmark could be found.");
                return;
            }
            questSo.QuestStartLandmark.OnReached.AddListener(StartLocationReached);
        }

        private void StartLocationReached()
        {
            _currentQuest.QuestStartLandmark.OnReached.RemoveListener(StartLocationReached);

            if (_currentQuest.QuestEndLandmark == null)
            {
                _currentQuest.QuestEndLandmark = _navigationManager.GetFurthestLandmark(playerController.transform.position);
            }
            _currentQuest.QuestEndLandmark.OnReached.AddListener(EndLocationReached);
        }

        private void EndLocationReached()
        {
            _currentQuest.QuestEndLandmark.OnReached.RemoveListener(EndLocationReached);

        }
    }
}
