using System;
using System.Linq;
using RacketRush.RR.Logic;
using TMPro;
using UnityEngine;

namespace RacketRush.RR.Views.UI
{
    public class GameConfigWindowView : BaseWindowView
    {
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private SelectionView difficultySelectionView;
        [SerializeField] private SelectionView musicSelectionView;
        [SerializeField] private SelectionView backgroundSelectionView;
        
        [SerializeField] private AudioClip[] bgMusicClips;
        [SerializeField] private GameObject[] bgEnvironments;
        
        public void Populate()
        {
            difficultySelectionView.Populate(Enum.GetNames(typeof(GameModeEnum)).ToList());
            musicSelectionView.Populate(bgMusicClips.Select(clip => clip.name).ToList());
            backgroundSelectionView.Populate(bgEnvironments.Select(env => env.gameObject.name).ToList());
        }
    }
}
