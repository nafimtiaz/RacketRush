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
    }
}
