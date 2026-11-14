using UnityEngine;

public class vagon : MonoBehaviour
{
    public Vector3 direccion = Vector3.forward;
    public float velocidad = 10f;
    public float segundos = 5f;

    Vector3 posicionInicial;
    float timer;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        transform.position += direccion.normalized * velocidad * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= segundos)
        {
            transform.position = posicionInicial;
            timer = 0f;
        }
    }
}
