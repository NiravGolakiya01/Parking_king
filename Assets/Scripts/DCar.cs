using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCar : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem smokeFX;
    [SerializeField] float danceValue;
    public Transform bodyTransform;


    private void Start()
    {
        bodyTransform.DOLocalMoveY(danceValue, .1f)
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetEase(Ease.Linear);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Car otherCar))
        {
            StopDancingAnim();
            rb.DOKill(false);

            // ADD Explosion:
            Vector3 hitPoint = collision.contacts[0].point;
            AddExplosionForce(hitPoint);

            smokeFX.Play();

            Game.Instance.OnCarCollision.Invoke();
        }
    }


    private void AddExplosionForce(Vector3 point)
    {
        rb.AddExplosionForce(400f, point, 3f);
        rb.AddForceAtPosition(Vector3.up * 2f, point, ForceMode.Impulse);
        rb.AddTorque(new Vector3(GetRandomAngle(), GetRandomAngle(), GetRandomAngle()));
    }


    private float GetRandomAngle()
    {
        float angle = 10f;
        float rand = Random.value;
        return rand > .5f ? angle : -angle;
    }

    public void StopDancingAnim()
    {
        bodyTransform.DOKill(true);
    }
}
