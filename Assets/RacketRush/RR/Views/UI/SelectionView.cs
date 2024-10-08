using System;
using System.Collections.Generic;
using DG.Tweening;
using RacketRush.RR.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RacketRush.RR.Views.UI
{
    public class SelectionView : BaseView
    {
        [SerializeField] private Button prevBtn;
        [SerializeField] private Button nextBtn;
        [SerializeField] private TextMeshProUGUI currentValue;

        protected override bool IsValidComponent
        {
            get
            {
                if (prevBtn == null ||
                    nextBtn == null ||
                    currentValue == null)
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        private List<string> _options;
        private int _selectedOptionIndex;
        private Action _onChange;

        public int SelectionIndex
        {
            get => _selectedOptionIndex;
            set => _selectedOptionIndex = value;
        }

        public string CurrentSelection => _options[_selectedOptionIndex];

        public void Populate(List<string> options, Action onChange)
        {
            _onChange = onChange;
            
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
                    _onChange.Invoke();
                    currentValue.DOFade(0f, 0f);
                    currentValue.text = _options[_selectedOptionIndex];
                    currentValue.DOFade(1f, GameConstants.ELEMENTS_TOGGLE_DURATION);
                    
                }
            };
        }
    }
}