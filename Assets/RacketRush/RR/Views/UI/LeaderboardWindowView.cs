using System.Collections.Generic;
using RacketRush.RR.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace RacketRush.RR.Views.UI
{
    public class LeaderboardWindowView : BaseWindowView
    {
        [SerializeField] private MenuWindowView menuWindowView;
        [SerializeField] private GameObject leaderboardEntryPrefab;
        [SerializeField] private Transform leaderboardEntriesParent;
        [SerializeField] private Button backButton;

        private HomeWindowView _homeWindowView;
        private List<LeaderboardEntryView> _currentLeaderboardEntries;
            
        protected override bool IsValidComponent
        {
            get
            {
                if (menuWindowView == null ||
                    leaderboardEntryPrefab == null ||
                    leaderboardEntriesParent == null ||
                    backButton == null)
                {
                    return false;
                }
                
                return base.IsValidComponent;
            }
        }

        public void Populate(HomeWindowView homeWindowView)
        {
            _homeWindowView = homeWindowView;
            _currentLeaderboardEntries = new List<LeaderboardEntryView>();
            backButton.onClick.AddListener(_homeWindowView.OnLeaderboardExitButtonClicked);
        }

        public void ClearAllEntries()
        {
            if (_currentLeaderboardEntries == null ||
                _currentLeaderboardEntries.Count == 0)
            {
                return;
            }
            
            foreach (var entry in _currentLeaderboardEntries)
            {
                Destroy(entry.gameObject);
            }
            
            _currentLeaderboardEntries.Clear();
            _currentLeaderboardEntries.TrimExcess();
        }

        public void CreateLeaderboardTable()
        {
            var entries = StorageUtils.GetGameState();
            ClearAllEntries();

            for (int i = 0; i < entries.Count; i++)
            {
                GameObject leaderboardEntryViewObject = Instantiate(leaderboardEntryPrefab, leaderboardEntriesParent);
                LeaderboardEntryView leaderboardEntryView = leaderboardEntryViewObject.GetComponent<LeaderboardEntryView>();
                _currentLeaderboardEntries.Add(leaderboardEntryView);
                leaderboardEntryView.Populate(entries[i], i + 1);
            }
        }
    }
}
