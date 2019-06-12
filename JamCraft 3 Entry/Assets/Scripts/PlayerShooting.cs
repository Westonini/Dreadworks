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

    private int bulletsInMag = 6;
    private int bulletsInPouch = 30;
    private float reloadTime = 1.5f;
    private bool reloadTimeActive = false;

    public TextMeshProUGUI ammoText;
    public TextMeshPro reloadText;

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "Ammo: \n" + bulletsInMag.ToString() + " / " + bulletsInPouch.ToString();

        if (Input.GetButtonDown("Fire1") && bulletsInMag != 0 && reloadTimeActive == false) //Shoot Input
        {
            Shoot();
        }

        if (Input.GetButtonDown("Reload") && bulletsInPouch > 0 && bulletsInMag != 6) //Reload Input
        {
            reloadTimeActive = true;
        }

        if (reloadTimeActive == true) // If the player is reloading
        {
            reloadText.text = "Reloading...";
            reloadTime -= Time.deltaTime;

            if (reloadTime <= 0)
            {
                Reload();

                reloadTimeActive = false;
                reloadText.text = "";
                reloadTime = 1.5f;
            }
        }
    }

    void Shoot()
    {
        Rigidbody bulletInstance;
        bulletInstance = Instantiate(bulletPrefab, shootPosition.position, shootPosition.rotation) as Rigidbody;
        bulletInstance.AddForce(shootPosition.forward * 1000);
        Destroy(bulletInstance.gameObject, 0.5f);
        StartCoroutine(ToggleMuzzleFlash());
        bulletsInMag -= 1;
    }

    void Reload()
    {
        int newBulletsInMag;
        newBulletsInMag = 6 - bulletsInMag;
        bulletsInMag += newBulletsInMag;
        bulletsInPouch -= newBulletsInMag;
        reloadText.text = "";

        if (bulletsInPouch < 0)
        {
            bulletsInPouch = 0;
        }
    }

    private IEnumerator ToggleMuzzleFlash()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlashLight.SetActive(false);
    }
}
