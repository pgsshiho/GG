using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool isShiftPressed = false;
    private Quaternion originalRotation;
    public float hp = 3.0f; // �÷��̾��� ü��
    public float speed = 5.0f; // �÷��̾��� �ʱ� �̵� �ӵ�
    public float jumpPower = 5.0f; // �÷��̾��� ������
    private Rigidbody2D rb; // Rigidbody2D ������Ʈ ����
    private bool isGrounded = true; // �÷��̾ ���鿡 ��� �ִ��� ���� (�ʱⰪ true)
    private float speedIncreaseInterval = 8.0f; // �ӵ� ���� ����
    private float timeSinceLastIncrease = 0.0f; // ������ �ӵ� ���� ���� ��� �ð�
    private Animator animator; // Animator ������Ʈ ����
    private CapsuleCollider2D capsuleCollider; // CapsuleCollider2D ������Ʈ ����
    private Vector2 originalColliderSize; // ���� ĸ�� �ݶ��̴� ũ��

    public HPUIManager hpUIManager; // HPUIManager ����

    // ���� ���� �� �� �� ȣ��, Rigidbody2D ������Ʈ �ʱ�ȭ
    void Start()
    {
        // ���� ȸ�� ���¸� �����մϴ�.
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }
        if (animator == null)
        {
            Debug.LogError("Animator�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider2D�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }
        else
        {
            originalColliderSize = capsuleCollider.size;
        }

        // HP UI ������Ʈ �ʱ�ȭ
        hpUIManager.UpdateHP((int)hp);
    }

    void Update()
    {
        // ü���� 0 �����̸� ���� �޴��� �̵�
        if (hp <= 0)
        {
            SceneManager.LoadScene("Mainmenu");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isShiftPressed = true;
            animator.SetBool("isRolling", true); // �ִϸ��̼� Ȱ��ȭ
            capsuleCollider.size = new Vector2(capsuleCollider.size.x / 2, capsuleCollider.size.y); // �ݶ��̴� X ũ�� ����
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isShiftPressed = false;
            animator.SetBool("isRolling", false); // �ִϸ��̼� ��Ȱ��ȭ
            capsuleCollider.size = originalColliderSize; // �ݶ��̴� ũ�� ����
        }

        // �����̽��ٸ� ������ ���鿡 ��� ���� �� ����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("����");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // �ӵ� ���� ���� üũ
        timeSinceLastIncrease += Time.deltaTime;
        if (timeSinceLastIncrease >= speedIncreaseInterval)
        {
            speed += 1.0f; // �ӵ��� 1 ����
            timeSinceLastIncrease = 0.0f; // ��� �ð� �ʱ�ȭ
            Debug.Log("�ӵ� ����: " + speed);
        }
    }

    void FixedUpdate()
    {
        // �÷��̾ ���������� �̵�
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);

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

    // �浹�� �߻��� �� ȣ��, ���鿡 ��Ҵ��� ���� Ȯ��
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground�� ����");
            isGrounded = true;
        }
    }

    // �������� ó���ϴ� �޼���
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("�������� ����. ���� ü��: " + hp);
        hpUIManager.UpdateHP((int)hp); // HP UI ������Ʈ
        if (hp <= 0)
        {
            SceneManager.LoadScene("Mainmenu");
        }
    }
}
