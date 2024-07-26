using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // 충돌이 발생할 때 호출
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와 충돌했는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 Player 스크립트를 가져와서 데미지를 입힘
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1); // 1의 데미지를 입힘
                Debug.Log("플레이어가 가시에 닿음");
            }
        }
    }
}
