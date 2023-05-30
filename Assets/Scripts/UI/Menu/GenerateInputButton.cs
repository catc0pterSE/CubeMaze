using Infrastructure.GameStateMachine;
using Infrastructure.GameStateMachine.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class GenerateInputButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_InputField _inputField;
        private GameStateMachine _gameStateMachine;

        public void Initialize(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _button.onClick.AddListener(LoadLevel);
        }

        private void LoadLevel()
        {
            int size = GetInputValue();
            
            if (size != default)
                _gameStateMachine.Enter<LoadLevelState, int>(size);
        }

        private int GetInputValue()
        {
            if (int.TryParse(_inputField.text, out int integer) && integer > 0)
                return integer;
            
            return default;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}