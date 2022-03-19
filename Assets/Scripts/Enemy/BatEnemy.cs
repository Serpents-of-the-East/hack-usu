using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : WalkingEnemy
{
    public GameObject player;
    public float flyingSpeed;
    public float maxDistanceAway;
    public float shotCooldown;
    public GameObject attackPrefab;
    private bool canShoot = true;
    public float detectionRange = 20f;

    protected override void Update()
    {
        float distance = (player.transform.position - gameObject.transform.position).magnitude;
        if (distance > maxDistanceAway && distance < detectionRange)
        {
            gameObject.transform.Translate((player.transform.position - gameObject.transform.position) * Time.deltaTime * flyingSpeed);
            if (canShoot)
            {
                Shoot();
                StartCoroutine(ShotCooldown());
            }
        }

    }

    IEnumerator ShotCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
    }

    public void Shoot()
    {
        GameObject attack = Instantiate(attackPrefab);
        attack.transform.position = transform.position;

        Vector2 lookDir = transform.position - player.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;

        attack.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
