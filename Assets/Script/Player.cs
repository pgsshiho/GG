using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool isShiftPressed = false;
    private Quaternion originalRotation;
    public float hp = 3.0f; // 플레이어의 체력
    public float speed = 5.0f; // 플레이어의 초기 이동 속도
    public float jumpPower = 5.0f; // 플레이어의 점프력
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트 참조
    private bool isGrounded = true; // 플레이어가 지면에 닿아 있는지 여부 (초기값 true)
    private float speedIncreaseInterval = 8.0f; // 속도 증가 간격
    private float timeSinceLastIncrease = 0.0f; // 마지막 속도 증가 이후 경과 시간
    private Animator animator; // Animator 컴포넌트 참조
    private CapsuleCollider2D capsuleCollider; // CapsuleCollider2D 컴포넌트 참조
    private Vector2 originalColliderSize; // 원래 캡슐 콜라이더 크기

    public HPUIManager hpUIManager; // HPUIManager 참조

    // 게임 시작 시 한 번 호출, Rigidbody2D 컴포넌트 초기화
    void Start()
    {
        // 원래 회전 상태를 저장합니다.
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 초기화되지 않았습니다.");
        }
        if (animator == null)
        {
            Debug.LogError("Animator가 초기화되지 않았습니다.");
        }
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider2D가 초기화되지 않았습니다.");
        }
        else
        {
            originalColliderSize = capsuleCollider.size;
        }

        // HP UI 업데이트 초기화
        hpUIManager.UpdateHP((int)hp);
    }

    void Update()
    {
        // 체력이 0 이하이면 메인 메뉴로 이동
        if (hp <= 0)
        {
            SceneManager.LoadScene("Mainmenu");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isShiftPressed = true;
            animator.SetBool("isRolling", true); // 애니메이션 활성화
            capsuleCollider.size = new Vector2(capsuleCollider.size.x / 2, capsuleCollider.size.y); // 콜라이더 X 크기 줄임
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isShiftPressed = false;
            animator.SetBool("isRolling", false); // 애니메이션 비활성화
            capsuleCollider.size = originalColliderSize; // 콜라이더 크기 복원
        }

        // 스페이스바를 누르고 지면에 닿아 있을 때 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("점프");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // 속도 증가 간격 체크
        timeSinceLastIncrease += Time.deltaTime;
        if (timeSinceLastIncrease >= speedIncreaseInterval)
        {
            speed += 1.0f; // 속도를 1 증가
            timeSinceLastIncrease = 0.0f; // 경과 시간 초기화
            Debug.Log("속도 증가: " + speed);
        }
    }

    void FixedUpdate()
    {
        // 플레이어를 오른쪽으로 이동
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);

        // 쉬프트 키가 눌렸을 때 회전
        if (isShiftPressed)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            // 쉬프트 키가 떼졌을 때 원래 상태로 복귀
            transform.rotation = originalRotation;
        }
    }

    // 충돌이 발생할 때 호출, 지면에 닿았는지 여부 확인
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground에 닿음");
            isGrounded = true;
        }
    }

    // 데미지를 처리하는 메서드
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("데미지를 입음. 현재 체력: " + hp);
        hpUIManager.UpdateHP((int)hp); // HP UI 업데이트
        if (hp <= 0)
        {
            SceneManager.LoadScene("Mainmenu");
        }
    }
}
