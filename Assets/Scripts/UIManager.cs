using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool UIactive = false;
    public GameObject UI;

       void Activate()
    {
        UIactive = false;
    }


    public void ToggleUI()
    {
        if (UIactive)
        {
            UIactive = false;
        }
        else
        {
            UIactive = true;
        }

        UI.SetActive(UIactive);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Start()
            {
                UIactive = false;
            }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleUI();
        }
    }
}
