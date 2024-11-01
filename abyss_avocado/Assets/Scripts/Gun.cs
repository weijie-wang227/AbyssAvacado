using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float firePower = 100f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float cooldown = 0.1f;

    private bool canFire = true;
    
    public void Fire()
    {
        if (!canFire) return;

        anim.SetTrigger("fire");
        Shoot();
        StartCoroutine(Cooldown());
        
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        var bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePower * firePoint.right, ForceMode2D.Impulse);
        bullet.damage = damage;
    }

    private IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(cooldown);
        canFire = true;
    }
}