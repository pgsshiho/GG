using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveAndJump2D : MonoBehaviour
{
    // �̵� �ӵ�
    public Image gameover; // ���� ���� UI �̹���
    public float speed = 5.0f;
    // ���� ��
    public float jumpForce = 10.0f;
    // �����̴� ���ҿ� ����� ����
    public float sliderDecreaseDuration = 5.0f;
    public Slider slider;

    // Rigidbody2D ������Ʈ�� ������ ����
    private Rigidbody2D rb;
    private float originalSliderValue;
    private bool hasGameOverOccurred = false;

    // ���� Ƚ���� ������ ����
    private int jumpCount = 0;
    private const int maxJumpCount = 2;

    void Start()
    {
        // Rigidbody2D ������Ʈ�� �����ɴϴ�.
        rb = GetComponent<Rigidbody2D>();

        // Rigidbody2D�� Interpolation ����
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        // �����̴��� ���� ���� �����մϴ�.
        originalSliderValue = slider.value;

        // ���� ���� UI�� ��Ȱ��ȭ�մϴ�.
        gameover.gameObject.SetActive(false);

        // �����̴� ���� ���������� ���ҽ�ŵ�ϴ�.
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
            // ���� ���� UI�� Ȱ��ȭ�ϰ� ������ ����ϴ�.
            GameOver();
        }
    }

    void FixedUpdate()
    {
        // ��ü�� ������ �̵��մϴ�.
        Vector2 targetVelocity = new Vector2(speed, rb.velocity.y);
        rb.velocity = targetVelocity;
    }

    void Jump()
    {
        // ���� ���� �ε巴�� �����մϴ�.
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڰ� �浹 �� ���� Ƚ���� �ʱ�ȭ�մϴ�.
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
            // ��� �ð��� ����մϴ�.
            elapsedTime += Time.deltaTime;
            // �����̴��� ���� ���������� ���ҽ�ŵ�ϴ�.
            float newValue = Mathf.Lerp(originalSliderValue, 0.0f, elapsedTime / sliderDecreaseDuration);
            slider.value = newValue;
            // ��ٸ��ϴ�.
            yield return null;
        }
        // �����̴��� ���� ��Ȯ�� 0���� �����մϴ�.
        slider.value = 0.0f;
    }

    void GameOver()
    {
        // ���� ���� UI�� Ȱ��ȭ�մϴ�.
        gameover.gameObject.SetActive(true);
        // ������ �ð��� ����ϴ�.
        Time.timeScale = 0;
        // ���� ������ �̹� �߻������� ǥ���մϴ�.
        hasGameOverOccurred = true;
    }
}
