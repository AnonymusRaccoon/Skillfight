using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof (CharacterController))]
public class FirstPersonController : NetworkBehaviour
{
    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_AirSpeed;
    [SerializeField] private float m_GravityMultiplier;
    [SerializeField] private MouseLook m_MouseLook;

    private Camera m_Camera;
    private bool m_Jump;
    private Vector2 m_Input;
    private Vector3 m_MoveDir = Vector3.zero;
    private CharacterController m_CharacterController;
    private bool m_PreviouslyGrounded;
    private bool m_Jumping;
    public Animator anim;

    [HideInInspector]
    public bool Pause = false;
    private GameObject PauseUI;

    [HideInInspector]
    public bool Select = true;


    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        m_Jumping = false;
		m_MouseLook.Init(transform , m_Camera.transform);
        Select = true;
        StartCoroutine(StartCor());

    }

    private IEnumerator StartCor()
    {
        yield return new WaitForSeconds(1);
        PauseUI = GameObject.Find("GameManager").GetComponent<GameReference>().PauseUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Select == false)
        {
            if (Pause == false)
            {
                Pause = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PauseUI.SetActive(true);
                return;
            }
            else
            {
                PauseUI.GetComponent<Pause>().Resume();
            }
        }

        if (!isLocalPlayer || Select || Pause)
            return;

        RotateView();
        if (!m_Jump && !m_Jumping)
        {
            m_Jump = Input.GetKey(KeyManager.KM.jump);
        }

        if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
        {
            m_MoveDir.y = 0f;
            m_Jumping = false;
        }
        if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
        {
            m_MoveDir.y = 0f;
        }

        m_PreviouslyGrounded = m_CharacterController.isGrounded;
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer || Select || Pause)
            return;

        float speed;
        GetInput(out speed);
        Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                            m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x*speed;
        m_MoveDir.z = desiredMove.z*speed;


        if (m_CharacterController.isGrounded)
        {

            if (m_Jump)
            {
                anim.Play("Jump");
                anim.SetFloat("Speed_Vertical", 0);
                anim.SetBool("WalkR", false);
                anim.SetBool("WalkL", false);
                m_MoveDir.y = m_JumpSpeed;
                m_Jump = false;
                m_Jumping = true;
            }
        }
        else
        {
            m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
        }
        m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);
    }

    private void GetInput(out float speed)
    {
        float horizontal = 0;
        float vertical = 0;
        if (Input.GetKey(KeyManager.KM.forward))
            vertical++;
        if (Input.GetKey(KeyManager.KM.backward))
            vertical--;

        if (Input.GetKey(KeyManager.KM.right))
            horizontal++;
        if (Input.GetKey(KeyManager.KM.left))
            horizontal--;

        anim.SetFloat("Speed_Vertical", vertical);

        anim.SetBool("WalkR", (horizontal > 0) ? true : false);
        anim.SetBool("WalkL", (horizontal < 0) ? true : false);

        m_IsWalking = !Input.GetKey(KeyCode.LeftShift);

        if (!m_Jumping)
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
        else
            speed = (vertical > 0) ? m_WalkSpeed : m_AirSpeed;

        m_Input = new Vector2(horizontal, vertical);

        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
    }


    private void RotateView()
    {
        m_MouseLook.LookRotation (transform, m_Camera.transform);
    }
}
