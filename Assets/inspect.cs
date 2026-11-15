using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inspect : MonoBehaviour
{
    [Header("Revelado")]
    [SerializeField] string propiedad = "_Reveal";
    [SerializeField] float gananciaRevelado = 0.0015f;

    [Header("Inspección")]
    public Transform objectToInspect;   // el wrapper
    public float rotationSpeed = 100f;

    private Dictionary<Interactable, float> reveals = new Dictionary<Interactable, float>();
    private HashSet<Interactable> contadas = new HashSet<Interactable>();

    private Interactable actual;
    private Renderer rend;
    private bool esRevelable;
    [SerializeField] float timetodestroy = 1.5f;
    private Vector3 previousMousePosition;
    private InputAction pointAction;
    private InputAction clickAction;

    void OnEnable()
    {
        pointAction = new InputAction(binding: "<Mouse>/position");
        clickAction = new InputAction(binding: "<Mouse>/leftButton");
        pointAction.Enable();
        clickAction.Enable();
    }



  

    void Update()
    {
        if (objectToInspect == null) return;

        // buscamos el Interactable dentro del wrapper (por si cambia el contenido)
        var info = objectToInspect.GetComponentInChildren<Interactable>();

        if (info != actual)
        {
            actual = info;
            rend = actual != null
                ? actual.GetComponentInChildren<Renderer>()
                : objectToInspect.GetComponentInChildren<Renderer>();

            esRevelable = actual != null && actual.tipo == TipoInspeccion.Revelable;

            if (esRevelable && !reveals.ContainsKey(actual))
                reveals[actual] = 0f;
        }

        if (clickAction.WasPressedThisFrame())
            previousMousePosition = pointAction.ReadValue<Vector2>();

        if (clickAction.IsPressed())
        {
            Vector3 deltaMousePosition = (Vector3)pointAction.ReadValue<Vector2>() - previousMousePosition;
            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            objectToInspect.rotation = rotation * objectToInspect.rotation;

            if (esRevelable && !contadas.Contains(actual))
                reveals[actual] += deltaMousePosition.magnitude * gananciaRevelado;

            previousMousePosition = pointAction.ReadValue<Vector2>();
        }

        // los objetos normales terminan acá (solo rotan)
        if (!esRevelable) return;

        reveals[actual] = Mathf.Clamp01(reveals[actual]);
        if (rend != null) rend.material.SetFloat(propiedad, reveals[actual]);

        // al terminar de revelar, suma una sola vez por objeto
        if (reveals[actual] >= 1f && !contadas.Contains(actual))
        {
            contadas.Add(actual);
            Objetives.Instance.SumarPista();
            
        }


    }
    void OnDisable()
    {
        pointAction.Disable();
        clickAction.Disable();

        // si en esta inspección se reveló una foto, destruimos la que está en la mano
        if (contadas.Count > 0 && CameraFoto.ultimaFoto != null)
        {
            Destroy(CameraFoto.ultimaFoto);
            CameraFoto.ultimaFoto = null;
        }

        contadas.Clear();
        reveals.Clear();
        actual = null;
        esRevelable = false;
        rend = null;
    }
}
