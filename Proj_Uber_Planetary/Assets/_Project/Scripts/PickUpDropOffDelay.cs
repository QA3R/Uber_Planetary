using System.Collections;
using UberPlanetary.Player.Movement;
using UnityEngine;
using DG.Tweening;
using UberPlanetary.General;

namespace UberPlanetary
{
    public class PickUpDropOffDelay : MonoBehaviour
    {
        private InputHandler _InputHandler;
        private AudioSource _audioSource; 
        
        [SerializeField]private GameObject transitionImage;
        [SerializeField] private float endScale, startScale, scaleDuration;
        [SerializeField] private Ease scaleEase;

        private void Start()
        {
            _InputHandler = FindObjectOfType<InputHandler>();
            _audioSource = GetComponent<AudioSource>();
            EndCondition.onGameOver += EndStuff;
        }

        public void PlayCutscene()
        {
            StartCoroutine(StartCutscene());
        }

        public void EndStuff()
        {
            transitionImage.transform.localScale = Vector3.zero;
            EndCondition.onGameOver -= EndStuff;
        }
        
        public IEnumerator StartCutscene()
        {
            //take away input
            _InputHandler.enabled = false;
            //fade to black
            transitionImage.transform.DOScale(endScale, scaleDuration).SetEase(scaleEase);

            yield return new WaitForSeconds(scaleDuration);
            
            //play sound
            _audioSource.Play();
            yield return new WaitForSeconds(_audioSource.clip.length);
            
            //fade back
            transitionImage.transform.DOScale(startScale, scaleDuration).SetEase(scaleEase);

            yield return new WaitForSeconds(scaleDuration);

            //give back input
            _InputHandler.enabled = true;
        }
    }
}
