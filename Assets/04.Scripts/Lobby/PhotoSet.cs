using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoSet : MonoBehaviour
{
    [SerializeField] Transform photoParent;
    [SerializeField] List<Photo> photoList;



    // photoList에 photo 삽입
    void SetPhotos()
    {
        photoList = new List<Photo>();
        foreach(Transform t in photoParent)
        {
            photoList.Add(t.GetComponent<Photo>());
        }
    }

    
    // 저장된 사진들이 있다면
    void SetPhotos(List<Sprite> sprites)
    {
        photoList = new List<Photo>();
        foreach (Transform t in photoParent)
        {
            Photo photo = t.GetComponent<Photo>();
            photoList.Add(photo);
            //photo.set~~~(sprites[i]);
        }
    }


    public void SelectImagesFromGallery()
    {
        int index = 0;
        NativeGallery.GetImagesFromGallery((pathes) =>
        {
            Rect rect = new Rect(0, 0, 128, 128);

            foreach(string path in pathes)
            {
                if (index >= photoList.Count) break;
                Sprite sprite = Sprite.Create(NativeGallery.LoadImageAtPath(path), rect, Vector2.zero);
                photoList[index++].SetSprite(sprite);
            }
        });
    }
}
