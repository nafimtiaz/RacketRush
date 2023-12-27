using TMPro;
using UnityEngine;

namespace RacketRush.RR.Views.UI
{
    public class GameStatsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void IncrementScore(int score)
        {
            scoreText.text = score.ToString();
        }
        
        public void ResetScore()
        {
            scoreText.text = "0";
        }

        public void UpdateTimer(string time)
        {
            timeText.text = time;
        }

        public void ResetTimer()
        {
            timeText.text = "00:00";
        }
    }
}
