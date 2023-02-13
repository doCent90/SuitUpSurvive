using UnityEngine;

namespace Assets.Source
{
    public class AimMovement : MonoBehaviour
    {
        [SerializeField] private Transform _aimIcon;
#if !UNITY_EDITOR
        private void Start() => Cursor.visible = false;
#endif
        private void Update() => _aimIcon.position = Input.mousePosition;
    }
}
