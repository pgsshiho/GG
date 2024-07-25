using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mainmenu : MonoBehaviour
{
    public GameObject start;
    public GameObject Option;
    public GameObject quit;
    public GameObject panel;
    public Slider Slider;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
