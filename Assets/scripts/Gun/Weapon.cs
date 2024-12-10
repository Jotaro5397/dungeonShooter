using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : MonoBehaviour
{   
    private Animator anim;
    private AudioSource _AudioSource;

    public float range = 100f;  // Maximum range of the Weapon
    public int bulletsPerMag = 30; // Bullets per each magazine
    public int bulletsLeft = 200;   // total bullets we have

    public int currentBullets; // The current bullets in out magazine
                                // shooting mode for the weapon 
    public enum shootMode { Auto, Semi }
    public shootMode shootingMode;
    public bool canUse = true;

    public float fireRate = 0.1f;

    [Header("Weapon Config")]
    public Transform shootPoint;
    public GameObject hitParticles;
    public GameObject bulletImpact;
    


    public Text ammoText;
    
    public ParticleSystem cartridgeEffect;
    public ParticleSystem muzzleFlash; // muzzleFlash

    //Sound
    public AudioClip shootSound;
    public AudioClip emptySound;
    public AudioClip reloadSound;

    float fireTimer;
    public float damage = 20f;


    private bool isReloading;
    private bool isAiming;
    private bool shootInput;

    private Vector3 originalPosition;
    public Vector3 aimPosition;
    public float aodSpeed = 8f;

    void OnEnabled()
    {
        // Update when active state is changed
        UpdateAmmoText(); // updates ammo text
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _AudioSource = GetComponent<AudioSource>();

        currentBullets = bulletsPerMag;
        originalPosition = transform.localPosition;

        UpdateAmmoText(); // updates ammo text
    }

    // Update is called once per frame
    void Update()
    {

        switch (shootingMode)
        {
            case shootMode.Auto:
                shootInput = Input.GetButton("Fire1");
                break;
            case shootMode.Semi:
                shootInput = Input.GetButtonDown("Fire1");
                break;
        }

        // Check if player is trying to shoot
        if (shootInput)
        {
            // Check if bullets in the magazine are greater than 0
            if (currentBullets > 0)
            {
                Fire(); // Execute the fire function if we press/hold the left mouse button
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                DoReload();
                
            }

            else
            {
                // Play the empty sound when there are no bullets left
                PlayEmptySound();
            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(currentBullets < bulletsPerMag && bulletsLeft > 0)
            DoReload();
        }
        

        if(fireTimer < fireRate)
            fireTimer += Time.deltaTime;   // Add into time counter

        AimDownSight();
        
        
    }

    void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        isReloading = info.IsName("Reload");
        if (info.IsName("Fire")) anim.SetBool("Fire", false);
        anim.SetBool("Aim", isAiming);
    }

    private void AimDownSight()
    {
        if(Input.GetButton("Fire2") && !isReloading)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * aodSpeed );
            isAiming = true;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * aodSpeed);
            isAiming = false;
        }
    }

    private void Fire()
    {
        if(fireTimer < fireRate || currentBullets <= 0 || isReloading) // if firerate or currentBullets on 0 return
        return;
        
        RaycastHit hit; 

        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + " found ");

            // spawn particles by using Instantiate  as well as alligning the particles to show normals towards the player
            GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            hitParticleEffect.transform.SetParent(hit.transform);
            GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

            if (hit.transform.CompareTag("Enemy"))
            {
                HealthController enemyHealth = hit.transform.GetComponent<HealthController>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }

            //  Particle duration
            Destroy(hitParticleEffect, 1f);
            Destroy(bulletHole, 2f);

            //if (hit.transform.GetComponent<HealthController>())
            //{
            //    hit.transform.GetComponent<HealthController>().ApplyDamage(damage);
            //}

        }

        anim.SetBool("Fire", true);

        cartridgeEffect.Play();
        muzzleFlash.Play();  // show the muzzle flash
        PlayShootSound();  // Play the shoot soud effect

        anim.CrossFadeInFixedTime("Fire", 0.1f);
        currentBullets --;  // deducts bullets
        if (canUse)
        {
            UpdateAmmoText(); // updates ammo text
        }


        fireTimer = 0.0f; // Reset fire timer
    }

    public void Reload()
    {
        if(bulletsLeft <= 0) return;

        int bulletsToLoad = bulletsPerMag - currentBullets;
        //                          IF                      THEN      1ST   ELSE  2ND
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;

        UpdateAmmoText(); // updates ammo text
    }

    private void DoReload()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (isReloading) return;

        anim.CrossFadeInFixedTime("Reload", 0.01f);
        PlayReloadSound();
    }

    private void PlayShootSound()
    {
        _AudioSource.PlayOneShot(shootSound);
        //_AudioSource.clip = shootSound;
        //_AudioSource.Play();
    }

    private void PlayEmptySound()
    {
        _AudioSource.PlayOneShot(emptySound);
    }

    private void PlayReloadSound()
    {
        _AudioSource.PlayOneShot(reloadSound);
    }

    private void UpdateAmmoText()
    {
        //                  30 / 120 bullets              
        ammoText.text = currentBullets + " / " + bulletsLeft;

    }

}
