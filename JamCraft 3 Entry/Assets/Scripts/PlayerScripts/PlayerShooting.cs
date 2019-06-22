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

    public float shootCooldown = 0.25f;
    private float shootCooldownReset;
    private bool shootIsOnCooldown = false;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadText;

    private Inventory inv;

    void Awake()
    {
        inv = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    void Start()
    {
        shootCooldownReset = shootCooldown;
    }

    void Update()
    {
        ammoText.text = "Ammo: \n" + bulletsInMag.ToString() + " / " + inv.ammo.ToString();

        if (Input.GetButtonDown("Fire1") && bulletsInMag != 0 && !reloadTimeActive && !shootIsOnCooldown) //Shoot Input
        {
            Shoot();
        }
        else if (Input.GetButtonDown("Fire1") && bulletsInMag == 0 && !reloadTimeActive && !shootIsOnCooldown) //Play empty clip sound
        {
            FindObjectOfType<AudioManager>().Play("EmptyClip");
        }


        if (Input.GetButtonDown("Reload") && inv.ammo > 0 && bulletsInMag != 6 && !reloadTimeActive) //Reload Input
        {
            reloadText.text = "Reloading...";
            reloadTimeActive = true;
            FindObjectOfType<AudioManager>().Play("Reloading");
            Invoke("Reload", 1.75f);
        }

        if (Input.GetButtonDown("Fire1") && !shootIsOnCooldown) //If the player presses fire key and shoot isnt on cooldown.
        {
            shootIsOnCooldown = true;
        }

        if (shootIsOnCooldown == true) //shoot cooldown timer so it's not spammable.
        {
            shootCooldown -= Time.deltaTime;

            if (shootCooldown <= 0)
            {
                shootIsOnCooldown = false;
                shootCooldown = shootCooldownReset;
            }
        }
    }

    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("GunShot");
        Rigidbody bulletInstance;
        bulletInstance = Instantiate(bulletPrefab, shootPosition.position, shootPosition.rotation) as Rigidbody;
        bulletInstance.velocity = shootPosition.forward * 50;
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
