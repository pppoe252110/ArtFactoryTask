using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    [SerializeField] private float _scaleDuration = 0.25f;
    [SerializeField] private float _physicsDuration = 5f;
    [SerializeField] private float _collisionTorque = 90f;
    [SerializeField] private float _collisionForce = 10f;
    [SerializeField] private float _downForce = 5f;
    [SerializeField] private float _startForce = 5f;
    [SerializeField] private float _startTorque = 90f;
    [SerializeField] private float _bouncelessPerCollision = 0.1f;
    [SerializeField] private float _bouncenessAmount = 0.85f;
    [SerializeField] private AnimationCurve _bouncenessCurve = AnimationCurve.EaseInOut(0, 1, 0, 1);
    [SerializeField] private Rigidbody2D _rb;

    private float _bounciness = 1f;

    internal bool IsSleeping
    {
        get
        {
            return _rb.IsSleeping();
        }
    }

    public void Proceed()
    {
        var tween = transform.DOScale(transform.localScale, _scaleDuration).SetEase(Ease.OutExpo);
        transform.localScale = Vector3.zero;
        tween.Play();

        _rb.AddForce(Quaternion.Euler(0, 0, Random.Range(-10, 10)) * Vector2.down * _startForce, ForceMode2D.Impulse);
        _rb.totalTorque = _startTorque * (Random.Range(1,3)*2-3);

        StartCoroutine(ProceedPhysics());
    }
    private void FixedUpdate()
    {
        if(_rb.linearVelocity.magnitude <=Physics2D.linearSleepTolerance)
        {
            _rb.Sleep();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer != gameObject.layer)
        {
            _bounciness -= _bouncelessPerCollision;
            _bounciness = Mathf.Clamp01(_bounciness);
            _rb.linearVelocity /= 2;
            _rb.AddTorque(_collisionTorque * ((Random.Range(1, 3) * 2 - 3) * _bounciness));
            _rb.AddForce(_collisionForce * Random.onUnitSphere * _bounciness);
        }
    }
    private IEnumerator ProceedPhysics()
    {
        var material = new PhysicsMaterial2D
        {
            bounciness = 1,
            bounceCombine = PhysicsMaterialCombine2D.Multiply,
            friction = 0,
            frictionCombine = PhysicsMaterialCombine2D.Multiply,
        };

        float time = 0;
        while (time < _physicsDuration) 
        {
            time += Time.deltaTime;

            material.bounciness = Mathf.Clamp01(_bouncenessCurve.Evaluate(time / _physicsDuration) * _bounciness) * _bouncenessAmount;
            material.friction = Mathf.Clamp01(1f - _bouncenessCurve.Evaluate(time / _physicsDuration)) * _bouncenessAmount;

            _rb.sharedMaterial = material;

            if (_rb.linearVelocityY > 0f)
            {
                _rb.AddForce(Quaternion.Euler(0, 0, Random.Range(-10, 10)) * Vector2.down * Time.deltaTime * _downForce, ForceMode2D.Impulse);
            }
            yield return null;
        }
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
