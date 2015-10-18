using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 15f;
    [SerializeField]
    private LayerMask playerAimMask;

    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject weapon;

    private RaycastHit hit;

    private PlayerMotor motor;
    private GunStats GunStats;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        GunStats = weapon.GetComponent<GunStats>();
    }

    void Update()
    {
        //Calculate the movement velocity as a 3d vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = Vector3.right * _xMov;
        Vector3 _movVertical = Vector3.forward * _zMov;

        //Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Apply movement
        motor.Move(_velocity);

        //Get position of mouse in world space and pass that location to the motor to have the character rotate towards it
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, playerAimMask))
        {
            Vector3 _aimPoint = hit.point;
            _aimPoint.y = 0.5f;
            motor.Rotate(_aimPoint);
            if (weapon != null)
            {
                GunStats.Aim(_aimPoint);
            }
        }

        //Make character jump
        if (Input.GetKeyDown("space"))
        {
            if (Physics.Raycast(transform.position, -Vector3.up, 1.25f))
            {
                motor.Jump(jumpForce);
            }
        }
    }

}
