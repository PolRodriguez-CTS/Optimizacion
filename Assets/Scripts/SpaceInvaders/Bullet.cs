using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;

    private float _shootForce = 50f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rb.AddForce(transform.forward * _shootForce, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            gameObject.SetActive(false);
            other.GetComponent<Enemy>().Death();
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
