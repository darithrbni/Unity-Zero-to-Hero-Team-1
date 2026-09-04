using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject giantBullet;

    public float fireRate = 1f;
    public float nextFireTime = 0f;

    public float rapidFireRate = 0.1f;
    public float defaultFireRate;

    public bool isSpreadShootActive = false;
    public bool isGiantBulletActive = false;

    public PowerUpUI powerUpUIManager; 
    
    private Coroutine currentPowerUpRoutine;

    // Start is called before the first frame update
    void Start()
    {
        defaultFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            // Instantiate(bullet, transform.position, Quaternion.Euler(90, 0, 0));
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (isSpreadShootActive)
        {
            float[] spreadAngles = {-20f, 0f, 20f};

            foreach (float angle in spreadAngles)
            {
                Quaternion baseRotation = Quaternion.Euler(90, 0, 0);
                Quaternion spreadRotation = Quaternion.Euler(0, angle, 0) * baseRotation;

                Instantiate(bullet, transform.position, spreadRotation);
            }
        }
        else if (isGiantBulletActive)
        {
            Instantiate(giantBullet, transform.position, Quaternion.Euler(90, 0, 0));
        }
        else
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(90, 0, 0));
        }
    }

    private void ResetAllPowerUps()
    {
        isSpreadShootActive = false;
        fireRate = defaultFireRate;
        isGiantBulletActive = false;
    }

    // Power Up Spread Shot Activation
    public void ActivateSpreadShoot(float duration)
    {
        if (currentPowerUpRoutine != null)
            StopCoroutine(currentPowerUpRoutine);

        ResetAllPowerUps();

        if (powerUpUIManager != null)
            powerUpUIManager.ActivateUI(PowerUpAnimation.JenisPowerUP.SpreadShot, duration);

        currentPowerUpRoutine = StartCoroutine(SpreadShortTimer(duration));
    }

    private IEnumerator SpreadShortTimer(float duration)
    {
        isSpreadShootActive = true;
        yield return new WaitForSeconds(duration);
        ResetAllPowerUps();
    }

    // Power Up Rapid Fire Activation
    public void ActivateRapidFire(float duration)
    {
        if (currentPowerUpRoutine != null)
            StopCoroutine(currentPowerUpRoutine);

        ResetAllPowerUps();

        if (powerUpUIManager != null)
            powerUpUIManager.ActivateUI(PowerUpAnimation.JenisPowerUP.RapidFire, duration);
    
        currentPowerUpRoutine = StartCoroutine(RapidFireTimer(duration));
    }

    private IEnumerator RapidFireTimer(float duration)
    {
        fireRate = rapidFireRate;
        yield return new WaitForSeconds(duration);
        ResetAllPowerUps();
    }

    public void ActivateGiantBullet(float duration)
    {
        if (currentPowerUpRoutine != null)
            StopCoroutine(currentPowerUpRoutine);

        ResetAllPowerUps();

        if (powerUpUIManager != null)
            powerUpUIManager.ActivateUI(PowerUpAnimation.JenisPowerUP.GiantBullet, duration);
        
        currentPowerUpRoutine = StartCoroutine(GiantBulletTimer(duration));
    }

    private IEnumerator GiantBulletTimer(float duration)
    {
        isGiantBulletActive = true;
        yield return new WaitForSeconds(duration);
        ResetAllPowerUps();

    }
}
