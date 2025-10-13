using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;

    [Header("점프 설정")]  // 새로 추가!
    public float jumpForce = 10.0f;  // 점프 힘

    private Animator animator;
    private Rigidbody2D rb;  // 새로 추가!
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = false;
    private int score = 0;
    private Vector3 startPosition;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        Debug.Log("시작 위치 저장: " + startPosition);

        // 디버그: 제대로 찾았는지 확인
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 없습니다! Player에 추가하세요.");
        }
    }

    void Update()
    {
        // 입력 감지
        float moveX = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            moveX = -1f;  // 왼쪽
        }
        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            moveX = 1f;   // 오른쪽
        }

        // 물리 기반 이동 (새로운 방식!)
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // 점프 입력 처리 (새로 추가!)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("Jump", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("점프!");
        }


        // 애니메이션 제어
        float currentSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", currentSpeed);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 "Ground" Tag를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("바닥에 착지!");
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("⚠️ 장애물 충돌! 시작 지점으로 돌아갑니다.");

            // 시작 위치로 순간이동
            transform.position = startPosition;

            // 속도 초기화 (안 하면 계속 날아감)
            rb.linearVelocity = new Vector2(0, 0);
        }
    }
        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Debug.Log("바닥에서 떨어짐");
                isGrounded = false;
            }
        }

    // 아이템 수집 감지 (Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gems"))
        {
            score++;  // 점수 증가
            Debug.Log("젬 획득! 현재 점수: " + score);
            Destroy(other.gameObject);
        }
            
        if (other.CompareTag("Goal"))
        {
            Debug.Log("🎉🎉🎉 게임 클리어! 🎉🎉🎉");
            Debug.Log("최종 점수: " + score + "점");

            // 캐릭터 조작 비활성화
            rb.linearVelocity = Vector2.zero;
            animator.enabled = false;
            enabled = false;
        }
    }

}