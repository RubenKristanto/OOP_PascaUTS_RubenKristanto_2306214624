using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Weapons currentWeapon;

    public void SwapWeapon(Weapons newWeapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = newWeapon;
        currentWeapon.transform.parent = transform;
        currentWeapon.transform.localPosition = Vector3.zero; 
        currentWeapon.gameObject.SetActive(true); 
    }
}

