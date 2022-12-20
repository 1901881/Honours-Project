using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Weapon weapon;
    public Camera camera2;

    public int speed = 10;
    public int health = 10;
    public float DeathTime = 5;
    private float Timer = 0;

    private Rigidbody2D playerBody;//References rigidbody applied to prefab
    private Vector2 velocity; //x,y movement
    private Vector2 inputMovement;
    private Vector3 mousePosInWorld;
    Vector3 aimDirection;

    private bool canDash = true;
    private bool isDashing;
    private const float dashingPower = 15.2f;
    private const float dashingTime = 0.3f;
    private const float dashingCooldown = 1f;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private TrailRenderer tr;

    //Changing Player Color
    private SpriteRenderer SpriteRend;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(speed, speed);//sets velocity x and y
        playerBody = GetComponent<Rigidbody2D>();//sets rigid body to variable

        SpriteRend = GetComponent<SpriteRenderer>();
        originalColor = SpriteRend.color;
    }

    // Update is called once per frame
    void Update()
    {
        //Converts mouse position into world position
        var mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePos);
        aimDirection = new Vector2(mousePosInWorld.x, mousePosInWorld.y) - playerBody.position;

        //Aim();

        if (isDashing)
        {
            return;
        }

        //sets variable to user input, no input = 0
        inputMovement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }


        if (health <= 0)
        {
            QuitGame();
            //Destroy(gameObject);
        }

        
    }

    private void FixedUpdate()//update that runs on a fixed cycle
    {
        if (isDashing)
        {
            return;
        }

        //takes the key being pressed * player speed * delta time(to make the movement smooth)
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = playerBody.position + delta;
        playerBody.MovePosition(newPosition);

        aimDirection = new Vector2(mousePosInWorld.x, mousePosInWorld.y) - playerBody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90.0f;
        playerBody.rotation = aimAngle;

        DeathTimer();
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
            StartCoroutine(Hit());
        }

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
     
        playerBody.AddForce(aimDirection.normalized * dashingPower, ForceMode2D.Impulse);

        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;

        playerBody.velocity = Vector3.zero;
        playerBody.angularVelocity = 0f;

        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator Hit()
    {
        SpriteRend.color = Color.white;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.08f);
        Time.timeScale = 1;
        SpriteRend.color = originalColor;
        health--;

    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif
            Application.Quit();
    }

    public void DeathTimer()
    {
        Timer += Time.deltaTime;
        if(Timer >= DeathTime)
        {
            Timer = 0;
            QuitGame();
        }
    }
}