using System.Collections;
using UnityEngine;

public class WeaponPickup1 : MonoBehaviour
{
    [SerializeField] private Weapons weaponholder; 
    private Weapons weapon; 
    private CircleCollider2D pickupCollider; 
    private SpriteRenderer pickupRenderer; 

    public float respawnTime = 5f; 

    void Awake()
    {
        pickupCollider = GetComponent<CircleCollider2D>();
        pickupRenderer = GetComponent<SpriteRenderer>();

        weapon = Instantiate(weaponholder);
        weapon.gameObject.SetActive(false); 
        TurnPickupVisual(true); 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerWeapon playerWeapon = collision.GetComponent<PlayerWeapon>();

            if (playerWeapon != null)
            {
                Weapons currentWeapon = collision.GetComponentInChildren<Weapons>();
                if (currentWeapon != null)
                {
                    TurnVisual(false, currentWeapon);
                }

                weapon.gameObject.SetActive(true);
                weapon.transform.parent = collision.transform;
                weapon.transform.localPosition = Vector3.zero;
                playerWeapon.SwapWeapon(weapon);

                TurnPickupVisual(false);
                StartCoroutine(RespawnPickup());
            }
        }
    }

    private IEnumerator RespawnPickup()
    {
        yield return new WaitForSeconds(respawnTime);

        weapon = Instantiate(weaponholder, transform.position, Quaternion.identity);
        weapon.gameObject.SetActive(false); 
        TurnPickupVisual(true); 
    }

    private void TurnPickupVisual(bool visible)
    {
        pickupRenderer.enabled = visible;
        pickupCollider.enabled = visible;
    }

    void TurnVisual(bool on)
    {
        if (weapon == null) return;

        foreach (var renderer in weapon.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = on;
        }
    }

    void TurnVisual(bool on, Weapons weaponToDestroy)
    {
        if (weaponToDestroy == null) return;

        foreach (Renderer renderer in weaponToDestroy.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = on;
        }

        if (!on)
        {
            Destroy(weaponToDestroy.gameObject);
        }
    }
}
