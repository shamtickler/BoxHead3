using UnityEngine;
using System.Collections;

public class GunStats : MonoBehaviour {

    public float damage;
    public float rateOfFire;
    public float magazineCurrent;
    public float magazineSize;
    public float reloadTime;
    public bool reloading = false;
    public float numberOfProjectiles;
    public float accuracy;

    public GameObject projectile;

    [SerializeField]
    private LayerMask shootMask;

    private bool canShoot = true;
    private RaycastHit hit;

    //Shoot weapon
    public void Shoot()
    {
        if (canShoot && magazineCurrent > 0)
        {

            //Get direction weapon is facing
            Vector3 _direction = transform.forward;

            //instantiate line prefab
            GameObject newLineObject = (GameObject)Instantiate(projectile, transform.position, Quaternion.Euler(_direction));
            LineRenderer newLine = newLineObject.GetComponent<LineRenderer>();

            //Add randomization based on "accuracy" stat value
            _direction.x += Random.Range(-accuracy/100, accuracy/100);
            _direction.z += Random.Range(-accuracy/100, accuracy/100);

            //create the ray we are using to represent a bullet
            Ray shootRay = new Ray(transform.position, _direction);

            if(Physics.Raycast(shootRay, out hit, 500, shootMask))
            {
                newLine.SetPosition(0, transform.position);
                newLine.SetPosition(1, hit.point);

            }
            else  //if the ray doesnt hit anything we draw it anyways
            {
                newLine.SetPosition(0, transform.position);
                newLine.SetPosition(1, transform.position + _direction * 500);
            }

            //subtracts a bullet from the magazine
            magazineCurrent -= 1;
            //calculates when the next time the gun can be fired is
            StartCoroutine(CalculateFireRate());
        }
    }

    //This is called after fireing each projectile to cound down when the next projectile cn be fired
    IEnumerator CalculateFireRate()
    {
        canShoot = false;
        float _waitTime = 1 / rateOfFire;
        yield return new WaitForSeconds(_waitTime);
        canShoot = true;
    }

    //reload the weapon to maximum magazine capacity
    public IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        magazineCurrent = magazineSize;
        reloading = false;
        Debug.Log("RELOADED!!!!!!!");
    }

    //gets called by PlayerController.Update() and controls the direction the weapon is facing
    public void Aim(Vector3 _aimLocation)
    {
        transform.LookAt(_aimLocation);
    }

    void Update()
    {
        //reload on button press
        if (Input.GetKeyDown("r"))
        {
            StartCoroutine(Reload());
        }

        //Shoot
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }

    }

}
