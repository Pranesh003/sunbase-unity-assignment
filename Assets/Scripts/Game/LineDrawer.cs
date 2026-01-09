using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class LineDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public CircleSpawner spawner;
    public AudioSource audioSource;
    public AudioClip popSound;

    private List<Vector3> points = new List<Vector3>();

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (lineRenderer == null || spawner == null || Camera.main == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            if (points.Count == 0 || Vector3.Distance(points[^1], pos) > 0.1f)
            {
                points.Add(pos);
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckCircles();
        }
    }

    void CheckCircles()
    {
        List<GameObject> circles = spawner.GetCircles();

        for (int i = circles.Count - 1; i >= 0; i--)
        {
            GameObject c = circles[i];
            if (c == null) continue;

            foreach (Vector3 p in points)
            {
                if (Vector2.Distance(c.transform.position, p) < 0.8f)
                {
                    // 🔊 Play sound ONCE
                    if (audioSource != null && popSound != null)
                        audioSource.PlayOneShot(popSound);

                    // ✨ Animate & destroy
                    c.transform.DOScale(0f, 0.2f)
                        .SetEase(Ease.InBack)
                        .OnComplete(() => Destroy(c));

                    circles.RemoveAt(i); // 🧹 remove from list
                    break;
                }
            }
        }
    }
}
