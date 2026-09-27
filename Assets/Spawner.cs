using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPrefab), spawnInterval, spawnInterval);
    }

    private void SpawnPrefab()
    {
        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        Instantiate(prefab, position, prefab.transform.rotation);
    }


}
