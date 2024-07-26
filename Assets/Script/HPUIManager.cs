using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{
    public List<Image> hpImages; // 플레이어의 HP를 나타내는 이미지 리스트

    // 플레이어의 HP가 변경되었을 때 호출되는 메서드
    public void UpdateHP(int hp)
    {
        for (int i = 0; i < hpImages.Count; i++)
        {
            if (i < hp)
            {
                hpImages[i].enabled = true; // HP가 있는 만큼 이미지를 활성화
            }
            else
            {
                hpImages[i].enabled = false; // HP가 없는 만큼 이미지를 비활성화
            }
        }
    }
}
