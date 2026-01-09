using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public int minCircles = 5;
    public int maxCircles = 10;

    private List<GameObject> circles = new List<GameObject>();

    void Start()
    {
        SpawnCircles();
    }

    void SpawnCircles()
    {
        int count = Random.Range(minCircles, maxCircles + 1);

        for (int i = 0; i < count; i++)
        {
            Vector2 pos = GetRandomPosition();
            GameObject c = Instantiate(circlePrefab, pos, Quaternion.identity);

            // 🎨 Random color
            SpriteRenderer sr = c.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Random.ColorHSV(
                    0f, 1f,
                    0.6f, 1f,
                    0.7f, 1f
                );
            }

            // ✨ Spawn pop animation
            c.transform.localScale = Vector3.zero;
            c.transform.DOScale(1.5f, 0.35f)
                .SetEase(Ease.OutBack);

            circles.Add(c);
        }
    }

    Vector2 GetRandomPosition()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera not found!");
            return Vector2.zero;
        }

        float x = Random.Range(0.15f, 0.85f);
        float y = Random.Range(0.2f, 0.8f);

        Vector3 viewPos = new Vector3(x, y, cam.nearClipPlane + 1);
        return cam.ViewportToWorldPoint(viewPos);
    }

    public List<GameObject> GetCircles()
    {
        return circles;
    }
}
