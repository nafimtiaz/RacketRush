using System;
using DG.Tweening;
using RacketRush.RR.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RacketRush.RR.Views.UI
{
    public class CountdownWindowView : BaseWindowView
    {
        [SerializeField] private TextMeshProUGUI solidText;
        [SerializeField] private TextMeshProUGUI linedText;

        private HomeWindowView _homeWindowView;
        private Sequence _countdownSequence;

        protected override bool IsValidComponent
        {
            get
            {
                if (solidText == null ||
                    linedText == null)
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        public void Populate(HomeWindowView homeWindowView)
        {
            _homeWindowView = homeWindowView;
        }

        public void StartCountdownSequence(Action onComplete)
        {
            int msgIndex = 0;
            linedText.transform.DOScale(Vector3.one, 0f);
            linedText.DOFade(1f, 0f);

            _countdownSequence = DOTween.Sequence();
            foreach (var msg in GameConstants.COUNTDOWN_MSG)
            {
                _countdownSequence.AppendCallback(() =>
                {
                    solidText.text = msg;
                    linedText.text = msg;
                    linedText.DOFade(1f, 0f);
                    linedText.transform.DOScale(Vector3.one, 0f);
                    linedText.transform.DOScale(GameConstants.COUNTDOWN_SCALING, GameConstants.COUNTDOWN_INTERVAL_DUR);
                    linedText.DOFade(0f, GameConstants.COUNTDOWN_INTERVAL_DUR);
                });
                _countdownSequence.AppendInterval(GameConstants.COUNTDOWN_INTERVAL_DUR);
                _countdownSequence.AppendCallback(() =>
                {
                    if (++msgIndex == GameConstants.COUNTDOWN_MSG.Length)
                    {
                        onComplete.Invoke();
                    }
                });
            }
        }

        private void OnDestroy()
        {
            if (_countdownSequence != null)
            {
                _countdownSequence.Kill();
                _countdownSequence = null;
            }
        }
    }
}
