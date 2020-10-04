using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle;

    public Gun activeGun;

    public float restingSize;
    public float maxSize;
    public float growSpeed;
    public float shrinkSpeed;

    public float currentSize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
        activeGun.SetReticle();
    }

    private void Update()
    {
        if (isMoving || activeGun.shooting)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * growSpeed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * shrinkSpeed);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize) * 18F;
    }

    bool isMoving
    {
        get
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                return true;
            else
                return false;
        }
    }
}
