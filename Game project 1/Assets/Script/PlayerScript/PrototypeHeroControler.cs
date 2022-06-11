using UnityEngine;
using System.Collections;

public class PrototypeHeroControler : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] float      m_maxSpeed = 4.5f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] Texture2D  defaultCursor;
    [Header("Effects")]

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Prototype    m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_moving = false;
    private bool                m_canMove = true;
    public bool                 m_canShoot {get; private set; }
    private bool                m_Talk = false;
    private int                 m_facingDirection = 1;
    private float               m_disableMovementTimer = 0.0f;

    // Use this for initialization
    void Start ()
    {
        m_canShoot = true;
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Make player go forward
    public void ForceForceward(float speed){
        m_body2d.velocity = transform.right * speed;
    }

    // Funtion that make player pass through cummon collider
    public void PassThrough()
    {
        // Ignore collision with enemies
        Physics2D.IgnoreLayerCollision(3,6,true);
    }

    // Funtion that stop the effect of the last Function
    public void DontPassThrough()
    {
        // Ignore collision with enemies
        Physics2D.IgnoreLayerCollision(3,6,false);
    }

    // Function to avoid character movement
    public void CantMove (){
        m_canMove = !m_canMove;
    }

    // Function that disable player shooting
    public void CantShoot(){
        m_canShoot =! m_canShoot;
    }

    // Function to flip character 
    public void Flip(){
        if (m_facingDirection == -1)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            m_facingDirection = 1;
        }
            
        else if (m_facingDirection == 1)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            m_facingDirection = -1;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Disable mouvement if Character Speaking
        if(DialogueManager.GetInstance().dialogueIsPlaying){
            m_canMove = false;
            m_canShoot = false;
            m_Talk = true;
        }
        else if (!m_canMove && m_Talk)
        {
            m_Talk = false;
            m_canMove = true;
            m_canShoot = true;
        }

        // Decrease timer that disables input movement. Used when attacking
        m_disableMovementTimer -= Time.deltaTime;
        if(m_canMove){
            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            // -- Handle input and movement --
            float inputX = 0.0f;


            if (m_disableMovementTimer < 0.0f)
                inputX = Input.GetAxis("Horizontal");

            // GetAxisRaw returns either -1, 0 or 1
            float inputRaw = Input.GetAxisRaw("Horizontal");
            // Check if current move input is larger than 0 and the move direction is equal to the characters facing direction

            if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
                m_moving = true;

            else
                m_moving = false;
        
            // SlowDownSpeed helps decelerate the characters when stopping
            float SlowDownSpeed = m_moving ? 1.0f : 0.5f;
            // Set movement
            m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);

            // Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

            // -- Handle Animations --
            //Jump
            if (Input.GetButtonDown("Jump") && m_grounded && m_disableMovementTimer < 0.0f)
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }

            // Roll
            if (Input.GetButtonDown("Roll") && m_grounded){
                m_animator.SetTrigger("Rolling");
            }
            m_animator.SetFloat("MoveSpeed", Mathf.Abs(inputX));
        }

        
    }
}
