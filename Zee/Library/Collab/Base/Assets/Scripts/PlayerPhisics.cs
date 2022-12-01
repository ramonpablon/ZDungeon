using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhisics : MonoBehaviour {

    protected Animator anim;
    protected Rigidbody rb;

    [Header("----PlayerConfig----")]
    public Transform otherPlayerPos;
    public string playerPrefix;
    protected AttackCollider AC = null;
    private InputManager IM;


    /// <summary>
    /// -------------
    /// * speed: controlada pelo script Turn.cs, seu valor não tem muita importancia
    /// ja que a velocidade em si é controlada pela animação
    /// 
    /// * angle: rotação atual do player, defide o lado que ele esta virado
    /// 90 = right / -90 = left
    /// moveX e muveY: recebe o valor do stick do controle
    /// 
    /// * hitDirection: recebe de que direção esta vindo o hit que ele ta tomando
    /// -------------
    /// </summary>
    [Header("----Moviment----")]
    public float jumpForce;
    [HideInInspector]
    public float speed = 3;
    public float dashSpeed = 400;
    public float angle = 90;
    protected float moveX, moveY;
    protected Vector3 hitDirection = Vector3.zero;
    public static bool collOfPlayers = false;


    /// <summary>
    /// -------------
    /// * offset: corretor de posição do sphereRadius no Editor
    /// -------------
    /// </summary>
    [Space]
    [Header("----Ground Detection----")]
    public bool onground;
    [Space]
    public Vector3 offset;
    [Range(0.0f, 1.0f)]
    public float sphereRadius;
    public LayerMask whatIsGround;


    /// <summary>
    /// -------------
    /// * canMove: desativa a rotação do player
    /// 
    /// * rootMotion: desativa a movimentação pelo eixo X
    /// set to FALSE pra usar a fisica no personagem
    /// 
    /// * currentMovimentIsInAir: evita que animaçõe de ataque aereo sejam
    /// interrompidas se o jogador estiver baixo demais (em teoria)
    /// -------------
    /// </summary>
    [Space]
    [Header("----Animations Constraints----")]
    public bool canMove = true;
    public bool canDash = true;
    public bool canJump = true;
    protected bool isFalling = false;
    public bool isDashing = false;
    public bool startDash = false;
    protected bool currentMovimentIsInAir = false;
    protected bool rootMotion = true;
    protected bool isKnockback = false;

    /// <summary>
    /// -------------
    /// * inCombat: diz se alguma animação de combate esta sendo executada
    /// serve pra impedir que o jogador se movimente enquando ataca
    /// 
    /// * triggerCollEnemy: o trigger esta colidindo com alguem chamado inimigo?
    /// 
    /// * isStun: é ativada quando o personagem toma um knockout, e só volta a ser falsa quando
    /// a animação de GetUp é reproduzida
    /// Tamebm é ativa na chamada de knockback
    /// 
    /// * getHit: controlado pelo script AttackCollider, chama a função GetHit que diz o tipo de comportamento
    /// para cada porrada recebida
    /// -------------
    /// </summary>
    [Space]
    [Header("----Combat----")]
    public GameObject parryCollider;
    [Space]
    public bool inCombat = false;
    public bool blocking = false;
    protected bool canBlock = true;
    protected bool canParry = true;
    protected bool triggerCollEnemy = false;
    protected bool isStun = false;
    [HideInInspector]
    public bool getHit = false;

    void Awake() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        IM = GetComponent<InputManager>();
    }

    void FixedUpdate()
    {
        if (!currentMovimentIsInAir)
            onground = Physics.CheckSphere(transform.position + offset, sphereRadius, whatIsGround);

        #region Horizontal Moviment

        //trava o personagem no eixo Z
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (rootMotion)
            rb.velocity = new Vector3(moveX * speed, rb.velocity.y);
        #endregion

        #region Jump balancing

        // balanço do pulo pra se parecer com o do Naruto
        if (rb.velocity.y < 0)
        {
            isFalling = true;
            rb.AddForce(-Vector3.up * 5.8f, ForceMode.Acceleration);
        }
        if (rb.velocity.y > 1)
        {
            rb.AddForce(-Vector3.up * 6.8f, ForceMode.Acceleration);
        }
        if (!onground && isFalling)
            rb.AddForce((Vector3.right * moveX) * speed, ForceMode.Acceleration);
        else
            isFalling = false;

        #endregion

        if (canMove)
            RotationOfPlayer();
        else // força a rotação do player para olhar para um dos lados enquanto ataca
            transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    void Jump()
    {
        // animação de pulo chama o void
        rb.AddForce(new Vector3(moveX / 2, 1.1f, 0) * jumpForce, ForceMode.Impulse);
    }

    void RotationOfPlayer()
    {
        if (rb.velocity.x < -0.5f && onground)
        {
            angle = -90;
        }
        else if (rb.velocity.x > 0.5f && onground)
        {
            angle = 90;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), 10 * Time.deltaTime);
    }

    public void KnockbackForce(Vector3 knockDirection, float knockBackForce)
    {
        rb.AddForce(knockDirection * knockBackForce, ForceMode.Impulse);
    }

    void DashForce()
    {
        // animaçaõ de dash chama o void
        startDash = true;
        rb.AddForce((otherPlayerPos.position - transform.position).normalized * dashSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision coll)
    {
        //quando esta em dash e colide com o inimigo ocorre uma repulsão
        if(coll.gameObject.tag.Equals("Player") && startDash)
        {
            if (otherPlayerPos.position.x < transform.position.x)
            {
                hitDirection = Vector3.right;
            }
            else if (otherPlayerPos.position.x > transform.position.x)
            {
                hitDirection = Vector3.left;
            }
            KnockbackForce(Vector3.up, 200);
            StartCoroutine(IM._Knockback(hitDirection, 500, 0.5f));
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag.Equals("AttackCollider"))
        {
            triggerCollEnemy = true;

            AC = coll.GetComponent<AttackCollider>();

            if (AC.pushDirection == PushDirection.None || AC.pushDirection == PushDirection.Forward || AC.pushDirection == PushDirection.HitStop)
            {
                if (otherPlayerPos.position.x < transform.position.x)
                {
                    hitDirection = Vector3.right;
                }
                else if (otherPlayerPos.position.x > transform.position.x)
                {
                    hitDirection = Vector3.left;
                }
            }
            else if (AC.pushDirection == PushDirection.Up)
            {
                hitDirection = Vector3.up;
            }
            else if (AC.pushDirection == PushDirection.Down)
            {
                hitDirection = Vector3.down;
            }

            if(getHit)
            {
                // toma hit ao atingir um AttackCollider inimigo
                IM.GetHit();
                getHit = false;
                AC = null;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Equals("AttackCollider"))
        {
            getHit = false;
            triggerCollEnemy = false;
        }
    }

    public void CollisionBetweenPlayers()
    {
        // repulsão dos players 
        if (Mathf.Abs(rb.velocity.x) < 0.1f && Mathf.Abs(otherPlayerPos.GetComponent<PlayerPhisics>().rb.velocity.x) < 0.1f)
        {
            if (Vector3.Distance(transform.position, otherPlayerPos.position) < 1.2f)
            {
                if (otherPlayerPos.position.x < transform.position.x)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0.5f, 0, 0), Time.deltaTime);
                }
                else if (otherPlayerPos.position.x > transform.position.x)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0.5f, 0, 0), Time.deltaTime);
                }
            }
        }
        // atrai o inimigo para um ponto a frente do personagem enquanto ele executa um ataque
        if(AC != null)
        {
            if (Vector3.Distance(transform.position, otherPlayerPos.position) < 1.2f)
            {
                if (otherPlayerPos.position.x < transform.position.x)
                {
                    transform.position = Vector3.Lerp(transform.position, otherPlayerPos.transform.position + new Vector3(10f, 0, 0), Time.deltaTime);
                }
                else if (otherPlayerPos.position.x > transform.position.x)
                {
                    transform.position = Vector3.Lerp(transform.position, otherPlayerPos.transform.position - new Vector3(10f, 0, 0), Time.deltaTime);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, sphereRadius);
    }
}
