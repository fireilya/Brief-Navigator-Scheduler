using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    public static class ImageServerMock
    {
        private static readonly Dictionary<string, Sprite> ImageCache = new();

        public static Sprite LoadImage(string path)
        {
            var imagePath = $"Images/{path}";
            if (ImageCache.TryGetValue(imagePath, out var image)) return image;
            var loadedImage = Resources.Load<Sprite>(imagePath);
            ImageCache.Add(imagePath, loadedImage);
            return loadedImage;
        }
        public static void UnloadImage(Sprite image) => Resources.UnloadAsset(image);
    }
}
