using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class CameraFoto : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform puntoFoto;
    [SerializeField] LayerMask capaEvidencia;
    [SerializeField] float distanciaMax = 3f;
    [SerializeField] float cooldown = 1f;
    public static GameObject ultimaFoto;
    float proximoUso;

    [SerializeField] GameObject iconoCamara;

    bool parpadeando;
    void Update()
    {
        // ¿hay una pista fotografiable en la mira?
        Evidence ev = null;
        bool puede = Physics.Raycast(
            cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)),
            out RaycastHit hit, distanciaMax, capaEvidencia);

        if (puede)
        {
            ev = hit.collider.GetComponentInParent<Evidence>();
            puede = ev != null && ev.polaroidPrefab != null && !ev.fotografiada;
        }

        // el icono solo se ve si se puede sacar foto (y no está parpadeando)
        if (iconoCamara != null && !parpadeando)
            iconoCamara.SetActive(puede);

        if (!Keyboard.current.fKey.wasPressedThisFrame) return;
        if (Time.time < proximoUso) return;

        proximoUso = Time.time + cooldown;
        // acá disparás tu flash

        if (!puede) return;

        ev.fotografiada = true;
        ultimaFoto = Instantiate(ev.polaroidPrefab, puntoFoto.position, puntoFoto.rotation, puntoFoto);

        if (iconoCamara != null) StartCoroutine(Parpadeo());
    }

    IEnumerator Parpadeo()
    {
        parpadeando = true;
        for (int i = 0; i < 3; i++)
        {
            iconoCamara.SetActive(true);
            yield return new WaitForSeconds(0.08f);
            iconoCamara.SetActive(false);
            yield return new WaitForSeconds(0.08f);
        }
        parpadeando = false; // queda apagado: la pista ya está fotografiada
    }
}
