using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIManager : MonoBehaviour
{
    private string apiUrl =
        "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public IEnumerator GetClients(System.Action<string> callback)
    {
        UnityWebRequest req = UnityWebRequest.Get(apiUrl);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
            callback?.Invoke(req.downloadHandler.text);
        else
            Debug.LogError("API Error");
    }
}
