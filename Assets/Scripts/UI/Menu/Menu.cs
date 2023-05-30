using Infrastructure.GameStateMachine;
using Infrastructure.GameStateMachine.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GenerateFixedButton[] _generateFixedButtons;
        [SerializeField] private GenerateInputButton _generateInputButton;
        [SerializeField] private Button _exitButton;

        public void Initialize(GameStateMachine gameStateMachine)
        {
            InitializeButtons(gameStateMachine);
        }

        private void OnDestroy() =>
            _exitButton.onClick.RemoveAllListeners();


        private void InitializeButtons(GameStateMachine gameStateMachine)
        {
            foreach (var button in _generateFixedButtons)
                button.Initialize(gameStateMachine);

            _generateInputButton.Initialize(gameStateMachine);
            _exitButton.onClick.AddListener(gameStateMachine.Enter<CloseGameState>);
        }
    }
}