using UnityEngine;

namespace Shared
{
    public static class ImageServerMock
    {
        public static Sprite LoadImage(string path) => 
            Resources.Load<Sprite>($"Images/{path}");
        public static void UnloadImage(Sprite image) => Resources.UnloadAsset(image);
    }
}
