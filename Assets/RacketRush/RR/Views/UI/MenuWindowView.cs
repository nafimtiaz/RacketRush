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

        private HomeWindowView _homeWindowView;

        protected override bool IsValidComponent
        {
            get
            {
                if (startBtn == null ||
                    leaderboardBtn == null ||
                    quitButton == null)
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        public void Populate(HomeWindowView homeWindowView)
        {
            _homeWindowView = homeWindowView;
            startBtn.onClick.AddListener(homeWindowView.OnPlayButtonClicked);
            leaderboardBtn.onClick.AddListener(homeWindowView.OnLeaderboardButtonClicked);
            quitButton.onClick.AddListener(homeWindowView.OnQuitButtonClicked);
            WindowCanvasGroup.blocksRaycasts = true;
        }
    }
}
