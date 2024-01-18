using UnityEngine;

public class PricelScr : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float speed;
    private void Start()
    {
        Cursor.visible = true;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _camera.ScreenToWorldPoint(Input.mousePosition), speed * Time.deltaTime);
    }
}
