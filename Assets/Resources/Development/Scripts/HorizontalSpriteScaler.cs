﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalSpriteScaler : MonoBehaviour
{
    public Sprite sprite;
    float aspectRatio = 1920 / 1080;

    float newWidth;
    float newHeight;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        aspectRatio = sprite.rect.width / sprite.rect.height;
    }

    private void Update()
    {
        ResizeSpriteToScreen();      
    }

    public void ResizeSpriteToScreen()
    {
        transform.localScale = new Vector3(1, 1, 1);

        var width = sprite.bounds.size.x;
        var height = sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        newWidth = (float)worldScreenWidth / width;
        newHeight = newWidth / aspectRatio;

        transform.localScale = new Vector3(newWidth, newHeight , 1);
    }
}
