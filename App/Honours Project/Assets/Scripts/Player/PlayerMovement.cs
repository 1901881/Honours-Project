using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Weapon weapon;

    public int speed = 10;

    private Rigidbody2D playerBody;//References rigidbody applied to prefab
    private Vector2 velocity; //x,y movement
    private Vector2 inputMovement;
    private Vector2 mousePosition;

    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(speed, speed);//sets velocity x and y
        playerBody = GetComponent<Rigidbody2D>();//sets rigid body to variable
    }

    // Update is called once per frame
    void Update()
    {
        //sets variable to user input, no input = 0
        inputMovement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()//update that runs on a fixed cycle
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;//takes the key being pressed * player speed * delta time(to make the movement smooth)
        Vector2 newPosition = playerBody.position + delta;
        playerBody.MovePosition(newPosition);

        Vector2 aimDirection = mousePosition - playerBody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90.0f;
        playerBody.rotation = aimAngle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bullet1Up")
        {
            weapon.bulletNum++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            health--;
        }

    }
}
