using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    public Camera playerCamera;
    public float interactRange = 3f;
    public LayerMask interactableLayer;
    public InteractPrompt promptCanvas;

    [Header("Inspect Mode")]
    public Camera inspectCamera;
    public GameObject playerCameraObject;
    public inspectionMOde inspectManager;
    public inspect inspectSystem;

    private Interactable currentTarget;
    private InputAction interactAction;
    private bool isInspecting;

    void OnEnable()
    {
        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.Enable();
        interactAction.performed += ctx => OnInteractPressed();
    }

    void OnDisable()
    {
        interactAction.Disable();
    }

    void OnInteractPressed()
    {
        if (isInspecting)
        {
            ExitInspect();
        }
        else if (currentTarget != null)
        {
            EnterInspect(currentTarget);
        }
    }

    void EnterInspect(Interactable target)
    {
        isInspecting = true;

        playerCameraObject.SetActive(false);
        inspectCamera.gameObject.SetActive(true);

        inspectSystem.objectToInspect = target.transform;

        inspectManager.EnterInspectMode();

        promptCanvas.Hide();
    }

    void ExitInspect()
    {
        isInspecting = false;

        inspectCamera.gameObject.SetActive(false);
        playerCameraObject.SetActive(true);

        inspectManager.ExitInspectMode();
    }

    void Update()
    {
        if (isInspecting) return;

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
