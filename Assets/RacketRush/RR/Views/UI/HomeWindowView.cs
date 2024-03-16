using UnityEngine;

namespace RacketRush.RR.Views.UI
{
    public class HomeWindowView : BaseWindowView
    {
        [SerializeField] private MenuWindowView menuWindowView;
        [SerializeField] private GameConfigWindowView gameConfigWindowView;
        [SerializeField] private CountdownWindowView countdownWindowView;
        [SerializeField] private ScorePopupWindowView scorePopupWindowView;
        [SerializeField] private GameObject uiPointer;

        protected override bool IsValidComponent
        {
            get
            {
                if (menuWindowView == null ||
                    gameConfigWindowView == null ||
                    countdownWindowView == null ||
                    scorePopupWindowView == null ||
                    uiPointer == null)
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        public void Populate()
        {
            menuWindowView.Populate(this);
            gameConfigWindowView.Populate(this);
            menuWindowView.ToggleVisibility(true);
            gameConfigWindowView.ToggleVisibility(false);
            scorePopupWindowView.ToggleVisibility(false);
            scorePopupWindowView.Populate(this);
        }

        public override void ToggleVisibility(bool on)
        {
            base.ToggleVisibility(on);
            uiPointer.SetActive(on);
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
            gameConfigWindowView.ToggleVisibility(false);
            StartMainGameSequence();
        }
        
        public void OnBackButtonClicked()
        {
            gameConfigWindowView.ToggleVisibility(false);
            menuWindowView.ToggleVisibility(true);
        }

        #endregion

        #region Countdown Window

        private void StartMainGameSequence()
        {
            countdownWindowView.ToggleVisibility(true);
            countdownWindowView.StartCountdownSequence(() =>
            {
                countdownWindowView.ToggleVisibility(false);
                GameManager.Instance.StartGame();
            });
        }

        #endregion
        
        #region Score Popup

        public void ShowScore(int score)
        {
            scorePopupWindowView.ToggleVisibility(true);
            scorePopupWindowView.SetScore(score);
        }

        public void PlayAgainFromScorePopup()
        {
            scorePopupWindowView.ToggleVisibility(false);
            GameManager.InitGameState(true);
            StartMainGameSequence();
        }
        
        public void BackToMenuFromScorePopup()
        {
            scorePopupWindowView.ToggleVisibility(false);
            menuWindowView.ToggleVisibility(true);
            GameManager.InitGameState();
        }

        #endregion
    }
}
