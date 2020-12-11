using System;
using Febucci.UI;
using UnityEngine;

namespace UberPlanetary
{
    public class PlaySubtitleText : MonoBehaviour
    {
        public TextAnimatorPlayer textAnimator;

        [TextArea]
        public string textToPlay;
        
        
        private void Awake()
        {
            textAnimator = GetComponent<TextAnimatorPlayer>();
            PlayText();
        }

        public void PlayText()
        {
            textAnimator.ShowText(textToPlay);
        }
    }
}
