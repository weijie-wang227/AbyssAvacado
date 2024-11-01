using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Animations;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform gunSlot; // Position where weapons are held
    [SerializeField] private Gun gun;
    
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
        }
    }

}
