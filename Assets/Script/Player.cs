using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float hp = 3.0f; // �÷��̾��� ü��
    public float speed = 5.0f; // �÷��̾��� �ʱ� �̵� �ӵ�
    public float jumpPower = 5.0f; // �÷��̾��� ������
    private Rigidbody2D rb; // Rigidbody2D ������Ʈ ����
    private bool isGrounded = true; // �÷��̾ ���鿡 ��� �ִ��� ���� (�ʱⰪ true)
    private float speedIncreaseInterval = 5.0f; // �ӵ� ���� ����
    private float timeSinceLastIncrease = 0.0f; // ������ �ӵ� ���� ���� ��� �ð�

    // ���� ���� �� �� �� ȣ��, Rigidbody2D ������Ʈ �ʱ�ȭ
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }
    }

    void Update()
    {
        // �÷��̾ ���������� �̵�
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // ü���� 0 �����̸� ���� �޴��� �̵�
        if (hp <= 0)
        {
            SceneManager.LoadScene("Mainmenu");
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
        if (hp <= 0)
        {
            SceneManager.LoadScene("Mainmenu");
        }
    }
}
