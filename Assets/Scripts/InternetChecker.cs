using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    public TextMeshProUGUI statusText;

    void Start()
    {
        InvokeRepeating(nameof(CheckInternetConnection), 0f, 10f);
    }

    void CheckInternetConnection()
    {
        StartCoroutine(CheckConnectionCoroutine());
    }

    IEnumerator CheckConnectionCoroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://www.google.com/"))
        {
            request.timeout = 5;
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                UpdateStatus("Status: Online");
            }
            else
            {
                UpdateStatus("Status: Offline");
            }
        }
    }

    void UpdateStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }
}
