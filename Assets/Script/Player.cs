using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float hp = 3.0f; // 플레이어의 체력
    public float speed = 5.0f; // 플레이어의 초기 이동 속도
    public float jumpPower = 5.0f; // 플레이어의 점프력
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트 참조
    private bool isGrounded = true; // 플레이어가 지면에 닿아 있는지 여부 (초기값 true)
    private float speedIncreaseInterval = 5.0f; // 속도 증가 간격
    private float timeSinceLastIncrease = 0.0f; // 마지막 속도 증가 이후 경과 시간

    // 게임 시작 시 한 번 호출, Rigidbody2D 컴포넌트 초기화
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 초기화되지 않았습니다.");
        }
    }

    void Update()
    {
        // 플레이어를 오른쪽으로 이동
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // 체력이 0 이하이면 메인 메뉴로 이동
        if (hp <= 0)
        {
            SceneManager.LoadScene("Mainmenu");
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
        if (hp <= 0)
        {
            SceneManager.LoadScene("Mainmenu");
        }
    }
}
