using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    // 이동 속도 (Inspector에서 조절 가능)
    public float moveSpeed = 5.0f;
    public string playerName;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        // 게임 시작 시 한 번만 - Animator 컴포넌트 찾아서 저장
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log("안녕하세요, " + playerName + "님!");
    }
    void Update()
    {
        // 이동 벡터 계산
        Vector3 movement = Vector3.zero;

        float currentMoveSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMoveSpeed = moveSpeed * 2f;
            Debug.Log("달리기 모드 활성화!");
        }

        if (Input.GetKey(KeyCode.A))
            {
                movement += Vector3.left;
                spriteRenderer.flipX = true;
            }

        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            spriteRenderer.flipX = false;
        }
        
        // 실제 이동 적용
        if (movement != Vector3.zero)
        {
            transform.Translate(movement *  currentMoveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetBool("Jump", true);
                Debug.Log("점프!");
            }
        }
        
        // 속도 계산: 이동 중이면 moveSpeed, 아니면 0
        float currentSpeed = movement != Vector3.zero ?  currentMoveSpeed : 0f;
        
        // Animator에 속도 전달
        if (animator != null)
        {
            animator.SetFloat("Speed", currentSpeed);
            Debug.Log("Current Speed: " + currentSpeed);
        }
    }
}