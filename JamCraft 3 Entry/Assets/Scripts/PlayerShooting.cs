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
    [HideInInspector]
    public bool reloadTimeActive = false;

    public TextMeshProUGUI ammoText;
    public TextMeshPro reloadText;

    private SlotSelection SS;
    private InventoryANDInteract inv;

    void Awake()
    {
        SS = GameObject.FindWithTag("Player").GetComponent<SlotSelection>();
        inv = GameObject.FindWithTag("Inventory").GetComponent<InventoryANDInteract>();
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

        if (SS.cancelReload == true)
        {
            CancelReloadAndMuzzleFlash();
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
        reloadTimeActive = false;
        reloadText.text = "";

        int newBulletsInMag;
        newBulletsInMag = 6 - bulletsInMag;
        bulletsInMag += newBulletsInMag;
        inv.ammo -= newBulletsInMag;
        reloadText.text = "";

        if (inv.ammo < 0)
        {
            inv.ammo = 0;
        }
    }

    private IEnumerator ToggleMuzzleFlash()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlashLight.SetActive(false);
    }

    void CancelReloadAndMuzzleFlash()
    {
        CancelInvoke("Reload");
        StopCoroutine(ToggleMuzzleFlash());
        muzzleFlashLight.SetActive(false);
        muzzleFlashParticles.Stop();
        reloadText.text = "";
        reloadTimeActive = false;
        
        SS.cancelReload = false;
    }
}
