using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool AllowMovement;
    public float speed;

    public float score;
    public string heroName;

    private Rigidbody m_rigidbody;
    private Transform m_transform;

    public ParticleSystem dashParticle;

    private Quaternion m_oldRotation;
    private float m_horAxis;
    private float m_verAxis;
    private Vector3 m_move;
    private Vector3 m_mousePos;

    [SerializeField]
    private Camera m_playerCamera;

    [SerializeField]
    private LayerMask m_layerMask;

    public Animator animator;

    [SerializeField]
    private LayerMask m_dashMask;

    public float dashDistance;
    public float dashCooldown;
    public bool canDash = true;
    private bool m_allowDash;

    public List<AudioClip> clips = new List<AudioClip>();
    public AudioSource bgMusic;

    private Animator m_anim;

    public Image playerDash;
    private float dashTimerForUI;

    private Stats m_stats;


    private void Awake()
    {
        m_transform = transform;
        m_rigidbody = GetComponent<Rigidbody>();
        m_allowDash = true;
        m_playerCamera = FindObjectOfType<Camera>();
        m_anim = GetComponentInChildren<Animator>();
        m_stats = GetComponent<Stats>();
    }

    private void Start()
    {

    }

    void FixedUpdate()
    {
        if (AllowMovement)
        {
            DoMovement();
            Rotate((MouseDir() - m_transform.position).normalized);
        }
    }

    private void Update()
    {
        if (m_transform.position.y < -20)
            GetComponent<Stats>().TakeDmg(100);
        
        if(m_stats != null && m_stats.isDead)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if (!AllowMovement)
            return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(m_allowDash && canDash)
            {
                StartCoroutine(Dash());
                dashTimerForUI = 0;
            }     
        }

        if (dashTimerForUI < dashCooldown)
            dashTimerForUI += Time.deltaTime;

        if(playerDash != null)
            UpdateDashImage();

        /*if (CurWeapon.GetType() == typeof(Sword))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && CurWeapon != null)
            {
                Sword sword = (Sword)CurWeapon;
                if (m_anim != null && sword.canMelee)
                {
                    sword.Swing();
                    m_anim.SetTrigger("Swing1");
                    SoundManager.PlayASource("Swing");
                }
            }
        }*/

        if (m_anim != null)
            m_anim.SetFloat("Speed", Mathf.Abs(m_move.magnitude) );
    }

    void DoMovement()
    {
        m_oldRotation = m_playerCamera.transform.rotation;

        Vector3 temp = m_oldRotation.eulerAngles;
        temp.x = 0;
        m_playerCamera.transform.rotation = Quaternion.Euler(temp);

        m_horAxis = Input.GetAxis("Horizontal");
        m_verAxis = Input.GetAxis("Vertical");

        m_move.x = m_horAxis;
        m_move.y = 0;
        m_move.z = m_verAxis;

        m_move = m_playerCamera.transform.TransformDirection(m_move);

        m_playerCamera.transform.rotation = m_oldRotation;

        m_move.y = 0;

        Move(m_move);
    }


    protected void Move(Vector3 moveVector)
    {
        if (AllowMovement)
        {
            m_transform.Translate(moveVector * Time.fixedDeltaTime * speed, Space.World);
        }
    }

    protected void Rotate(Vector3 rotateVector)
    {
        if (AllowMovement)
        {
            m_transform.rotation = Quaternion.LookRotation(new Vector3(rotateVector.x,0,rotateVector.z));
        }
    }

    private IEnumerator Dash()
    {
        SoundManager.PlayASource("GunSound");
        m_allowDash = false;

        Vector3 dir = m_transform.forward;

        if (m_move.magnitude > 0)
        {
            dir = m_move;
        }

        RaycastHit[] hit = Physics.RaycastAll(m_transform.position, dir, dashDistance + 1, m_dashMask);

        if (hit.Length > 1 && hit[1].collider != null)
        {
            //???
            m_transform.position = hit[1].point - dir;
        }
        else
        {
            m_transform.Translate(dir * dashDistance, Space.World);
        }
        dashParticle.Play();
        yield return new WaitForSeconds(dashCooldown);

        m_allowDash = true;
    }

    private Vector3 MouseDir()
    {
        m_mousePos.x = Input.mousePosition.x;
        m_mousePos.y = Input.mousePosition.y;
        m_mousePos.z = Camera.main.WorldToScreenPoint(m_transform.position).z;

        return Camera.main.ScreenToWorldPoint(m_mousePos);
    }

    public void UpdateHealthImage()
    {
        int hp = (int)m_stats.health;
        //playerHp.sprite = playerHps[hp];
    }

    public void UpdateDashImage()
    {
        playerDash.fillAmount = dashTimerForUI / dashCooldown;
    }

    public void Dead()
    {
        m_anim.SetTrigger("Death");
        AllowMovement = false;

        if (GetComponent<Gun>())
            GetComponent<Gun>().enabled = false;

        if (GetComponentInChildren<Sword>())
            GetComponentInChildren<Sword>().enabled = false;
    }

}