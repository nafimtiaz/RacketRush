using DG.Tweening;
using RacketRush.RR.Logic;
using RacketRush.RR.Utils;
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
        
        [Header("Game Config")]
        [SerializeField] [Tooltip("The base score for each hit without multipliers")]
        private int baseScorePerHit;
        [SerializeField] [Tooltip("Duration of per game session in seconds")] 
        private int gameDuration;
        
        private static GameManager _instance;
        private GameState _currentState;
        private int _timeRemaining;
        private Sequence _timeSequence;

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
            UpdateTimer();
            targetHandlerView.Populate(OnHitSuccess);
            ballThrowerView.Populate(OnNewBallThrow);
        }

        #region Update Game State

        private void PrepareGameState()
        {
            _currentState = new GameState();
            _currentState.Name = $"SomePlayer_{Random.Range(1, 10)}";
            _timeRemaining = gameDuration;
        }

        private void UpdateTimer()
        {
            _timeSequence = DOTween.Sequence();
            _timeSequence.AppendInterval(1f);
            _timeSequence.AppendCallback(() =>
            {
                _timeRemaining--;
                gameStatsView.UpdateTimer(_timeRemaining.ToMinuteAndSecondsString());

                if (_timeRemaining == 0)
                {
                    _timeSequence.Kill();
                }
            });
            _timeSequence.SetLoops(-1);
        }

        private void OnNewBallThrow()
        {
            _currentState.IncrementBallThrowCount();
        }

        private void OnHitSuccess()
        {
            _currentState.IncrementScoreAndShotsOnTarget(baseScorePerHit);
            gameStatsView.IncrementScore(_currentState.Score);
        }

        #endregion

        private void OnDestroy()
        {
            if (_timeSequence != null)
            {
                _timeSequence.Kill();
            }
        }
    }
}