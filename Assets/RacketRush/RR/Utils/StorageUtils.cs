using System;
using System.Collections.Generic;
using System.Linq;
using RacketRush.RR.Logic;
using UnityEngine;

namespace RacketRush.RR.Utils
{
    [Serializable]
    public class GameStateListWrapper
    {
        public List<GameState> gameStates;
    }
    
    public static class StorageUtils
    {
        public static List<GameState> GetGameState()
        {
            string data = PlayerPrefs.GetString(GameConstants.GAME_STATS_KEY);
            List<GameState> gameStates = new List<GameState>();

            if (!string.IsNullOrEmpty(data))
            {
                GameStateListWrapper wrapper = JsonUtility.FromJson<GameStateListWrapper>(data);
                
                if (wrapper != null && wrapper.gameStates != null)
                {
                    gameStates = wrapper.gameStates.OrderByDescending(stat => stat.Score).ToList();
                }
            }

            return gameStates;
        }
        
        public static void SaveGameState(GameState gameState)
        {
            string data = PlayerPrefs.GetString(GameConstants.GAME_STATS_KEY);
            List<GameState> gameStates;

            if (!string.IsNullOrEmpty(data))
            {
                GameStateListWrapper wrapper = JsonUtility.FromJson<GameStateListWrapper>(data);
                gameStates = wrapper != null && wrapper.gameStates != null ? wrapper.gameStates : new List<GameState>();
            }
            else
            {
                gameStates = new List<GameState>();
            }

            gameStates.Add(gameState);

            GameStateListWrapper newWrapper = new GameStateListWrapper { gameStates = gameStates };
            string newData = JsonUtility.ToJson(newWrapper);
            PlayerPrefs.SetString(GameConstants.GAME_STATS_KEY, newData);
        }
    }
}