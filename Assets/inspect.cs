using UnityEngine;
using UnityEngine.InputSystem;

public class inspect : MonoBehaviour
{
    [Header("Revelado")]
    [SerializeField] string propiedad = "_Reveal";   
    [SerializeField] float Revel = 0.0005f; 
    [SerializeField] float decay = 0f;          
    
    private Renderer rend;
    private float reveal;

    public Transform objectToInspect;
    public float rotationSpeed = 100f;

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

    void OnDisable()
    {
        pointAction.Disable();
        clickAction.Disable();
    }

    void Update()
    {
        if (objectToInspect == null) return;

        // buscamos el Renderer una sola vez (sirve aunque el material esté en un hijo)
        if (rend == null)
            rend = objectToInspect.GetComponentInChildren<Renderer>();

        if (clickAction.WasPressedThisFrame())
        {
            previousMousePosition = pointAction.ReadValue<Vector2>();
        }

        if (clickAction.IsPressed())
        {
            Vector3 deltaMousePosition = (Vector3)pointAction.ReadValue<Vector2>() - previousMousePosition;
            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            objectToInspect.rotation = rotation * objectToInspect.rotation;

            // cuanto más movés el mouse, más sube el float
            reveal += deltaMousePosition.magnitude * Revel;

            previousMousePosition = pointAction.ReadValue<Vector2>();
        }
      

        reveal = Mathf.Clamp01(reveal);

        if (rend != null)
            rend.material.SetFloat(propiedad, reveal);
    }
}
