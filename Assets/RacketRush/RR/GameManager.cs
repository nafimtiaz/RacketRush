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
        [SerializeField] private GameStatsWindowView gameStatsWindowView;
        [SerializeField] private BallThrowerView ballThrowerView;
        [SerializeField] private HomeWindowView homeWindowView;
        [SerializeField] private GameObject racketBody;
        
        [Header("Game Config")]
        [SerializeField] [Tooltip("The base score for each hit without multipliers")]
        private int baseScorePerHit;
        [SerializeField] [Tooltip("Duration of per game session in seconds")] 
        private int gameDuration;
        
        private static GameManager _instance;
        private GameState _currentState;
        private int _timeRemaining;
        private Sequence _timeSequence;
        private bool _isGameRunning;

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
            InitGameState();
            homeWindowView.Populate();
        }

        #region Update Game State

        public static void InitGameState(bool fromExistingProfile = false)
        {
            if (!fromExistingProfile)
            {
                _instance._currentState = new GameState();
                _instance._currentState.Name = $"SomePlayer_{Random.Range(1, 10)}";
            }
            else
            {
                _instance._currentState.ResetProgressionsOnly();
            }
            
            _instance._timeRemaining = _instance.gameDuration;
        }

        public void StartGame()
        {
            racketBody.SetActive(true);
            homeWindowView.ToggleVisibility(false);
            gameStatsWindowView.ToggleVisibility(true);
            gameStatsWindowView.ResetValues();
            targetHandlerView.Populate(OnHitSuccess);
            ballThrowerView.Populate(OnNewBallThrow);
            _isGameRunning = true;
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            _timeSequence = DOTween.Sequence();
            _timeSequence.AppendInterval(1f);
            _timeSequence.AppendCallback(() =>
            {
                _timeRemaining--;
                gameStatsWindowView.UpdateTimer(_timeRemaining.ToMinuteAndSecondsString());

                if (_timeRemaining <= 0)
                {
                    OnGameEnded();
                }
            });
            _timeSequence.SetLoops(-1);
        }

        private void OnNewBallThrow()
        {
            if (_isGameRunning)
            {
                _currentState.IncrementBallThrowCount();
            }
        }

        private void OnHitSuccess()
        {
            if (_isGameRunning)
            {
                _currentState.IncrementScoreAndShotsOnTarget(baseScorePerHit);
                gameStatsWindowView.IncrementScore(_currentState.Score);   
            }
        }

        private void OnGameEnded()
        {
            _timeSequence.Kill();
            _isGameRunning = false;
            racketBody.SetActive(false);
            homeWindowView.ShowScore(_currentState.Score);
            homeWindowView.ToggleVisibility(true);
            ballThrowerView.StopBallThrowSequence();
            targetHandlerView.StopTargetGeneration();
        }

        #endregion

        private void OnDestroy()
        {
            if (_timeSequence != null)
            {
                _timeSequence.Kill();
                _timeSequence = null;
            }
        }
    }
}