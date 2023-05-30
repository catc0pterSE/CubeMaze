using Infrastructure.GameStateMachine;
using Infrastructure.GameStateMachine.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class GenerateFixedButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _size;
        [SerializeField] private TMP_Text _text;

        public void Initialize(GameStateMachine gameStateMachine) =>
            _button.onClick.AddListener(() => gameStateMachine.Enter<LoadLevelState, int>(_size));

        private void Awake() =>
            _text.text = $"{_size} x {_size}";

        private void OnDestroy() =>
            _button.onClick.RemoveAllListeners();
    }
}