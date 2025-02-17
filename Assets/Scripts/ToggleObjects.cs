using UnityEngine;
using UnityEngine.UI;

public class ToggleObjects : MonoBehaviour
{
    public Button triggerButton;
    public GameObject[] appearObjects;
    public GameObject[] disappearObjects;

    void Start()
    {
        triggerButton.onClick.AddListener(ToggleUI);
    }

    void ToggleUI()
    {
        foreach(GameObject obj in appearObjects)
            if(obj != null) obj.SetActive(true);
        
        foreach(GameObject obj in disappearObjects)
            if(obj != null) obj.SetActive(false);
    }
}
