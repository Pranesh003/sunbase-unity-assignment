using UnityEngine;
using TMPro;

public class ClientItemUI : MonoBehaviour
{
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI pointsText;

    public void Setup(ClientModel client)
    {
        labelText.text = client.label;
        pointsText.text = client.points.ToString();
    }
}
