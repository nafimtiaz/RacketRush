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
    }
}
