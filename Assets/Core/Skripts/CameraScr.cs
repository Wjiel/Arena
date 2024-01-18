using UnityEngine;

public class CameraScr : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, player.position, speed * Time.deltaTime);
    }
}
