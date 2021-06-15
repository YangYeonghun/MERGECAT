using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGalleyMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NativeGallery.Permission permission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Read);

        if(permission == NativeGallery.Permission.Granted)
        {

        }
        else
        {
            NativeGallery.RequestPermission(NativeGallery.PermissionType.Read);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
