using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.Audio;

public class Mainmenu : MonoBehaviour
{
    public GameObject start;
    public GameObject Option;
    public GameObject quit;
    public GameObject panel;
    public Slider volumeSlider;
    public AudioClip clip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.Play();

        // 슬라이더 값에 대한 초기 볼륨 설정
        volumeSlider.value = audioSource.volume;

        // 슬라이더 값 변경에 따른 이벤트 핸들러 등록
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StArt()
    {
        SceneManager.LoadScene("Stage");
    }

    public void OpTion()
    {
        panel.SetActive(true);
    }

    public void QuIt()
    {
        Application.Quit();
    }

    public void Re()
    {
        panel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
