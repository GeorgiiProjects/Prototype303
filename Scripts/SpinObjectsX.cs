using UnityEngine;

public class SpinObjectsX : MonoBehaviour
{
    // скорость вращения монет, бомб и шарика, настраивается в инспекторе.
    public float spinSpeed;

    void Update()
    {
        // Заставляем вращаться монеты, бомбы и шарик со скоростью spinSpeed одинаково на всех пк.
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
