using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class CannonController : MonoBehaviour
{
  [SerializeField] private Transform barrel;
  [SerializeField] private Transform barrelSpawnLocation;
  
  [SerializeField] private Transform wheelFrontRight;
  [SerializeField] private Transform wheelFrontLeft;
  [SerializeField] private Transform wheelBackRight;
  [SerializeField] private Transform wheelBackLeft;

  public GameObject projectilePrefab;
  public float projectileForce = 30f;
  
  public float aimSpeed = 10f;
  public float aimRotationMin = -10f;
  public float aimRotationMax = 10f;

  public float turnSpeed = 30f;
  public float turnRotationMin = -30f;
  public float turnRotationMax = 30f;
  
  private SimpleControls _simpleControls;
  private Vector2 _aimRotation = Vector2.zero;
  private Vector2 _turnRotation;
  private Vector2 _wheelRotation = Vector2.zero;

  public float shotDelay = 0.5f;
  private float _timeSinceLastShot = 0f;

  private void Awake()
  {
    _simpleControls = new SimpleControls();
    _turnRotation = transform.localEulerAngles;
    turnRotationMin += _turnRotation.y;
    turnRotationMax += _turnRotation.y;
  }

  private void OnEnable()
  {
    _simpleControls.Enable();
    _simpleControls.gameplay.fire.performed += ctx => { Fire();};
    _simpleControls.gameplay.menu.performed += ctx => { MenuManager.Instance.ToggleMenu();};
  }

  private void OnDisable()
  {
    _simpleControls.Disable();
  }

  private void Update()
  {
    Vector2 move = _simpleControls.gameplay.move.ReadValue<Vector2>();
    Aim(move.y);
    Turn(move.x);

    _timeSinceLastShot += Time.deltaTime;
  }

  private void ToggleMenu()
  {
    if (MenuManager.Instance != null)
    {
      MenuManager.Instance.ToggleMenu();
    }
  }

  private void Aim(float aimDirection)
  {
    float scaledAimSpeed = aimSpeed * Time.deltaTime;
    float aimAmount = aimDirection * scaledAimSpeed;
    
    _aimRotation.x = Mathf.Clamp(_aimRotation.x + aimAmount, aimRotationMin, aimRotationMax);
    barrel.localEulerAngles = _aimRotation;
  }

  private void Turn(float turnDirection)
  {
    float scaledTurnSpeed = turnSpeed * Time.deltaTime;
    float turnAmount = turnDirection * scaledTurnSpeed;

    _turnRotation.y = Mathf.Clamp(_turnRotation.y + turnAmount, turnRotationMin, turnRotationMax);
    transform.localEulerAngles = _turnRotation;

    _wheelRotation.x = _turnRotation.y;
    wheelFrontLeft.localEulerAngles = _wheelRotation;
    wheelBackLeft.localEulerAngles = _wheelRotation *2;
    wheelFrontRight.localEulerAngles = -_wheelRotation;
    wheelBackRight.localEulerAngles = -_wheelRotation *2;
  }

  private void Fire()
  {
    if (_timeSinceLastShot >= shotDelay)
    {
      _timeSinceLastShot = 0f;
      var cannonBall = Instantiate(projectilePrefab, barrelSpawnLocation.position, barrelSpawnLocation.rotation);
      var rigidBody = cannonBall.GetComponent<Rigidbody>();
      rigidBody.AddForce(cannonBall.transform.forward * projectileForce, ForceMode.Impulse);
    }
  }
}
