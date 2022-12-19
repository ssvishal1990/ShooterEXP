using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MouseWorld
{
    public class MouseTracker : MonoBehaviour
    {
        private static MouseTracker instance;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private GameObject aimReticule;

        private void Awake()
        {
            instance = this;
        }

        public static Vector3 GetMousePosition()
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.groundLayerMask);
            return raycastHit.point;
        }

        private void Update()
        {
            Vector3 mousePosition =  GetMousePosition();
            aimReticule.transform.position = mousePosition;
        }

        public static Transform getAimReticulePosition()
        {
            return instance.aimReticule.transform;
        }


    }
}