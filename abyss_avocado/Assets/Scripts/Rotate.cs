using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject bullet;
    public Boolean canFire;
    private float timer = 0f;
    private float delay = 0.5f;
    public Transform bulletTransform;
    private Vector3 mousePos;
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetMouseButton(0) && canFire)
        {
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            canFire = false;
        }
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                canFire = true;
                timer = 0;
            }
        }
    }

}
