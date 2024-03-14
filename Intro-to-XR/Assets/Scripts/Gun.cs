using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public CustomGrab leftHand = null;
    public CustomGrab rightHand = null;
    public InputActionReference shootL;
    public InputActionReference shootR;
    bool shooting = false;
    public GameObject muzzlePoint;
    public GameObject impactPrefab;
    public float bulletSpeed = 20f; 
    public LayerMask hittableLayers; 

    public float fireRate = 5.0f; // Shots per second
    private bool canShoot = true; 

    private AudioSource audioSource;
    public GameObject bulletPrefab;

    void Start() 
    {
        shootL.action.Enable();
        shootR.action.Enable();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (leftHand != null && leftHand.grabbedObject == this.gameObject.transform)
        {
            shooting = shootL.action.IsPressed();
        } 
        else if (rightHand != null && rightHand.grabbedObject == this.gameObject.transform)
        {
            shooting = shootR.action.IsPressed();
        }
        if (shooting) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(canShoot) 
        {
            canShoot = false;
            //Debug.Log("Shoot");
            audioSource.Play();
            // Raycast for hitting objects
            RaycastHit hitInfo;
            if (Physics.Raycast(muzzlePoint.transform.position, muzzlePoint.transform.forward, out hitInfo, Mathf.Infinity, hittableLayers))
            {
                // Create impact effect
                //GameObject impact = Instantiate(impactPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                //Destroy(impact, 2f); // Destroy impact after 2 seconds
                
                // Simulate simple bullet travel (optional)
                //GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //bullet.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                //bullet.transform.position = muzzlePoint.transform.position;
                //Rigidbody rb = bullet.AddComponent<Rigidbody>();

                GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.transform.position, muzzlePoint.transform.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(muzzlePoint.transform.forward * bulletSpeed, ForceMode.Impulse);
                Destroy(bullet, 3f); // Destroy bullet after 3 seconds
                
            }
            StartCoroutine(ShootingCooldown());
        }
    }

    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(1f / fireRate);
        canShoot = true;
    }
}