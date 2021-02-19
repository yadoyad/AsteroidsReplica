using System.Collections;
using UnityEngine;

public class UFOWeapon : MonoBehaviour
{
    [Header("Настройки оружия")]
    [SerializeField] private float weaponCooldown = 0.2f;
    [SerializeField] private float firepPointOffset = .4f;
    [Header("Вспомогательные элементы")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float lastShotTime = 0; 
    private Transform playerPosition;

    private void Start() 
    {
        StartCoroutine(ShootingRoutine());
    }

    IEnumerator ShootingRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        Shoot();
        StartCoroutine(ShootingRoutine());
    }

    private void Shoot()
    {
        LookAtPlayer();
        if(Time.time > lastShotTime + weaponCooldown)
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            lastShotTime = Time.time;
        }
    }

    private void LookAtPlayer()
    {
        var myPlayer = GameObject.FindGameObjectWithTag("Player");
        Ray ray = new Ray(transform.localPosition, myPlayer.transform.localPosition - transform.localPosition);
        Vector3 desiredPosition = ray.origin + ray.direction * firepPointOffset;

        firePoint.position = desiredPosition;
        var angle = Mathf.Atan2(myPlayer.transform.position.x - firePoint.position.x, myPlayer.transform.position.y - firePoint.position.y) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }
}
