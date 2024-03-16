using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RacketRush.RR.Views.UI
{
    public class ScorePopupWindowView : BaseWindowView
    {
        [SerializeField] private Button backToMenuButton;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private TextMeshProUGUI scoreText;

        private HomeWindowView _homeWindowView;

        protected override bool IsValidComponent
        {
            get
            {
                if (backToMenuButton == null ||
                    playAgainButton == null ||
                    scoreText == null)
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        public void Populate(HomeWindowView homeWindowView)
        {
            _homeWindowView = homeWindowView;
            backToMenuButton.onClick.AddListener(_homeWindowView.BackToMenuFromScorePopup);
            playAgainButton.onClick.AddListener(_homeWindowView.PlayAgainFromScorePopup);
        }

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}
