using System;

namespace RacketRush.RR.Logic
{
    [Serializable]
    public class GameState
    {
        public string Name;
        public int Score;
        public int BallThrowCount;
        public int ShotsOnTarget;
        public float Accuracy => ((float)(ShotsOnTarget) / BallThrowCount) * 100f;

        public GameState()
        {
            Name = String.Empty;
            Score = 0;
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
