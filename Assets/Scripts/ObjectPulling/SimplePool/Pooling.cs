using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling Instance;

    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private string parentName;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        GameObject obj;
        GameObject parent = new GameObject(parentName);

        for (int i = 0; i < _poolSize; i++)
        {
            obj = Instantiate(_prefab);
            obj.transform.SetParent(parent.transform);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < _poolSize; i++)
        {
            //Comprobamos si el objeto está desactivado
            if(!_pooledObjects[i].activeInHierarchy)
            {
                GameObject objectToSpawn;
                objectToSpawn = _pooledObjects[i];
                
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
                return objectToSpawn;
            }
        }
        Debug.Log("No hay balas disponibles");
        return null;
    } 
}
