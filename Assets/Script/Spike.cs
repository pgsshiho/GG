using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // �浹�� �߻��� �� ȣ��
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾�� �浹�ߴ��� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� Player ��ũ��Ʈ�� �����ͼ� �������� ����
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1); // 1�� �������� ����
                Debug.Log("�÷��̾ ���ÿ� ����");
            }
        }
    }
}
