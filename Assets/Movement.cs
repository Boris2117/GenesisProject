using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 direction = Vector3.forward;

    private void Start()
    {

        Destroy(gameObject, 10f);
    }
    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
