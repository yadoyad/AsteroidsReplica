using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Настройки оружия")]
    [SerializeField] private float weaponCooldown = 0.2f;
    [Header("Вспомогательные элементы")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float lastShotTime = 0; 

    private void Update()
    {
        if(!PlayerManager.instance.isDead)
        {
            if(Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if(Time.time > lastShotTime + weaponCooldown)
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            lastShotTime = Time.time;
        }
    }
}
