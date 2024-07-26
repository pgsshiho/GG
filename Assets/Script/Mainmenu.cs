using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    public GameObject start;
    public GameObject Option;
    public GameObject quit;
    public GameObject panel;
    public GameObject Return;
    public GameObject con;
    public GameObject X;
    public GameObject cont;
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

        // �����̴� ���� ���� �ʱ� ���� ����
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", audioSource.volume);

        // �����̴� �� ���濡 ���� �̺�Ʈ �ڵ鷯 ���
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
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void control()
    {
        con.SetActive(true);
    }

    public void dprtm()
    {
        con.SetActive(false);
    }
}
