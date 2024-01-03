using System.Collections.Generic;
using DG.Tweening;
using RacketRush.RR.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RacketRush.RR.Views.UI
{
    public class SelectionView : MonoBehaviour
    {
        [SerializeField] private Button prevBtn;
        [SerializeField] private Button nextBtn;
        [SerializeField] private TextMeshProUGUI currentValue;

        private List<string> _options;
        private int _selectedOptionIndex;

        public int SelectionIndex
        {
            get => _selectedOptionIndex;
            set => _selectedOptionIndex = value;
        }

        public void Populate(List<string> options)
        {
            if (options != null && options.Count > 0)
            {
                _options = options;
                prevBtn.onClick.AddListener(OnSelectionChangeClicked(true));
                nextBtn.onClick.AddListener(OnSelectionChangeClicked(false));
                _selectedOptionIndex = 0;
                currentValue.text = options[0];   
            }
        }

        // change the selection based on prev and next button
        private UnityAction OnSelectionChangeClicked(bool isPrev)
        {
            bool hasChanged = false;
            
            return () =>
            {
                if (isPrev)
                {
                    if (_selectedOptionIndex > 0)
                    {
                        hasChanged = true;
                        _selectedOptionIndex--;
                    }
                }
                else
                {
                    if (_selectedOptionIndex < _options.Count - 1)
                    {
                        hasChanged = true;
                        _selectedOptionIndex++;
                    }
                }

                if (hasChanged)
                {
                    currentValue.DOFade(0f, 0f);
                    currentValue.text = _options[_selectedOptionIndex];
                    currentValue.DOFade(1f, GameConstants.ELEMENTS_TOGGLE_DURATION);
                }
            };
        }
    }
}