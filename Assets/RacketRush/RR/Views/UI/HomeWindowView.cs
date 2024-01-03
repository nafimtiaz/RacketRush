using UnityEngine;

namespace RacketRush.RR.Views.UI
{
    public class HomeWindowView : BaseWindowView
    {
        [SerializeField] private MenuWindowView menuWindowView;
        [SerializeField] private GameConfigWindowView gameConfigWindowView;

        public void Populate()
        {
            menuWindowView.Populate(OnStartButtonClicked, OnLeaderboardButtonClicked);
            gameConfigWindowView.Populate();
            menuWindowView.ToggleVisibility(true);
            gameConfigWindowView.ToggleVisibility(false);
        }

        #region Main Menu
        
        private void OnStartButtonClicked()
        {
            menuWindowView.ToggleVisibility(false);
            gameConfigWindowView.ToggleVisibility(true);
        }
        
        private void OnLeaderboardButtonClicked()
        {
            menuWindowView.ToggleVisibility(false);
            gameConfigWindowView.ToggleVisibility(true);
        }

        #endregion
    }
}
