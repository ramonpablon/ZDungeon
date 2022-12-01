using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : PlayerPhisics {

    #region Click Event

    [HideInInspector]
    public int noOfClicks = 0;
    [HideInInspector]
    public bool canChangeMaxInput = true;

    private float lastClickedTime = 0;
    private readonly float maxComboDelay = 1;
    private float time;


    int currentInput = 0;

    #endregion

    float parryTimer = 0;
    float parryDalayTime = 0;
    float maxParryDalay = 3;

    [HideInInspector]
    public float damage;

    [HideInInspector]
    public float animatorSpeedValue = 1; // usado pelo hitstop para pausar o personagem

    IEnumerator LockStun = null; // permite rotacionar o player em direção ao dano recebido
    IEnumerator Dash = null;

    void Start()
    {
        anim = GetComponent<Animator>();
        damage = -2;
    }

    // Update is called once per frame
    void Update () {
        damage = Mathf.Clamp(damage, -2, 5);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Knockout_end") && onground)
            CollisionBetweenPlayers();

        time += Time.deltaTime;

        if ((time - lastClickedTime > maxComboDelay) && !inCombat)
        {
            noOfClicks = 0;
        }

        #region Moviment

        moveX = Input.GetAxisRaw(playerPrefix + "_Horizontal");
        moveY = Input.GetAxisRaw(playerPrefix + "_Vertical");

        if (canMove && anim.GetBool("Jump") == false) // não permite virar enquanto prepara pro pulo
            anim.SetFloat("MoveX", Mathf.Abs(moveX * speed));

        if (Input.GetButtonDown(playerPrefix + "_ButtonA") && onground && canJump)
        {
            StartCoroutine(_Jump(1f));
            anim.SetTrigger("Jump");
        }

        if (Input.GetButtonDown(playerPrefix + "_ButtonLT") && canDash && !inCombat)
        {
            if (Dash != null)
                StopCoroutine(Dash);

            Dash = _Dash(0.7f);
            StartCoroutine(Dash);

            anim.SetTrigger("Dash");
        }

        #endregion

        #region Combat

            #region Block
        if (Input.GetButtonDown(playerPrefix + "_ButtonRT") && canBlock && !isDashing && !inCombat)
        {
            Blocking(true);
            anim.SetTrigger("Block");
        }
        else if (Input.GetButtonUp(playerPrefix + "_ButtonRT") && !isStun)
            Blocking(false);

        #endregion

            #region Parry
        if (Input.GetButtonDown(playerPrefix + "_ButtonRT") && canParry && !parryCollider.activeSelf)
        {
            parryCollider.SetActive(true);
            parryTimer = parryDalayTime = 0;
        }

        ParryActiveTime(.1f);

        if (AC != null)
        {
            if (!AC.gameObject.activeSelf)
            {
                getHit = false;
                triggerCollEnemy = false;
                AC = null;
            }
        }

        #endregion

        // ataque
        if (!isStun)
        {
            if (Input.GetButtonDown(playerPrefix + "_ButtonB") && !isDashing && !blocking)
            {

                if (onground)
                {
                    if (moveY > 0)
                        StartCoroutine(_Attack(3, "Up"));
                    if (moveY < 0)
                        StartCoroutine(_Attack(2, "Down"));
                    if ((moveX <= 0 || moveX >= 0) && moveY == 0)
                        StartCoroutine(_Attack(4, "Simple"));
                }
                else
                {
                    //air animations
                }
            }
        }

        #endregion

        #region Animations
        if (!isDashing)
            anim.SetBool("IsFalling", isFalling);
        anim.SetBool("Onground", onground);
        anim.SetBool("InCombat", inCombat);
        anim.SetBool("Blocking", blocking);
        anim.SetBool("IsDashing", isDashing);
        anim.SetBool("IsStun", isStun);

        anim.speed = animatorSpeedValue;

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            inCombat = true;
        else
            inCombat = false;

        #endregion
    }

    public void GetHit()
    {
        /// <summary>
        /// -------------
        /// +++++ PushDirection types: +++++
        /// * None: tipo de ataque padrão
        /// * Forward: tipo de ataque que lança o personagem para frente
        /// * Down: tipo de ataque que lança o personagem para baixo
        /// * Up: tipo de ataque que lança o personagem para cima
        /// * Hitstop: tipo de ataque loop, que dura até o trigger sair de contato
        /// -------------
        /// </summary>
        if(!parryCollider.activeSelf)
        {
            GetDamage();

            if (AC.pushDirection == PushDirection.None)
            {
                if (!AC.blockBreak)
                {
                    if (!blocking)
                    {
                        if (onground)
                        {
                            int hits = 2;
                            int hitNumber = Random.Range(0, hits);

                            anim.Play("Dano" + (hitNumber + 1).ToString());
                        }
                        else
                        {
                            anim.Play("knockoutBack_start", 0, 0.2f); // animação dano no ar
                            rb.velocity = Vector3.zero;
                            StartCoroutine(_Knockback(Vector3.up, 400, AC.stunTime));
                        }
                        StartCoroutine(_Knockback(hitDirection, AC.pushForce, AC.stunTime));
                    }
                    else
                    {
                        anim.SetTrigger("BlockGetHit");
                        StartCoroutine(_Knockback(hitDirection, 100, 1f));
                    }
                }
                else
                {
                    if (onground)
                    {
                        if (!blocking)
                            anim.Play("Dano3"); // mudar para a animação mais agressiva
                        else
                        {
                            anim.SetTrigger("BlockBreak");
                            blocking = false;
                            canBlock = false;
                        }

                        StartCoroutine(_Knockback(hitDirection, AC.pushForce, 0.8f));
                    }
                    else
                    {
                        anim.Play("knockoutBack_start", 0, 0.2f); // animaçao dano no ar
                        StartCoroutine(_Knockback(hitDirection, AC.pushForce, AC.stunTime));
                    }
                }
            }
            if (AC.pushDirection == PushDirection.Forward)
            {
                if (!blocking)
                {
                    anim.Play("knockoutBack_start");
                    StartCoroutine(_Knockback(hitDirection, AC.pushForce, AC.stunTime));
                }
                else
                {
                    anim.SetTrigger("BlockGetHit");
                    StartCoroutine(_Knockback(hitDirection, 100, .3f));
                }
            }
            if (AC.pushDirection == PushDirection.Down)
            {
                if (!blocking)
                {
                    anim.Play("knockoutBack_mid");
                    StartCoroutine(_Knockback(hitDirection, AC.pushForce, AC.stunTime));
                }
                else
                {
                    anim.SetTrigger("BlockGetHit");
                    StartCoroutine(_Knockback(hitDirection, 100, .3f));
                }
            }
            if (AC.pushDirection == PushDirection.Up)
            {
                if (!blocking)
                {
                    anim.Play("KnockoutUp");
                    StartCoroutine(_Knockback(hitDirection, AC.pushForce, AC.stunTime));
                }
                else
                {
                    anim.SetTrigger("BlockGetHit");
                    StartCoroutine(_Knockback(hitDirection, 100, .3f));
                }
            }
            if (AC.pushDirection == PushDirection.HitStop)
            {
                StartCoroutine(_HitStop(blocking, AC.timeBetweenHits));
            }
        }
    }
    public void GetDamage()
    {
        if(AC != null)
        {
            life = life - AC.damage;
            if(!blocking)
                damage = damage + 0.2f;
        }
    }

    private IEnumerator _Attack(int maxInput, string attack)
    {
        #region Get last Input Number
        // impede que trave os ataques em trocas rapidas de input
        int lastInput = currentInput;

        if (canChangeMaxInput)
            currentInput = maxInput;
        else
            currentInput = lastInput;
        #endregion

        #region Lock Moviment in Combat
        canChangeMaxInput = false;
        canJump = false;
        canMove = false;
        inCombat = true;
        #endregion

        lastClickedTime = Time.time;

        noOfClicks++;

        if (noOfClicks != 0 && noOfClicks <= maxInput)
            anim.SetTrigger(attack + noOfClicks.ToString());

        noOfClicks = Mathf.Clamp(noOfClicks, 0, currentInput);

        yield return new WaitUntil(() => inCombat == false);

        #region Unlock Moviment
        canChangeMaxInput = true;
        canMove = true;
        canJump = true;

        if (Input.GetButton(playerPrefix + "_ButtonRT"))
        {
            Blocking(true);
            anim.SetTrigger("Block");
        }

        #endregion
    }
    private IEnumerator _AttackInAir(int maxInput, string attack)
    {
        currentMovimentIsInAir = true;
        StartCoroutine(_Attack(maxInput, attack));
        yield return null;

        currentMovimentIsInAir = false;
    }

    private void Blocking(bool isBlock)
    {
        blocking = isBlock;
    }
    private void ParryActiveTime(float timer)
    {
        if (parryTimer < timer)
            parryTimer += Time.deltaTime;
        else
            parryCollider.SetActive(false);

        if (parryDalayTime < maxParryDalay)
        {
            parryDalayTime += Time.deltaTime;
            canParry = false;
        }
        else
        {
            parryCollider.SetActive(false);
            canParry = true;
        }
    }

    private IEnumerator _Jump(float time)
    {
        anim.applyRootMotion = false;
        rootMotion = false;
        canJump = false;
        collOfPlayers = true;
        yield return new WaitForSeconds(time);
        yield return new WaitUntil(() => onground == true);
        anim.applyRootMotion = true;
        collOfPlayers = false;
        rootMotion = true;
        canJump = true;
    }

    private IEnumerator _Dash(float time)
    {
        #region Turn Player
        if (otherPlayer.position.x < transform.position.x)
        {
            angle = -90;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (otherPlayer.position.x > transform.position.x)
        {
            angle = 90;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        #endregion

        isDashing = true;
        onground = false;
        currentMovimentIsInAir = true;
        anim.applyRootMotion = false;
        rootMotion = false;
        canJump = false;
        canMove = false;
        canDash = false;

        yield return new WaitForSeconds(time);

        currentMovimentIsInAir = false;
        anim.applyRootMotion = true;
        rootMotion = true;
        canJump = true;
        canMove = true;

        yield return new WaitUntil(() => onground);
        canDash = true;
    }
    private void EndDash()
    {
        startDash = false;
        isDashing = false;
    }

    public IEnumerator _Knockback(Vector3 knockDirection, float knockBackForce, float time)
    {
        // direção // força // tempo de stun sem mover os controles (padrão (.3f)
        if(LockStun != null)
            StopCoroutine(LockStun);

        LockStun = _LockStun(true, true, 0, time);
        StartCoroutine(LockStun);

        KnockbackForce(knockDirection, knockBackForce);
        yield return null;
    }
    private IEnumerator _LockStun(bool lockMovement, bool timed, float delayTime, float lockTime)
    {

        if (isDashing)
        {
            // pausa o dash se o player receber dano
            isDashing = false;
            rb.velocity = Vector3.zero;
        }

        yield return new WaitForSeconds(delayTime);

        if (lockMovement)
        {
            ///<summary>
            ///-------------
            ///* isStun: variavel main que reforça todos os itens abaixo
            ///* canJump: bloqueia o pulo
            ///* canMove: bloqueia o personagem girar
            ///* rootMotion: bloqueia o personagem se mover
            ///* applyRootMotion: root motion desativado
            ///* isDashing: por segurança, é uma segunda tentativa de bloquear o Dash
            ///* canblock: se o personagem não esta defendido, não permite defender
            ///* canParry: o jogador não pode dar parry se eles estiver recebendo dano, 
            ///somente quando houver uma pausa entre os ataques
            ///-------------
            /// </summary>

            isStun = true;
            canJump = false;
            canMove = false;
            rootMotion = false;
            anim.applyRootMotion = false;
            isDashing = false;
            canParry = false;
            if (!blocking)
                canBlock = false;

            #region TurnPlayer
            if (hitDirection == Vector3.right)
            {
                angle = -90;
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (hitDirection == Vector3.left)
            {
                angle = 90;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            #endregion

        }
        if (timed)
        {
            yield return new WaitForSeconds(lockTime);

            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Knockout_end"))
            {
                yield return new WaitUntil(() => isStun == false);
            }

            isStun = false;
            canJump = true;
            rootMotion = true;
            canMove = true;
            anim.applyRootMotion = true;
            canBlock = true;

            if (!Input.GetButton(playerPrefix + "_ButtonRT"))
                Blocking(false);

            if (parryDalayTime < maxParryDalay)
            {
                canParry = false;
            }
        }

        yield return null;
    }
    public void Unlock()
    {
        // executado pela animação "GetUp"
        isStun = false;
    }

    private IEnumerator _HitStop(bool inBlocking, float timeBetweenHits)
    {
        while (triggerCollEnemy)
        {
            if(!blocking)
            {
                if (onground)
                {
                    yield return new WaitForSeconds(timeBetweenHits);
                    int hits = 2;
                    int hitNumber = Random.Range(0, hits);

                    anim.Play("Dano" + (hitNumber + 1).ToString());
                }
                else
                {
                    anim.Play("knockoutBack_start", 0, 0.2f);
                    KnockbackForce(Vector3.up, 300);
                }
                StartCoroutine(_Knockback(hitDirection, 100, 0));
            }
            else
            {
                anim.SetTrigger("BlockGetHit");
                StartCoroutine(_Knockback(hitDirection, 100, .3f));
            }

            #region otherEnemy

            otherPlayer.gameObject.GetComponent<InputManager>().animatorSpeedValue = 0;
            yield return new WaitForSeconds(timeBetweenHits);
            otherPlayer.gameObject.GetComponent<InputManager>().animatorSpeedValue = 1;

            #endregion

            yield return null;
        }
    } // possivel exclusão
}
