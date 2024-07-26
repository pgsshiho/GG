using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{
    public List<Image> hpImages; // �÷��̾��� HP�� ��Ÿ���� �̹��� ����Ʈ

    // �÷��̾��� HP�� ����Ǿ��� �� ȣ��Ǵ� �޼���
    public void UpdateHP(int hp)
    {
        for (int i = 0; i < hpImages.Count; i++)
        {
            if (i < hp)
            {
                hpImages[i].enabled = true; // HP�� �ִ� ��ŭ �̹����� Ȱ��ȭ
            }
            else
            {
                hpImages[i].enabled = false; // HP�� ���� ��ŭ �̹����� ��Ȱ��ȭ
            }
        }
    }
}
