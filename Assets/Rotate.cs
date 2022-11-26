using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 dir;

    private void Update() => transform.Rotate(dir, speed);
}