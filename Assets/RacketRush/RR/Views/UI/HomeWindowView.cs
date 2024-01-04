using UnityEngine;

namespace RacketRush.RR.Views.UI
{
    public class HomeWindowView : BaseWindowView
    {
        [SerializeField] private MenuWindowView menuWindowView;
        [SerializeField] private GameConfigWindowView gameConfigWindowView;

        public void Populate()
        {
            menuWindowView.Populate(this);
            gameConfigWindowView.Populate(this);
            menuWindowView.ToggleVisibility(true);
            gameConfigWindowView.ToggleVisibility(false);
        }

        #region Main Menu
        
        public void OnPlayButtonClicked()
        {
            menuWindowView.ToggleVisibility(false);
            gameConfigWindowView.ToggleVisibility(true);
        }
        
        public void OnLeaderboardButtonClicked()
        {
            menuWindowView.ToggleVisibility(false);
            gameConfigWindowView.ToggleVisibility(true);
        }
        
        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        #endregion
        
        #region Game Config Window
        
        public void OnStartButtonClicked()
        {
            //TODO: Start main gameplay
        }
        
        public void OnBackButtonClicked()
        {
            gameConfigWindowView.ToggleVisibility(false);
            menuWindowView.ToggleVisibility(true);
        }

        #endregion
    }
}
