using UnityEngine;
using TMPro;
using DG.Tweening;

public class ClientPopupUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI addressText;

    public void Show(ClientModel client)
    {
        nameText.text = client.name;
        pointsText.text = "Points: " + client.points;
        addressText.text = "Address: " + client.address;

        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
    }

    public void Close()
    {
        transform.DOScale(0f, 0.2f)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
