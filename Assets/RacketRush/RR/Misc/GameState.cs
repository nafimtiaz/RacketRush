using System;

namespace RacketRush.RR.Misc
{
    public class GameState
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public GameModeEnum Difficulty { get; set; }
        public float Accuracy { get; set; }

        public GameState()
        {
            Name = String.Empty;
            Score = 0;
            Difficulty = GameModeEnum.Easy;
            Accuracy = 0f;
        }
    }
}
