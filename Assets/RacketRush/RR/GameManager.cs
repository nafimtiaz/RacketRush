using RacketRush.RR.Logic;
using RacketRush.RR.Views.Actors;
using RacketRush.RR.Views.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RacketRush.RR
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TargetHandlerView targetHandlerView;
        [SerializeField] private GameStatsView gameStatsView;
        [SerializeField] private BallThrowerView ballThrowerView;
        
        private static GameManager _instance;
        private GameState _currentState;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject("GameManager");
                        _instance = singletonObject.AddComponent<GameManager>();
                    }
                }

                return _instance;
            }
        }
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start()
        {
            PrepareGameState();
            targetHandlerView.Populate(OnHitSuccess);
            ballThrowerView.Populate(OnNewBallThrow);
        }

        #region Update Game State

        private void PrepareGameState()
        {
            _currentState = new GameState();
            _currentState.Name = $"SomePlayer_{Random.Range(1, 10)}";
        }
        
        private void OnNewBallThrow()
        {
            _currentState.IncrementBallThrowCount();
        }

        private void OnHitSuccess()
        {
            _currentState.IncrementScoreAndShotsOnTarget(GameConstants.BASE_SCORE_PER_HIT);
            gameStatsView.IncrementScore(_currentState.Score);
        }

        #endregion
    }
}