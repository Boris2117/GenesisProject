using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace HeneGames.DialogueSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickMovement : MonoBehaviour
    {
        private NavMeshAgent nav;

        [SerializeField] private LayerMask pointMask;

        private void Start()
        {
            nav = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            ClickMovementInput();
        }

        private void ClickMovementInput()
        {
            var mouse = Mouse.current;
            if (mouse == null) return;

            //Mouse left click
            if (mouse.leftButton.wasPressedThisFrame)
            {
                //Ray from the camera to the mouse position
                Ray _ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());

                //Set navmesh destination to raycast hit point
                if (Physics.Raycast(_ray, out RaycastHit _hit, 100f, pointMask))
                {
                    if (nav.enabled)
                    {
                        nav.SetDestination(_hit.point);
                    }
                }
            }
        }

        public void StopMovement(bool _value)
        {
            nav.isStopped = _value;
        }
    }
}