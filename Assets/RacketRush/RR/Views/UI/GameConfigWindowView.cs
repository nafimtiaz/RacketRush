using System;
using System.Linq;
using RacketRush.RR.Logic;
using RacketRush.RR.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RacketRush.RR.Views.UI
{
    public class GameConfigWindowView : BaseWindowView
    {
        [SerializeField] private MenuWindowView menuWindowView;
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private SelectionView difficultySelectionView;
        [SerializeField] private SelectionView musicSelectionView;
        [SerializeField] private SelectionView backgroundSelectionView;
        [SerializeField] private Button backButton;
        [SerializeField] private Button startButton;
        [SerializeField] private AudioClip[] bgMusicClips;
        [SerializeField] private GameObject[] bgEnvironments;
        [SerializeField] private AudioSource bgAudioSource;
        [SerializeField] private KeyboardView keyboardView;

        private HomeWindowView _homeWindowView;

        protected override bool IsValidComponent
        {
            get
            {
                if (nameField == null ||
                    difficultySelectionView == null ||
                    musicSelectionView == null ||
                    backgroundSelectionView == null ||
                    backButton == null ||
                    startButton == null ||
                    bgMusicClips.HasValidLength(3) ||
                    bgEnvironments.HasValidLength(3) ||
                    bgAudioSource == null ||
                    keyboardView == null)
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        public void Populate(HomeWindowView homeWindowView)
        {
            _homeWindowView = homeWindowView;
            difficultySelectionView.Populate(Enum.GetNames(typeof(GameModeEnum)).ToList(), OnDifficultyChanged);
            musicSelectionView.Populate(bgMusicClips.Select(clip => clip.name).ToList(), OnMusicChanged);
            backgroundSelectionView.Populate(bgEnvironments.Select(env => env.gameObject.name).ToList(), OnBackgroundChanged);
            WindowCanvasGroup.blocksRaycasts = false;
            startButton.onClick.AddListener(homeWindowView.OnStartButtonClicked);
            backButton.onClick.AddListener(homeWindowView.OnBackButtonClicked);
            keyboardView.Populate(this, nameField);
        }

        #region Callbacks

        private void OnDifficultyChanged()
        {
            
        }
        
        private void OnMusicChanged()
        {
            int musicIndex = musicSelectionView.CurrentSelection == MusicModeEnum.On.ToString() ? 0 : 1;
            bgAudioSource.clip = bgMusicClips[musicIndex];
            bgAudioSource.Play();
        }
        
        private void OnBackgroundChanged()
        {
            for (int i = 0; i < bgEnvironments.Length; i++)
            {
                bgEnvironments[i].SetActive(i == backgroundSelectionView.SelectionIndex);
            }
        }

        #endregion
    }
}
