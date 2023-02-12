using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class BulletHolder : MonoBehaviour       //contains the bullet damage and holds a player id to identify wich player shot the bullet.
{
    [SerializeField] 
    int Damage=50;

    public int Playerid;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,10f); //destroy bullet if it hits nothing
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>(); //on hitting enemy pass on player id and call damage om enemy.
            enemy.TakeDamage(Damage, Playerid);
            Destroy(gameObject);
        }
    }

}
