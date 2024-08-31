using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FillSlider : MonoBehaviour
{
    [SerializeField] private Slider _fillSlider;
    [SerializeField] private ItemsSpawner _spawner;

    public float Value
    {
        get
        {
            return _fillSlider.value;
        }
    }

    void Update()
    {
        _fillSlider.value = Mathf.MoveTowards(_fillSlider.value, _spawner.GetSleepings(), Time.deltaTime);
    }

    public void DoAnimation()
    {
        _fillSlider.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f).SetEase(Ease.OutBounce).Play();
    }
}
