using TMPro;
using UnityEngine;

namespace RacketRush.RR.Views.UI
{
    public class GameStatsWindowView : BaseWindowView
    {
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void IncrementScore(int score)
        {
            scoreText.text = score.ToString();
        }
        
        public void ResetValues()
        {
            scoreText.text = "0";
            timeText.text = "00:00";
        }

        public void UpdateTimer(string time)
        {
            timeText.text = time;
        }
    }
}
