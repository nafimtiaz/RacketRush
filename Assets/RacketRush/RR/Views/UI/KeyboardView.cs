using RacketRush.RR.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RacketRush.RR.Views.UI
{
    public class KeyboardView : BaseWindowView
    {
        [SerializeField] private GameObject charButtonPrefab;
        [SerializeField] private Transform buttonGrid;
        [SerializeField] private Button clearButton;
        [SerializeField] private Button okButton;

        private GameConfigWindowView _configWindowView;
        private TMP_InputField _targetText;

        protected override bool IsValidComponent
        {
            get
            {
                if (charButtonPrefab == null ||
                    buttonGrid == null ||
                    clearButton == null ||
                    okButton == null)
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        public void Populate(GameConfigWindowView configWindowView, TMP_InputField targetText)
        {
            _configWindowView = configWindowView;
            _targetText = targetText;
            PopulateKeys();
            WindowCanvasGroup.blocksRaycasts = false;
        }

        private void PopulateKeys()
        {
            PopulateKeysByRange(GameConstants.KEYBOARD_CHAR_LETTER_INIT, GameConstants.KEYBOARD_CHAR_LETTER_LAST);
            PopulateKeysByRange(GameConstants.KEYBOARD_CHAR_NUMBER_INIT, GameConstants.KEYBOARD_CHAR_NUMBER_LAST);
        }

        private void PopulateKeysByRange(int initAsciiIndex, int lastAsciiIndex)
        {
            for (int i = initAsciiIndex; i <= lastAsciiIndex; i++)
            {
                GameObject button = Instantiate(charButtonPrefab, buttonGrid);
                string s = ((char)i).ToString();
                button.name = s;
                button.GetComponentInChildren<TextMeshProUGUI>().text = s;
                button.GetComponent<Button>().onClick.AddListener(() => OnKeyPressed(s));
            }
        }

        private void OnKeyPressed(string s)
        {
            _targetText.text += s;
        }
    }
}
