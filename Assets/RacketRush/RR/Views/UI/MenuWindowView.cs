using System;
using UnityEngine;
using UnityEngine.UI;

namespace RacketRush.RR.Views.UI
{
    public class MenuWindowView : BaseWindowView
    {
        [SerializeField] private Button startBtn;
        [SerializeField] private Button leaderboardBtn;
        [SerializeField] private Button quitButton;

        public void Populate(Action onStart, Action onLeaderboardBtnClicked)
        {
            startBtn.onClick.AddListener(onStart.Invoke);
            leaderboardBtn.onClick.AddListener(onLeaderboardBtnClicked.Invoke);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
