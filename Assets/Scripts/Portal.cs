using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;          
    [SerializeField] private float rotateSpeed;     
    private Vector2 newPosition;                   

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        bool playerHasWeapon = CheckPlayerWeapon();
        
        GetComponent<SpriteRenderer>().enabled = playerHasWeapon;
        GetComponent<CircleCollider2D>().enabled = playerHasWeapon;

        if (playerHasWeapon)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, newPosition) < 0.5f)
            {
                ChangePosition();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    private void ChangePosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        newPosition = new Vector2(x, y);
    }

    private bool CheckPlayerWeapon()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerWeapon playerWeapon = player.GetComponent<PlayerWeapon>();
            return playerWeapon != null;
        }
        return false;
    }
}
