using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Skrypt odpowiada za usatwienie rozdzielczosci kemerze
/// </summary>
public class CameraResolution : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);

    int shake_num = 0;
    public IEnumerator Camera_Shake()
    {
        Vector3 originPos = Camera.main.transform.localPosition;
        float duration = 4f;
        float magnitude = 0.7f;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            this.transform.localPosition += new Vector3(x, y, originPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        this.transform.localPosition = originPos;
        shake_num++;
        if (shake_num < 6)
            Invoke("Camera_Shake", 1f);
    }
}