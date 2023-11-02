using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private WeaponBase myWeapon;
    private bool weaponShootToggle;

    private Vector2 currentRotation;
    

    [SerializeField, Range(1,20)] private float mouseSensX;
    [SerializeField, Range(1,20)] private float mouseSensY;
    [SerializeField, Range(-90,0)] private float minViewAngle;
    [SerializeField, Range(0,90)] private float maxViewAngle;

    [SerializeField] private Transform followTarget;

    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float projectileForce;

  //  [SerializeField] private TextMeshProUGUI ammo;

    private int ammoCounter = 24;

    private Vector2 currentAngle;

    private bool isGrounded;
    private Vector3 _moveDir;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.init(this);
        InputManager.GameMode();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDir);
        CheckGround();

        if(ammoCounter <= 0)
        {
            ammoCounter = 24;
        }
    }

    public void SetMovementDirection(Vector3 newDirection)
    {
        _moveDir = newDirection;
    }

    public void Jump()
    {
        Debug.Log("Jump called");
        if (isGrounded)
        {
            Debug.Log("Jumped");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.size.y);
        Debug.DrawRay(transform.position, Vector3.down * GetComponent<Collider>().bounds.size.y, Color.red, 0, false);
    }

    public void SetLookRotation(Vector2 readValue)
    {
        currentAngle.x += readValue.x * Time.deltaTime * mouseSensX;
        currentAngle.y += readValue.y * Time.deltaTime * mouseSensY;

        currentAngle.y = Mathf.Clamp(currentAngle.y, minViewAngle, maxViewAngle);

        transform.rotation = Quaternion.AngleAxis(currentAngle.x, Vector3.up);
        followTarget.localRotation = Quaternion.AngleAxis(currentAngle.y, Vector3.right);
    }

    public void Shoot()
    {
       /* Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
       currentProjectile.AddForce(followTarget.forward * projectileForce, ForceMode.Impulse);

       --ammoCounter;

      // ammo.text = ("ammo:"+ammoCounter.ToString());

       Destroy(currentProjectile.gameObject, 4); */

       Print("I shot");

       weaponShootToggle = !weaponShootToggle;
       if (weaponShootToggle) myWeapon.StartShooting();
       else myWeapon.StopShooting();
    }
}
