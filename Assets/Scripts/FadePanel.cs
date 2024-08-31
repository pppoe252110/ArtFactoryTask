using DG.Tweening;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    [SerializeField] private FillSlider _fillSlider;
    [SerializeField] private CanvasGroup _fadePanel;
    [SerializeField] private EndGameScreen _endGameScreen;
    private bool _gameEnded;
    void Update()
    {
        if (_fillSlider.Value >= 1 && !_gameEnded)
        {
            _gameEnded = true;

            _fillSlider.DoAnimation();

            _fadePanel.gameObject.SetActive(true);
            _fadePanel.DOFade(1f, 1f).SetEase(Ease.OutCirc).OnComplete(() =>_endGameScreen.Show()).Play();
        }
    }
}
