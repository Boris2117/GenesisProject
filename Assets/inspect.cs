using UnityEngine;
using UnityEngine.InputSystem;

public class inspect : MonoBehaviour
{
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

            previousMousePosition = pointAction.ReadValue<Vector2>();
        }
    }
}
