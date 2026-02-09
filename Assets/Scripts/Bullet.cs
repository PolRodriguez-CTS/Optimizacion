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
        gameObject.SetActive(false);  
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
