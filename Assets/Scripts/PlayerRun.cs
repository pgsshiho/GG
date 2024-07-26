using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveAndJump2D : MonoBehaviour
{
    // 이동 속도
    public Image gameover; // 게임 오버 UI 이미지
    public float speed = 5.0f;
    // 점프 힘
    public float jumpForce = 10.0f;
    // 슬라이더 감소에 사용할 변수
    public float sliderDecreaseDuration = 5.0f; // 슬라이더가 0이 되기까지의 시간 (초 단위)
    public Slider slider;
    // 장애물 충돌 시 슬라이더 감소 값
    public float obstacleHitDecreaseAmount = 10.0f; // 장애물과 충돌 시 줄일 값
    // 매 프레임마다 감소할 슬라이더 값
    public float sliderDecreaseAmountPerFrame = 0.1f; // 매 프레임마다 줄일 값

    // Rigidbody2D 컴포넌트를 저장할 변수
    private Rigidbody2D rb;
    private float originalSliderValue;
    private bool hasGameOverOccurred = false;

    // 점프 횟수를 추적할 변수
    private int jumpCount = 0;
    private const int maxJumpCount = 2;

    // 원래 회전 상태를 저장할 변수
    private Quaternion originalRotation;
    private bool isShiftPressed = false;

    void Start()
    {
        // Rigidbody2D 컴포넌트를 가져옵니다.
        rb = GetComponent<Rigidbody2D>();

        // Rigidbody2D의 Interpolation 설정
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        // 슬라이더의 시작 값을 저장합니다.
        originalSliderValue = slider.value;

        // 게임 오버 UI를 비활성화합니다.
        gameover.gameObject.SetActive(false);

        // 슬라이더 값을 점진적으로 감소시킵니다.
        StartCoroutine(DecreaseSliderValue());

        // 원래 회전 상태를 저장합니다.
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        // 쉬프트 키 입력 감지
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isShiftPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isShiftPressed = false;
        }

        if (slider.value <= 0.0f && !hasGameOverOccurred)
        {
            // 게임 오버 UI를 활성화하고 게임을 멈춥니다.
            GameOver();
        }
    }

    void FixedUpdate()
    {
        // 물체를 옆으로 이동합니다.
        Vector2 targetVelocity = new Vector2(speed, rb.velocity.y);
        rb.velocity = targetVelocity;

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

    void Jump()
    {
        // 점프 힘을 부드럽게 적용합니다.
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥과 충돌 시 점프 횟수를 초기화합니다.
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 장애물과 충돌 시 슬라이더 값을 감소시킵니다.
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle hit! Decreasing slider value.");
            DecreaseSliderOnHit();
        }
    }

    private IEnumerator DecreaseSliderValue()
    {
        while (true)
        {
            // 슬라이더의 값을 매 프레임마다 특정 값만큼 감소시킵니다.
            slider.value -= sliderDecreaseAmountPerFrame * Time.deltaTime;
            // 슬라이더 값이 0보다 작아지지 않도록 합니다.
            if (slider.value < 0)
            {
                slider.value = 0;
            }
            // 기다립니다.
            yield return null;
        }
    }

    void GameOver()
    {
        // 게임 오버 UI를 활성화합니다.
        gameover.gameObject.SetActive(true);
        // 게임의 시간을 멈춥니다.
        Time.timeScale = 0;
        // 게임 오버가 이미 발생했음을 표시합니다.
        hasGameOverOccurred = true;
    }

    void DecreaseSliderOnHit()
    {
        // 슬라이더 값을 특정 값만큼 즉시 감소시킵니다.
        slider.value -= obstacleHitDecreaseAmount;
        Debug.Log("Slider value decreased to: " + slider.value);
        // 슬라이더 값이 0보다 작아지지 않도록 합니다.
        if (slider.value < 0)
        {
            slider.value = 0;
        }
    }
}
