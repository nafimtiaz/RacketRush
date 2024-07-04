using RacketRush.RR.Logic;
using TMPro;
using UnityEngine;

namespace RacketRush.RR.Views.UI
{
    public class LeaderboardEntryView : BaseWindowView
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI accuracyText;

        private HomeWindowView _homeWindowView;

        protected override bool IsValidComponent
        {
            get
            {
                if (nameText == null ||
                    scoreText == null ||
                    accuracyText == null)
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        public void Populate(GameState state)
        {
            nameText.text = state.Name;
            scoreText.text = state.Score.ToString();
            accuracyText.text = state.Accuracy.ToString("F1");
        }
    }
}
