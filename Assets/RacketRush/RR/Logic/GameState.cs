using System;

namespace RacketRush.RR.Logic
{
    public class GameState
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public GameModeEnum Difficulty { get; set; }
        public int BallThrowCount { get; set; }
        public int ShotsOnTarget { get; set; }
        public float Accuracy => ((float)(ShotsOnTarget) / BallThrowCount) * 100f;

        public GameState()
        {
            Name = String.Empty;
            Score = 0;
            Difficulty = GameModeEnum.Easy;
            BallThrowCount = 0;
            ShotsOnTarget = 0;
        }

        public void IncrementScoreAndShotsOnTarget(int scoreAmount)
        {
            Score += scoreAmount;
            ShotsOnTarget++;
        }
        
        public void IncrementBallThrowCount()
        {
            BallThrowCount++;
        }

        public void ResetProgressionsOnly()
        {
            Score = 0;
            BallThrowCount = 0;
            ShotsOnTarget = 0;
        }
    }
}
