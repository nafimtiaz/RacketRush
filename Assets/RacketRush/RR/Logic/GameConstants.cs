using UnityEngine;

namespace RacketRush.RR.Logic
{
    public class GameConstants : MonoBehaviour
    {
        public static readonly string LAYER_RACKET = "Racket";
        public static readonly string RACKET_DYN_RB_NAME = "RacketDynamicRigidbody";
        
        public static readonly int[] TRIANGLE_CCW = { 0, 1, 2 };
        public static readonly int[] TRIANGLE_CW = { 0, 2, 1 };
        
        public static readonly float VIEW_TOGGLE_DURATION = 0.5f;
        public static readonly float ELEMENTS_TOGGLE_DURATION = 0.25f;
        
        public static readonly string[] COUNTDOWN_MSG = { "3","2","1","GO!" };
        public static readonly float COUNTDOWN_INTERVAL_DUR = 1f;
        public static readonly Vector3 COUNTDOWN_SCALING = new Vector3(1.5f, 1.5f,1.5f);

        public static readonly int KEYBOARD_CHAR_LETTER_INIT = 65;
        public static readonly int KEYBOARD_CHAR_LETTER_LAST = 90;
        public static readonly int KEYBOARD_CHAR_NUMBER_INIT = 48;
        public static readonly int KEYBOARD_CHAR_NUMBER_LAST = 57;
        
        public static readonly string GAME_STATS_KEY = "GameStats";
    }
}
