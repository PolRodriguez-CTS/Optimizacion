using UnityEngine;

public class EnemyBullet : MonoBehaviour
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

        if(other.tag == "Player")
        {
            other.GetComponent<Enemy>().Death();
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
