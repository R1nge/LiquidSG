using UnityEngine;

public class Wobble : MonoBehaviour
{
    [SerializeField] private float maxWobble = 0.03f;
    [SerializeField] private float wobbleSpeed = 1f;
    [SerializeField] private float recovery = 1f;
    private Renderer _rend;
    private Vector3 _lastPos;
    private Vector3 _velocity;
    private Vector3 _lastRot;
    private Vector3 _angularVelocity;
    private float _wobbleAmountX;
    private float _wobbleAmountZ;
    private float _wobbleAmountToAddX;
    private float _wobbleAmountToAddZ;
    private float _pulse;
    private float _time = 0.5f;
    private static readonly int WobbleX = Shader.PropertyToID("_WobbleX");
    private static readonly int WobbleZ = Shader.PropertyToID("_WobbleZ");

    private void Awake() => _rend = GetComponent<Renderer>();

    private void Update()
    {
        _time += Time.deltaTime;

        _wobbleAmountToAddX = Mathf.Lerp(_wobbleAmountToAddX, 0, Time.deltaTime * (recovery));
        _wobbleAmountToAddZ = Mathf.Lerp(_wobbleAmountToAddZ, 0, Time.deltaTime * (recovery));

        _pulse = 2 * Mathf.PI * wobbleSpeed;
        _wobbleAmountX = _wobbleAmountToAddX * Mathf.Sin(_pulse * _time);
        _wobbleAmountZ = _wobbleAmountToAddZ * Mathf.Sin(_pulse * _time);

        _rend.material.SetFloat(WobbleX, _wobbleAmountX);
        _rend.material.SetFloat(WobbleZ, _wobbleAmountZ);

        _velocity = (_lastPos - transform.position) / Time.deltaTime;
        _angularVelocity = transform.rotation.eulerAngles - _lastRot;

        _wobbleAmountToAddX +=
            Mathf.Clamp((_velocity.x + (_angularVelocity.z * 0.2f)) * maxWobble, -maxWobble, maxWobble);
        _wobbleAmountToAddZ +=
            Mathf.Clamp((_velocity.z + (_angularVelocity.x * 0.2f)) * maxWobble, -maxWobble, maxWobble);

        _lastPos = transform.position;
        _lastRot = transform.rotation.eulerAngles;
    }
}