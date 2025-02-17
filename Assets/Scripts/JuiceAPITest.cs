using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;

public class JuiceAPITest : MonoBehaviour
{
    public Button sendRequestButton; //button to do the api thing
    private string serverUrl = "https://juice.hackclub.com/api"; //idk lol
    private string airtableUrl = "https://api.airtable.com/v0/appYGvbQexcM4HCbS/Signups";
    private string airtableApiKey = "patmkvAW1ZLPq2rOt.fe4df6a7e67f72547ffa86939c35e6fed212c9c74b8d240bc8cfdae0f9ff37d0";
    private string testAuthToken = "WEmGuT2oVRC5Mwxf"; //my juice token thing

    void Start()
    {
        if (sendRequestButton != null) 
        {
            sendRequestButton.onClick.AddListener(() => StartCoroutine(SendAPIRequests()));
        }
    }

    IEnumerator SendAPIRequests()
    {
        Debug.Log("Sending API requests...");
        yield return StartCoroutine(VerifyTokenAndFetchUserData());
        yield return StartCoroutine(SendDummyVideoData());
    }

    IEnumerator VerifyTokenAndFetchUserData()
    {
        string url = airtableUrl + "?filterByFormula={token}='" + testAuthToken + "'";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", "Bearer " + airtableApiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Token verified: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Token verification failed: " + request.error);
        }
    }

    IEnumerator SendDummyVideoData()
    {
        string url = serverUrl + "/video/upload"; //dummy stuff for the api call
        WWWForm form = new WWWForm();
        form.AddField("token", testAuthToken);
        form.AddField("description", "Dummy video upload");
        form.AddField("stretchId", "test_stretch_id");
        form.AddField("stopTime", "2024-02-17T12:00:00Z");
        form.AddField("isJuice", "true");

        byte[] dummyVideoData = Encoding.UTF8.GetBytes("Dummy video content");
        form.AddBinaryData("video", dummyVideoData, "dummy.mp4", "video/mp4");

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Dummy video uploaded successfully: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error uploading dummy video: " + request.error);
        }
    }
}
