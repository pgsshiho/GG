using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransparentWall : MonoBehaviour
{
    public TextMeshProUGUI uiText; // TextMeshProUGUI 텍스트 참조

    // 시작할 때 UI 텍스트 오브젝트를 찾습니다.
    void Start()
    {
        // UI 텍스트 오브젝트 찾기
        GameObject textObject = GameObject.FindWithTag("UIText");
        if (textObject != null)
        {
            uiText = textObject.GetComponent<TextMeshProUGUI>();
            uiText.enabled = false; // 처음에는 텍스트를 비활성화
        }
        else
        {
            Debug.LogError("UI 텍스트 오브젝트를 찾을 수 없습니다.");
        }
    }

    // 플레이어와 충돌 시 텍스트를 표시합니다.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어와 충돌했는지 확인
        {
            SceneManager.LoadScene("Ending");
        }
    }

    // 일정 시간 후 텍스트를 숨기는 코루틴
    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (uiText != null)
        {
            uiText.enabled = false; // 텍스트 숨김
        }
    }
}
