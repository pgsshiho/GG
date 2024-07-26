using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransparentWall : MonoBehaviour
{
    public TextMeshProUGUI uiText; // TextMeshProUGUI �ؽ�Ʈ ����

    // ������ �� UI �ؽ�Ʈ ������Ʈ�� ã���ϴ�.
    void Start()
    {
        // UI �ؽ�Ʈ ������Ʈ ã��
        GameObject textObject = GameObject.FindWithTag("UIText");
        if (textObject != null)
        {
            uiText = textObject.GetComponent<TextMeshProUGUI>();
            uiText.enabled = false; // ó������ �ؽ�Ʈ�� ��Ȱ��ȭ
        }
        else
        {
            Debug.LogError("UI �ؽ�Ʈ ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    // �÷��̾�� �浹 �� �ؽ�Ʈ�� ǥ���մϴ�.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾�� �浹�ߴ��� Ȯ��
        {
            SceneManager.LoadScene("Ending");
        }
    }

    // ���� �ð� �� �ؽ�Ʈ�� ����� �ڷ�ƾ
    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (uiText != null)
        {
            uiText.enabled = false; // �ؽ�Ʈ ����
        }
    }
}
