using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables

    private Vector3 direction;
    private Movement movement;
    private PlayerAnimator animator;
    public SceneManagement scene;
    [SerializeField] private PlayerHUD playerHUD;

    #region Input variables
    // #. Keyboard
    private float hAxis;
    private float vAxis;

    // #. Joypad(KeyPad)
    //[HideInInspector]
    public bool[] keyControl = new bool[9];
    //[HideInInspector]
    public bool isControl;
    public Image inputImg;
    [HideInInspector]
    public bool isButtonRoll;
    public float rollDelay = 1.0f;
    public float rollCoolTime = 1.0f;
    public Image rollImg;
    [HideInInspector]
    public bool isButtonFire;
    Vector3 defaultPosition;
    #endregion

    #region Weapon variables
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    float fireDelay;
    bool isFireReady;
    public int defaultAmmo; //추가
    public int ammo;//수정

    [Header("Grenade")]
    public GameObject grenadeObj;
    public Transform grenadePos;
    public bool isGrenadesEnable = false; //추가 : 수류탄 공격이 가능한지
    public int hasGrenades;
    public float throwPower;
    public float throwHeight;
    public Image grenadeImg;
    #endregion

    #region Player Stat variables
    [Header("HP")]
    public int DefaultHP;
    [HideInInspector] public int HP;
    public bool isDead = false;

    private Timer InvincibleTimer = new Timer();
    private Timer SpeedUpTimer = new Timer();
    private float speedupDuration = 0;

    public float invincible_Duration;
    public bool isInvincible = false;
    #endregion

    #region Item FX
    public GameObject HealFX;
    public GameObject OtherFX;
    public GameObject InvincibleFX;
    public float FX_Duration = 2.0f;
    #endregion

    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<PlayerAnimator>();
        weapon = GetComponentInChildren<Weapon>();
        playerHUD = FindObjectOfType<PlayerHUD>();
        scene = FindObjectOfType<SceneManagement>();

        HP = DefaultHP; // 추가
        ammo = defaultAmmo; // 추가

        grenadeImg.color = new Color(1, 1, 1, 0.25f);

        playerHUD.targetTransform = gameObject.transform;
        playerHUD.offset = new Vector3(0, 5f, 0);
        playerHUD.SetUpHUD();

        defaultPosition = this.gameObject.transform.position;
    }

    private void Update()
    {
        if (isDead)
        {
            hAxis = 0; vAxis = 0;
            return;
        }
        GetInput();

        Move();
        Roll();
        Fire();

        if(isInvincible)
            UpdateInvincibleTimer();
        UpdateSpeedUpTimer();
    }

    #region Input

    private void GetInput()
    {
        // #. KeyBoard Control
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        // #. Keypad Control
        if (hAxis == 0 && vAxis == 0)
        {
            if (keyControl[0]) { hAxis = -1; vAxis = 1; inputImg.rectTransform.anchoredPosition = new Vector3(-50, 50, 0);  }
            if (keyControl[1]) { hAxis = 0; vAxis = 1; inputImg.rectTransform.anchoredPosition = new Vector3(0, 75, 0); }
            if (keyControl[2]) { hAxis = 1; vAxis = 1; inputImg.rectTransform.anchoredPosition = new Vector3(50, 50, 0); }
            if (keyControl[3]) { hAxis = -1; vAxis = 0; inputImg.rectTransform.anchoredPosition = new Vector3(-75, 0, 0); }
            if (keyControl[4]) { hAxis = 0; vAxis = 0; inputImg.rectTransform.anchoredPosition = new Vector3(0, 0, 0); }
            if (keyControl[5]) { hAxis = 1; vAxis = 0; inputImg.rectTransform.anchoredPosition = new Vector3(75, 0, 0); }
            if (keyControl[6]) { hAxis = -1; vAxis = -1; inputImg.rectTransform.anchoredPosition = new Vector3(-50, -50, 0); }
            if (keyControl[7]) { hAxis = 0; vAxis = -1; inputImg.rectTransform.anchoredPosition = new Vector3(0, -75, 0); }
            if (keyControl[8]) { hAxis = 1; vAxis = -1; inputImg.rectTransform.anchoredPosition = new Vector3(50, -50, 0); }
            if (!isControl) { hAxis = 0; vAxis = 0; inputImg.rectTransform.anchoredPosition = new Vector3(0, 0, 0); }
        }
    }

    #endregion

    #region Mobile KeyPad

    public void KeyPad(int type)
    {
        for (int i = 0; i < 9; i++)
        {
            keyControl[i] = i == type;
        }
    }

    public void KeyDown()
    {
        isControl = true;
    }

    public void KeyUp()
    {
        isControl = false;
    }

    public void ButtonRollDown()
    {
        if (!movement.isRoll)
        {
            isButtonRoll = true;
        }
    }

    public void ButtonFireDown()
    {
        isButtonFire = true;
    }

    public void ButtonFireUp()
    {
        isButtonFire = false;
    }

    public void ButtonGrenadeDown()
    {
        if (!isGrenadesEnable || isButtonRoll) return;
        Grenade();
    }

    #endregion

    #region Movement & Roll

    private void Move()
    {
        direction = new Vector3(hAxis, 0, vAxis);
        movement.MoveTo(direction);
        animator.OnMovement(Mathf.Clamp01(Mathf.Abs(hAxis) + Mathf.Abs(vAxis)));
    }

    private void Roll()
    {
        rollDelay += Time.deltaTime;
        if(rollCoolTime < rollDelay)
        {
            rollImg.color = new Color(1, 1, 1, 1.0f);
            if (isButtonRoll && !movement.isRoll)
            {
                movement.isRoll = true;
                SoundManager.Instance.PlaySE(SoundList.Sound_roll, transform.position);
                movement.Roll(direction);
                animator.OnRoll();
                rollDelay = 0.0f;
            }
        }
        else
        {
            rollImg.color = new Color(1, 1, 1, 0.25f);
        }
    }

    #endregion

    #region Action

    private void Fire()
    {
        if (weapon == null) return;

        fireDelay += Time.deltaTime;
        isFireReady = weapon.rate < fireDelay;

        if (isButtonFire && isFireReady && !isButtonRoll)
        {
            weapon.Use();
            animator.OnFire();
            fireDelay = 0;
            ammo = weapon.currentAmmo; //추가
            playerHUD.UpdateAmmo(ammo);
        }
    }

    private void Grenade()
    {
        GameObject instantGrenade = Instantiate(grenadeObj, grenadePos.position, grenadePos.rotation);
        Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
        Vector3 throwVec = transform.forward * throwPower;
        throwVec.y = throwHeight;

        rigidGrenade.AddForce(throwVec, ForceMode.Impulse);
        rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

        hasGrenades = 0;
        isGrenadesEnable = false;
        grenadeImg.color = new Color(1, 1, 1, 0.25f);
    }
    #endregion

    #region Damage
    public void TakeDamage(int value)
    {
        if (isInvincible) return;

        HP = (HP - value) > 0 ? ( HP - value) : 0; // 추가
        if (HP <= 0)
        {
            HP = 0;
            playerHUD.UpdateHP(HP);
            SoundManager.Instance.PlaySE(SoundList.Sound_lose, transform.position);
            isDead = true;
            animator.OnDead();
            Invoke("GameOverScene", 3.0f);
            return;
        }
        playerHUD.UpdateHP(HP);
    }

    private void GameOverScene()
    {
        scene.ChangeScene("GameOver");
    }
    #endregion

    #region Item

    // #. HP
    public void AddHP(int value)
    {
        SoundManager.Instance.PlaySE(SoundList.Sound_acquisition_heal, transform.position);
        HP = HP + value < DefaultHP ? HP + value : DefaultHP;

        HealFX.SetActive(true);
        StartCoroutine(OffHealFX());
        playerHUD.UpdateHP(HP);
    }

    // #. Ammo
    public void AddAmmo(int value)
    {
        SoundManager.Instance.PlaySE(SoundList.Sound_acquisition_bullet, transform.position);
        ammo = ammo + value < defaultAmmo ? ammo + value : defaultAmmo;
        weapon.currentAmmo = ammo;
        OtherFX.SetActive(true);
        StartCoroutine(OffOtherFX());
        playerHUD.UpdateAmmo(ammo);
    }

    public void Reset()
    {
        HP = DefaultHP;
        playerHUD.UpdateHP(HP);

        ammo = defaultAmmo;
        weapon.currentAmmo = ammo;
        playerHUD.UpdateAmmo(ammo);

        isGrenadesEnable = false;
        hasGrenades = 0;
        grenadeImg.color = new Color(1, 1, 1, 0.25f);

        this.gameObject.transform.position = new Vector3(0, 6.27f, 0.9f);

        GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position =
            new Vector3(0, 21.6f, -16.6f);
    }

    // #. Greande
    public void AddGrenade()
    {
        SoundManager.Instance.PlaySE(SoundList.Sound_acquisition_item, transform.position);
        hasGrenades = 1;
        isGrenadesEnable = true;
        OtherFX.SetActive(true);
        StartCoroutine(OffOtherFX());
        grenadeImg.color = new Color(1, 1, 1, 1.0f);
    }

    // #. Invincible
    public void AddInvincible(float duration)
    {
        InvincibleTimer.ResetTimer();
        InvincibleTimer.StartTimer(); 
        SoundManager.Instance.PlaySE(SoundList.Sound_acquisition_item, transform.position);
        isInvincible = true;
        invincible_Duration = duration;
        OtherFX.SetActive(true);
        InvincibleFX.SetActive(true);
        StartCoroutine(OffOtherFX());
    }

    private void UpdateInvincibleTimer()
    {
        InvincibleTimer.UpdateTimer();

        if (InvincibleTimer.GetTimer() >= invincible_Duration)
        {
            InvincibleTimer.StopTimer();
            isInvincible = false;
            InvincibleFX.SetActive(false);
        }
    }

    // #. SpeedUp
    public void AddSpeedUp(float value, float duration)
    {
        SpeedUpTimer.ResetTimer();
        SpeedUpTimer.StartTimer();

        SoundManager.Instance.PlaySE(SoundList.Sound_acquisition_item, transform.position);
        movement.moveSpeed = value;
        speedupDuration = duration;
        movement.isSpeedUp = true;
        OtherFX.SetActive(true);
        StartCoroutine(OffOtherFX());
    }

    public void DecreaseSpeed(float value)
    {
        float moveSpeed = movement.moveSpeed;
        moveSpeed = moveSpeed - value >= 0 ? moveSpeed - value : 0;
        movement.moveSpeed = moveSpeed;
    }

    private void UpdateSpeedUpTimer()
    {
        SpeedUpTimer.UpdateTimer();

        if (SpeedUpTimer.GetTimer() >= speedupDuration)
        {
            movement.isSpeedUp = false;
            SpeedUpTimer.StopTimer();
        }
        if (SpeedUpTimer.GetTimerStopState())
        {
            movement.moveSpeed = movement.defaultMoveSpeed;
        }
    }
    #endregion

    #region OFF FX
    IEnumerator OffHealFX()
    {
        yield return new WaitForSeconds(FX_Duration);
        HealFX.SetActive(false);
    }

    IEnumerator OffOtherFX()
    {
        yield return new WaitForSeconds(FX_Duration);
        OtherFX.SetActive(false);
    }
    #endregion
}
