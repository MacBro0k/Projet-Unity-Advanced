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
    private weaponScript        m_weapon;

    public Transform            wallCheck;
    public Transform            LedgeCheck;

    public Vector2              ledgePosBot;
    public Vector2              ledgePos1;
    public Vector2              ledgePos2;

    private bool                m_grounded = false;
    private bool                m_moving = false;
    private bool                m_canMove = true;
    public bool                 m_canShoot {get; private set; }
    private bool                m_Talk = false;
    private bool                m_isTouchingWallR;
    private bool                m_isTouchingWallL;
    private bool                m_isTouchingLedge;
    private bool                m_OnWallSlide;
    private bool                m_TouchingLedge;
    private bool                canClimLedge = false;
    private bool                m_LedgeDetected;

    private int                 m_facingDirection = 1;
    private float               m_disableMovementTimer = 0.0f;

    public float ledgeClimbXoffset1 = 0f;
    public float ledgeClimbYoffset1 = 0f;
    public float ledgeClimbXoffset2 = 0f;
    public float ledgeClimbYoffset2 = 0f;

    public LayerMask            LayerGround;
    public float                wallCheckDistance;
    public float                wallSlidingSpeed;
    public float                wallJumpForce;


    // Use this for initialization
    void Start ()
    {
        m_canShoot = true;
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
        m_weapon = transform.Find("ArmPivot").GetComponent<weaponScript>();
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    
    public void OnWallSlide(){
        m_animator.SetBool("Wallslide", false);
        m_OnWallSlide = true;
    }

    public void FinishLedgeClimb(){
        canClimLedge = false;
        transform.position = ledgePos2;
        m_canMove = true;
        m_LedgeDetected = false;
        m_animator.SetBool("LedgeGrab",canClimLedge);
    }

    // Function that only flip the sprite of the character
    public void FlipSprite(){
        if(m_isTouchingWallL)
            GetComponent<SpriteRenderer>().flipX = true;
        else{
            GetComponent<SpriteRenderer>().flipX = false;
        }
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

    // Function that called when player no longer Roll
    public void NoLongerRolling(){
        m_animator.SetBool("IsRolling",false);
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
    public void Update ()
    {

        m_isTouchingLedge = Physics2D.Raycast(LedgeCheck.position, transform.right, wallCheckDistance, LayerGround);
        
        m_isTouchingWallR = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, LayerGround);
        m_isTouchingWallL = Physics2D.Raycast(wallCheck.position, -transform.right, wallCheckDistance, LayerGround);

        if(m_isTouchingWallR && !m_isTouchingLedge && !m_LedgeDetected)
        {
            m_LedgeDetected = true;

            ledgePosBot = wallCheck.position;
        }

        if(m_LedgeDetected && !canClimLedge){
            canClimLedge = true;

            if (m_facingDirection > 0){
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXoffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYoffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXoffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYoffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXoffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYoffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXoffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYoffset2);
            }

            m_canMove = false;

            m_animator.SetBool("LedgeGrab",canClimLedge);
        }

        if(canClimLedge){
            transform.position = ledgePos1;
        }

        if(!m_isTouchingWallR && !m_isTouchingWallL && m_OnWallSlide)
            m_OnWallSlide=false;

        if(!m_OnWallSlide)
            m_animator.SetBool("Wallslide", (m_isTouchingWallR||m_isTouchingWallL) && !m_grounded && !canClimLedge);
        m_animator.SetBool("WallslideR", m_isTouchingWallR && !m_grounded && !canClimLedge);
        m_animator.SetBool("WallslideL", m_isTouchingWallL && !m_grounded && !canClimLedge);

        if((m_isTouchingWallR && !m_grounded) || canClimLedge){
            m_weapon.Disable();
        }
        if ((m_isTouchingWallL && !m_grounded)){
            m_weapon.Enable();
        }
        if((!m_isTouchingWallL && !m_isTouchingWallR && !canClimLedge)){
            m_weapon.Enable();
        }

        //Wall Jump
        if (Input.GetButtonDown("Jump") && (m_OnWallSlide)){
            m_OnWallSlide = false;
            //Vector2 forceToAdd 
            m_body2d.AddForce( new Vector2(wallJumpForce, wallJumpForce), ForceMode2D.Impulse);
            //m_body2d.AddForce(forceToAdd, ForceMode2D.Impulse);
        }

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

        if (Cutscene.GetInstance().CutscenePlaying)
        {
            m_canMove = false;
            m_canShoot = false;
            m_Talk = true;
        }
        else if (!m_canMove && m_Talk)
        {
            m_canMove = true;
            m_canShoot = true;
        }

        // Decrease timer that disables input movement. Used when attacking
        m_disableMovementTimer -= Time.deltaTime;

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
        if(m_canMove){
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

            if(m_OnWallSlide && !m_grounded && m_body2d.velocity.y < 0){
                if(m_body2d.velocity.y < -wallSlidingSpeed){
                    m_body2d.velocity = new Vector2(m_body2d.velocity.x, -wallSlidingSpeed);
                }
            }

            // -- Handle Animations --
            //Jump
            if (Input.GetButtonDown("Jump") && m_grounded && m_disableMovementTimer < 0.0f)
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }

            // Roll
            if (Input.GetButtonDown("Roll") && m_grounded){
                m_animator.SetBool("IsRolling",true);
            }
            m_animator.SetFloat("MoveSpeed", Mathf.Abs(inputX));         
        }
    }
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Gizmos.DrawLine(LedgeCheck.position, new Vector3(LedgeCheck.position.x + wallCheckDistance, LedgeCheck.position.y));
    }   
    #endif
}
