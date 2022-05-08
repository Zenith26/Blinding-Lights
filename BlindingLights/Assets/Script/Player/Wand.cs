using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public Transform bulletspawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Timedelay());
        }
    }

    IEnumerator Timedelay()
    {
        yield return new WaitForSeconds(1);

        var bullet = Instantiate(bulletPrefab, bulletspawnPoint.position, bulletspawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletspawnPoint.forward * bulletSpeed;
    }
}
