using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFoto : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform puntoFoto;
    [SerializeField] LayerMask capaEvidencia;
    [SerializeField] float distanciaMax = 3f;
    [SerializeField] float cooldown = 1f;
    public static GameObject ultimaFoto;
    float proximoUso;

    void Update()
    {
        if (!Keyboard.current.fKey.wasPressedThisFrame) return;
        if (Time.time < proximoUso) return;

        proximoUso = Time.time + cooldown;
        // acá disparás tu flash

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (!Physics.Raycast(ray, out RaycastHit hit, distanciaMax, capaEvidencia)) return;

        var ev = hit.collider.GetComponentInParent<Evidence>();
        if (ev == null || ev.polaroidPrefab == null || ev.fotografiada) return;

        ev.fotografiada = true;
        ultimaFoto = Instantiate(ev.polaroidPrefab, puntoFoto.position, puntoFoto.rotation, puntoFoto);
    }
}
