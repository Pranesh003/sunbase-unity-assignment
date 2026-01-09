using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ClientListUI : MonoBehaviour
{
    public APIManager apiManager;
    public TMP_Dropdown filterDropdown;
    public Transform content;
    public GameObject clientItemPrefab;
    public ClientPopupUI popupUI;

    private List<ClientModel> allClients = new List<ClientModel>();
    private Dictionary<int, ClientModel> dataMap = new Dictionary<int, ClientModel>();

    void Start()
    {
        StartCoroutine(apiManager.GetClients(OnDataReceived));
        filterDropdown.onValueChanged.AddListener(OnFilterChanged);
    }

    void OnDataReceived(string json)
    {
        // Parse clients array
        ClientResponse response = JsonUtility.FromJson<ClientResponse>(json);
        if (response == null || response.clients == null)
        {
            Debug.LogError("Client parsing failed");
            return;
        }

        // Parse data{} block
        ParseDataBlock(json);

        allClients.Clear();

        foreach (ClientModel c in response.clients)
        {
            if (dataMap.ContainsKey(c.id))
            {
                c.name = dataMap[c.id].name;
                c.address = dataMap[c.id].address;
                c.points = dataMap[c.id].points;
            }
            allClients.Add(c);
        }

        RefreshList();
    }

    void RefreshList()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        int filter = filterDropdown.value;

        foreach (ClientModel client in allClients)
        {
            if (filter == 1 && !client.isManager) continue;
            if (filter == 2 && client.isManager) continue;

            GameObject item = Instantiate(clientItemPrefab, content);
            item.GetComponent<ClientItemUI>().Setup(client);

            item.GetComponent<Button>().onClick.RemoveAllListeners();
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                popupUI.Show(client);
            });
        }
    }

    void OnFilterChanged(int value)
    {
        RefreshList();
    }

    // Regex-based data parser (SAFE)
    void ParseDataBlock(string json)
    {
        dataMap.Clear();

        Regex regex = new Regex(
            "\"(\\d+)\":\\{\"address\":\"(.*?)\",\"name\":\"(.*?)\",\"points\":(\\d+)\\}"
        );

        MatchCollection matches = regex.Matches(json);

        foreach (Match m in matches)
        {
            int id = int.Parse(m.Groups[1].Value);

            ClientModel c = new ClientModel
            {
                id = id,
                address = m.Groups[2].Value,
                name = m.Groups[3].Value,
                points = int.Parse(m.Groups[4].Value)
            };

            dataMap[id] = c;
        }
    }
}
