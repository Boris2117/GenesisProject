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
    public Transform inspectSpawnPoint; // objeto vacío frente a la inspectCamera

    private Interactable currentTarget;
    private InputAction interactAction;
    private bool isInspecting;
    private GameObject currentInspectInstance;

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

        Vector3 targetPosition = inspectCamera.transform.position + inspectCamera.transform.forward * 2f;

        // instanciar la copia (todavía sin acomodar)
        GameObject instance = Instantiate(target.gameObject, targetPosition, Quaternion.identity);

        // calcular el centro visual real (padre + hijos)
        Renderer[] renderers = instance.GetComponentsInChildren<Renderer>();
        Vector3 visualCenter = targetPosition;
        if (renderers.Length > 0)
        {
            Bounds combinedBounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
            {
                combinedBounds.Encapsulate(renderers[i].bounds);
            }
            visualCenter = combinedBounds.center;
        }

        // crear un wrapper vacío EXACTAMENTE en el centro visual
        GameObject wrapper = new GameObject("InspectWrapper");
        wrapper.transform.position = targetPosition;

        // meter la instancia adentro del wrapper, compensando el offset
        instance.transform.SetParent(wrapper.transform);
        instance.transform.position += (targetPosition - visualCenter);

        currentInspectInstance = wrapper; // ahora "el objeto" es el wrapper

        Interactable copyInteractable = instance.GetComponent<Interactable>();
        if (copyInteractable != null) Destroy(copyInteractable);

        Rigidbody rb = instance.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        inspectSystem.objectToInspect = wrapper.transform; // rotás el WRAPPER, no la botella

        inspectManager.EnterInspectMode();

        promptCanvas.Hide();
    }

    

    void ExitInspect()
    {
        isInspecting = false;

        if (currentInspectInstance != null)
        {
            Destroy(currentInspectInstance);
            currentInspectInstance = null;
        }

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
