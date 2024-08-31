using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private Image[] _stars;
    [SerializeField] private GameObject _screen;
    public void Show()
    {
        _screen.SetActive(true);
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].transform.localScale = Vector3.zero;
            var sequence = DOTween.Sequence();
            var step = i;
            sequence.AppendInterval((1+step));

            sequence.Append(_stars[i].transform.DOScale(1, 1f)).SetEase(Ease.OutBounce);
            sequence.Play();
        }
        _screen.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f).SetEase(Ease.OutBounce).Play();
    }
}
