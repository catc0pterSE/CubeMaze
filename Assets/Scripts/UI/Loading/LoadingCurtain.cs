using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Utility.Extensions;

namespace UI.Loading
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _fadingStep = 0.01f;

        private float _defaultCurtainAlpha;
        private Coroutine _fadeJob;

        private void Awake()
        {
            _defaultCurtainAlpha = _curtain.alpha;
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            this.EnableObject();
            _curtain.alpha = _defaultCurtainAlpha;
        }

        public void FadeInFadeOut(Action toDoWhenFaded = null, bool withText = false)
        {
            toDoWhenFaded += FadeOut;
            FadeIn(toDoWhenFaded, withText);
        }

        public void FadeIn(Action toDoAfter = null, bool withText = false)
        {
            this.EnableObject();

            if (withText == false)
                _text.DisableObject();
            else
                _text.EnableObject();

            if (_fadeJob != null)
                StopCoroutine(_fadeJob);

            _fadeJob = StartCoroutine(Fade(_defaultCurtainAlpha, toDoAfter));
        }

        public void FadeOut()
        {
            if (_fadeJob != null)
                StopCoroutine(_fadeJob);

            _fadeJob = StartCoroutine(Fade(0, this.DisableObject));
        }

        private IEnumerator Fade(float targetAlpha, Action toDoWhenFaded = null)
        {
            while (Math.Abs(_curtain.alpha - targetAlpha) > Mathf.Epsilon)
            {
                _curtain.alpha = Mathf.MoveTowards(_curtain.alpha, targetAlpha, _fadingStep);
                yield return null;
            }

            toDoWhenFaded?.Invoke();
        }
    }
}