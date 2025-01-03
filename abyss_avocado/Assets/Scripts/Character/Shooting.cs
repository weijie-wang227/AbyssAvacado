using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform gunSlot; // Position where weapons are held
    [SerializeField] private Gun gun;
    Rigidbody2D rgd;
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Aim weapon in direction of cursor
        var aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var aimDir = (aimPos - gunSlot.position).normalized;
        float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        gunSlot.eulerAngles = new Vector3(0, 0, aimAngle);

        if (Input.GetButtonDown("Fire1"))
        {
            gun.Fire();
            rgd.AddForce(-aimDir * 20f, ForceMode2D.Impulse);
        }
    }

}
