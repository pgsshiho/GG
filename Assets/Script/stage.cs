using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource audioSource;
    public GameObject quit;
    public GameObject panel;
    public Slider volumeSlider;
    public GameObject Return;
    public int a = 0;
    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.Play();

        // 슬라이더 값에 대한 초기 볼륨 설정
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", audioSource.volume);
        audioSource.volume = volumeSlider.value;

        // 슬라이더 값 변경에 따른 이벤트 핸들러 등록
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && a == 0)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
            Panel.SetActive(true);
            a++;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && a == 1)
        {
            panel.SetActive(false);
            Time.timeScale = 1;
            a--;
            Panel.SetActive(false);
        }
    }

    public void Re()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
        a--;
        Panel.SetActive(false);
    }

    public void qu()
    {
        SceneManager.LoadScene("Mainmenu");
        Time.timeScale = 1.0f;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
