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
    public float sliderDecreaseDuration = 5.0f; // �����̴��� 0�� �Ǳ������ �ð� (�� ����)
    public Slider slider;
    // ��ֹ� �浹 �� �����̴� ���� ��
    public float obstacleHitDecreaseAmount = 10.0f; // ��ֹ��� �浹 �� ���� ��
    // �� �����Ӹ��� ������ �����̴� ��
    public float sliderDecreaseAmountPerFrame = 0.1f; // �� �����Ӹ��� ���� ��

    // Rigidbody2D ������Ʈ�� ������ ����
    private Rigidbody2D rb;
    private float originalSliderValue;
    private bool hasGameOverOccurred = false;

    // ���� Ƚ���� ������ ����
    private int jumpCount = 0;
    private const int maxJumpCount = 2;

    // ���� ȸ�� ���¸� ������ ����
    private Quaternion originalRotation;
    private bool isShiftPressed = false;

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

        // ���� ȸ�� ���¸� �����մϴ�.
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        // ����Ʈ Ű �Է� ����
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
            // ���� ���� UI�� Ȱ��ȭ�ϰ� ������ ����ϴ�.
            GameOver();
        }
    }

    void FixedUpdate()
    {
        // ��ü�� ������ �̵��մϴ�.
        Vector2 targetVelocity = new Vector2(speed, rb.velocity.y);
        rb.velocity = targetVelocity;

        // ����Ʈ Ű�� ������ �� ȸ��
        if (isShiftPressed)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            // ����Ʈ Ű�� ������ �� ���� ���·� ����
            transform.rotation = originalRotation;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ֹ��� �浹 �� �����̴� ���� ���ҽ�ŵ�ϴ�.
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
            // �����̴��� ���� �� �����Ӹ��� Ư�� ����ŭ ���ҽ�ŵ�ϴ�.
            slider.value -= sliderDecreaseAmountPerFrame * Time.deltaTime;
            // �����̴� ���� 0���� �۾����� �ʵ��� �մϴ�.
            if (slider.value < 0)
            {
                slider.value = 0;
            }
            // ��ٸ��ϴ�.
            yield return null;
        }
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

    void DecreaseSliderOnHit()
    {
        // �����̴� ���� Ư�� ����ŭ ��� ���ҽ�ŵ�ϴ�.
        slider.value -= obstacleHitDecreaseAmount;
        Debug.Log("Slider value decreased to: " + slider.value);
        // �����̴� ���� 0���� �۾����� �ʵ��� �մϴ�.
        if (slider.value < 0)
        {
            slider.value = 0;
        }
    }
}
