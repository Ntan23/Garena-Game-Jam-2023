using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private Button[] buttons;
    
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateButtonContaienrAlpha(float alpha) => buttonContainer.GetComponent<CanvasGroup>().alpha = alpha;

    public void Continue()
    {
        LeanTween.moveLocalY(title, 350.0f, 0.5f).setOnComplete(() =>
        {
            LeanTween.value(buttonContainer, UpdateButtonContaienrAlpha, 0.0f, 1.0f, 0.3f).setOnComplete(() =>
            {
                foreach(Button btn in buttons) btn.enabled = true;
                buttonContainer.GetComponent<CanvasGroup>().blocksRaycasts = true;
            });
        });
    }
}
