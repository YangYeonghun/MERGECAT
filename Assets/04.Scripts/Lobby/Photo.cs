using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Photo : MonoBehaviour
{
    [SerializeField] Button photoBtn;
    [SerializeField] Image photoImg;

    public void SetSprite(Sprite photo)
    {
        photoImg.sprite = photo;
    }

    public void OnClick()
    {
        NativeGallery.GetImageFromGallery((x) =>
        {
            Rect rect = new Rect(0, 0, 128, 128);
            Sprite sprite = Sprite.Create(NativeGallery.LoadImageAtPath(x), rect, Vector2.zero);
            SetSprite(sprite);
        });
    }
}
