using DG.Tweening;
using RacketRush.RR.Logic;
using UnityEngine;

namespace RacketRush.RR.Views.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseWindowView : BaseView
    {
        private CanvasGroup _canvasGroup;

        public CanvasGroup WindowCanvasGroup => _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void ToggleVisibility(bool on)
        {
            _canvasGroup.blocksRaycasts = on;
            _canvasGroup.DOFade(on ? 1f : 0f, GameConstants.VIEW_TOGGLE_DURATION);
        }
        
        public virtual void ToggleInteractivity(bool on)
        {
            _canvasGroup.blocksRaycasts = on;
        }
    }
}