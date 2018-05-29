using UnityEngine;
using System.Collections;

public static class DeviceUtil {

    public static string GetBundleIdentifier()
    {
        return Application.bundleIdentifier;
    }
}
