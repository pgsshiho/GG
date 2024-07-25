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
    public float sliderDecreaseDuration = 5.0f;
    public Slider slider;

    // Rigidbody2D 컴포넌트를 저장할 변수
    private Rigidbody2D rb;
    private float originalSliderValue;
    private bool hasGameOverOccurred = false;

    // 점프 횟수를 추적할 변수
    private int jumpCount = 0;
    private const int maxJumpCount = 2;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
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

    private IEnumerator DecreaseSliderValue()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < sliderDecreaseDuration)
        {
            // 경과 시간을 계산합니다.
            elapsedTime += Time.deltaTime;
            // 슬라이더의 값을 점진적으로 감소시킵니다.
            float newValue = Mathf.Lerp(originalSliderValue, 0.0f, elapsedTime / sliderDecreaseDuration);
            slider.value = newValue;
            // 기다립니다.
            yield return null;
        }
        // 슬라이더의 값을 정확히 0으로 설정합니다.
        slider.value = 0.0f;
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
}
