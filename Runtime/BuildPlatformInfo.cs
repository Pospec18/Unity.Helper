namespace Pospec.Helper
{
    public static class BuildPlatformInfo
    {
        public const BuildPlatform current =
#if UNITY_EMBEDDED_LINUX
            BuildPlatform.EmbededLinux;
#elif UNITY_QNX
            BuildPlatform.QNX;
#elif UNITY_STANDALONE_OSX
            BuildPlatform.OSX;
#elif UNITY_STANDALONE_WIN
            BuildPlatform.Windows;
#elif UNITY_STANDALONE_LINUX
            BuildPlatform.Linux;
#elif UNITY_IOS
            BuildPlatform.IOS;
#elif UNITY_IPHONE
            BuildPlatform.iPhone;
#elif UNITY_VISIONOS
            BuildPlatform.VisionOS;
#elif UNITY_ANDROID
            BuildPlatform.Android;
#elif UNITY_TVOS
            BuildPlatform.TvOS;
#elif UNITY_WSA
            BuildPlatform.WSA;
#elif UNITY_WSA_10_0
            BuildPlatform.WSA10;
#elif UNITY_WEBGL
            BuildPlatform.WebGL;
#else
            BuildPlatform.Unknown;
#endif
    }

    public enum BuildPlatform { Unknown, Windows, Linux, EmbededLinux, OSX, Android, IOS, iPhone, WebGL, VisionOS, TvOS, WSA, WSA10, QNX }
}
