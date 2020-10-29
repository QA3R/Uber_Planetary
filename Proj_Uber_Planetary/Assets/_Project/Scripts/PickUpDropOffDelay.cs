using System;
using System.Collections;
using UberPlanetary.Player.Movement;
using UnityEngine;
using DG.Tweening;
using UberPlanetary.Core.ExtensionMethods;
using UberPlanetary.Core.Interfaces;
using UberPlanetary.General;

namespace UberPlanetary
{
    public class PickUpDropOffDelay : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private AudioSource _audioSource; 
        private PlayerController _player; 
        
        [SerializeField] private GameObject transitionImage;
        [SerializeField] private float endScale, startScale, scaleDuration;
        [SerializeField] private Ease scaleEase;
        [SerializeField] private float playerLerpDuration;
        [SerializeField] private AnimationCurve lerpAnimationCurve;

        public event Action ONParkingCompleted;
        private void Start()
        {
            _inputHandler = FindObjectOfType<InputHandler>();
            _audioSource = GetComponent<AudioSource>();
            _player = FindObjectOfType<PlayerController>();
            EndCondition.onGameOver += EndStuff;
        }

        public void PlayCutscene(ILandmark landmark)
        {
            StartCoroutine(StartCutscene(landmark.GetTransform.position));
        }

        public void EndStuff()
        {
            transitionImage.transform.localScale = Vector3.zero;
            EndCondition.onGameOver -= EndStuff;
        }
        
        public IEnumerator StartCutscene(Vector3 posi)
        {
            //take away input
            _inputHandler.enabled = false;

            _player.MovementAxisModifier = 0;
            _player.RotationAxisModifier = 0;

            float t = 0;
            Vector3 startPosi = _player.transform.position;
            while (t <= playerLerpDuration)
            {
                t += Time.deltaTime;
                _player.transform.position = Vector3.Lerp(startPosi, posi, lerpAnimationCurve.Evaluate(t.Remap(0, playerLerpDuration, 0, 1)));
                yield return new WaitForEndOfFrame();
            }
            
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
            _inputHandler.enabled = true;
            
            _player.MovementAxisModifier = 1;
            _player.RotationAxisModifier = 1;
            ONParkingCompleted?.Invoke();
        }
    }
}
