using System;
using RacketRush.RR.Controllers;
using UnityEngine;

namespace RacketRush.RR
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        
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

        [SerializeField] private TargetController targetController;

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
            targetController.GenerateAndCacheTargets();
            targetController.PlayNextTarget();
        }
    }
}