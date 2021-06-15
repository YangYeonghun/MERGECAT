using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VoxelBusters;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EssentialKit;
using VoxelBusters.EssentialKit.Editor;

public class ShareCapture : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickShareButton()
    {
        StartCoroutine(CaptureScreenShot());
    }

    IEnumerator CaptureScreenShot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

        ShareSheet(texture);
    }

    void ShareSheet(Texture2D capture)
    {
        ShareSheet _sheet = new ShareSheet();
        _sheet.AddText("Hello");
        _sheet.AddImage(capture);
        _sheet.AddURL(URLString.URLWithPath("https://twitter.com/RoixGames"));

    }
}
