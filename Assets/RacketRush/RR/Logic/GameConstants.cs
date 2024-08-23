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
        
        // Each int[] represents the indices of the points of triangular targets
        public static readonly int[][] TRIANGLE_INDEX_ARR = new int[][]
        {
            new [] {0,1,2},
            new [] {0,2,3},
            new [] {0,3,4},
            new [] {0,4,5},
            new [] {0,5,1},
            new [] {5,7,1},
            new [] {1,7,8},
            new [] {9,1,8},
            new [] {2,1,9},
            new [] {10,2,9},
            new [] {11,2,10},
            new [] {11,3,2},
            new [] {12,3,11},
            new [] {12,13,3},
            new [] {13,4,3},
            new [] {13,14,4},
            new [] {4,14,15},
            new [] {4,15,5},
            new [] {5,15,6},
            new [] {5,6,7},
            new [] {6,16,17},
            new [] {6,17,7},
            new [] {7,17,18},
            new [] {7,18,8},
            new [] {8,18,19},
            new [] {9,8,19},
            new [] {9,19,20},
            new [] {10,9,20},
            new [] {10,20,21},
            new [] {21,11,10},
            new [] {22,11,21},
            new [] {22,12,11},
            new [] {22,23,12},
            new [] {12,23,13},
            new [] {23,24,13},
            new [] {24,14,13},
            new [] {24,25,14},
            new [] {14,25,15},
            new [] {15,25,16},
            new [] {15,16,6}
        };
    }
}
