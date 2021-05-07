using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTips : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private Text tutorialText;
    
    [TextArea]
    public string tutorialTip;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = tutorialTip;

        tutorialUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorialUI.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !tutorialUI.activeSelf)
        {
            tutorialUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            tutorialUI.SetActive(false);
        }
    }
}
