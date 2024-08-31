namespace Pospec.Helper
{
    public static class PlatformInfo
    {
        public const Platform current =
#if UNITY_EMBEDDED_LINUX
            Platform.EmbededLinux;
#elif UNITY_QNX
            Platform.QNX;
#elif UNITY_STANDALONE_OSX
            Platform.OSX;
#elif UNITY_STANDALONE_WIN
            Platform.Windows;
#elif UNITY_STANDALONE_LINUX
            Platform.Linux;
#elif UNITY_IOS
            Platform.IOS;
#elif UNITY_IPHONE
            Platform.iPhone;
#elif UNITY_VISIONOS
            Platform.VisionOS;
#elif UNITY_ANDROID
            Platform.Android;
#elif UNITY_TVOS
            Platform.TvOS;
#elif UNITY_WSA
            Platform.WSA;
#elif UNITY_WSA_10_0
            Platform.WSA10;
#elif UNITY_WEBGL
            Platform.WebGL;
#else
            Platform.Unknown;
#endif
    }

    public enum Platform { Unknown, Windows, Linux, EmbededLinux, OSX, Android, IOS, iPhone, WebGL, VisionOS, TvOS, WSA, WSA10, QNX }
}
