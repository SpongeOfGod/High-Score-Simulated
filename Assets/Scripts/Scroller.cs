using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private float _x, _y;
    [SerializeField] private float opacityStart;
    [SerializeField] private float range;
    [SerializeField] private float opacityEnd;
    [SerializeField] private bool tremble;
    float currentOpacity = 0;

    void Update()
    {
        if (tremble) 
        {
            _x = Random.Range(range, -range);
            _y = Random.Range(range, -range);
        }
        rawImage.uvRect = new Rect(rawImage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, rawImage.uvRect.size);

        currentOpacity = Random.Range(opacityStart, opacityEnd);
        rawImage.color = new Color(1, 1, 1, currentOpacity);
    }
}
