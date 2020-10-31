using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    
    [SerializeField] private Sprite musicOff;
    [SerializeField] private Sprite musicOn;
    private Image image;

    private void Awake()
    {
        image.sprite = musicOff;
    }

    public void ChangeSpriteOn()
    {
        GetComponent<Image>().sprite = musicOn;
    }
    
    public void ChangeSpriteOff()
    {
        GetComponent<Image>().sprite = musicOff;
    }
}
