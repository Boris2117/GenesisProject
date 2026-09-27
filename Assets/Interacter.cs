using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    public Camera playerCamera;
    public float interactRange = 3f;
    public LayerMask interactableLayer;
    public InteractPrompt promptCanvas;

    [Header("Inspect Mode")]
    public Camera inspectCamera;          // la cámara de inspección (desactivada por default)
    public GameObject playerCameraObject; // la cámara del pj, para desactivarla al inspeccionar
    public inspectionMOde inspectManager;
    public inspect inspectSystem;

    private Interactable currentTarget;
    private InputAction interactAction;

    void OnEnable()
    {
        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.Enable();
        interactAction.performed += ctx => TryInteract();
    }

    void OnDisable()
    {
        interactAction.Disable();
    }

    void TryInteract()
    {
        if (currentTarget != null)
        {
            EnterInspect(currentTarget);
        }
    }

    void EnterInspect(Interactable target)
    {
        // apagar cámara del pj, prender cámara de inspección
        playerCameraObject.SetActive(false);
        inspectCamera.gameObject.SetActive(true);

        // asignar el objeto a inspeccionar
        inspectSystem.objectToInspect = target.transform;

        // avisar al manager (cursor, StarterAssetsInputs, etc.)
        inspectManager.EnterInspectMode();

        // ocultar el prompt mientras se inspecciona
        promptCanvas.Hide();
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);

            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                currentTarget = interactable;
                promptCanvas.Show();
                return;
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);
        }

        currentTarget = null;
        promptCanvas.Hide();
    }
}
