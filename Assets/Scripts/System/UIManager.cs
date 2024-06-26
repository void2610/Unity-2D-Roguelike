namespace NManager
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using System.Collections.Generic;
    using TMPro;
    using NUI;

    public class UIManager : MonoBehaviour
    {
        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        [SerializeField]
        private List<CanvasGroup> canvasGroups;

        private void ChangeCanvasGroupEnabled(CanvasGroup c, bool enabled)
        {
            c.alpha = enabled ? 1 : 0;
            c.blocksRaycasts = enabled;
            c.interactable = enabled;
        }

        public void ChangeUIState(GameManager.GameState state)
        {
            foreach (var canvasGroup in canvasGroups)
            {
                ChangeCanvasGroupEnabled(canvasGroup, false);
            }

            switch (state)
            {
                case GameManager.GameState.Playing:
                    ChangeCanvasGroupEnabled(canvasGroups[0], true);
                    break;
                case GameManager.GameState.Paused:
                    break;
                case GameManager.GameState.GameOver:
                    ChangeCanvasGroupEnabled(canvasGroups[1], true);
                    break;
                case GameManager.GameState.Other:
                    break;
            }
        }

        private void Start()
        {
            ChangeUIState(GameManager.GameState.Playing);
        }
    }
}