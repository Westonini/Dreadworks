using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public Rigidbody bulletPrefab;

    public Transform shootPosition;

    public GameObject muzzleFlashLight;
    public ParticleSystem muzzleFlashParticles;

    [HideInInspector]
    public int bulletsInMag = 6;
    [HideInInspector]
    public bool reloadTimeActive = false;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadText;

    private Inventory inv;

    void Awake()
    {
        inv = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        ammoText.text = "Ammo: \n" + bulletsInMag.ToString() + " / " + inv.ammo.ToString();

        if (Input.GetButtonDown("Fire1") && bulletsInMag != 0 && reloadTimeActive == false) //Shoot Input
        {
            Shoot();
        }

        if (Input.GetButtonDown("Reload") && inv.ammo > 0 && bulletsInMag != 6 && reloadTimeActive == false) //Reload Input
        {
            reloadText.text = "Reloading...";
            reloadTimeActive = true;
            Invoke("Reload", 1.5f);
        }

    }

    void Shoot()
    {
        Rigidbody bulletInstance;
        bulletInstance = Instantiate(bulletPrefab, shootPosition.position, shootPosition.rotation) as Rigidbody;
        bulletInstance.AddForce(shootPosition.forward * 1500);
        Destroy(bulletInstance.gameObject, 0.5f);
        StartCoroutine(ToggleMuzzleFlash());
        bulletsInMag -= 1;
    }

    void Reload()
    {
        reloadTimeActive = false;
        reloadText.text = "";

        int newBulletsInMag;

        //Ammo reloading logic
        if (inv.ammo <= 6 && bulletsInMag == 0)
        {
            newBulletsInMag = inv.ammo;
        }
        else if (inv.ammo <= 6 && bulletsInMag != 0)
        {
            int newBulletsInMagPlaceholder = 6 - bulletsInMag;

            if (newBulletsInMagPlaceholder > inv.ammo)
            {
                newBulletsInMag = inv.ammo;
            }
            else
            {
                newBulletsInMag = 6 - bulletsInMag;
            }
        }
        else
        {
            newBulletsInMag = 6 - bulletsInMag;         
        }
        
        bulletsInMag += newBulletsInMag;
        inv.ammo -= newBulletsInMag;
        reloadText.text = "";

        if (inv.ammo < 0)
        {
            inv.ammo = 0;
        }
    }

    public IEnumerator ToggleMuzzleFlash()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlashLight.SetActive(false);
    }
}
