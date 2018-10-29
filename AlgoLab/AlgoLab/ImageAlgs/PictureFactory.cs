using System.Linq;

namespace ImageAlgs
{
    public class PictureFactory
    {
        public Picture Create(int width, int height)
        {
            Picture picture = new Picture(Enumerable.Range(0, width*height).Select(item => (byte)item), width, height);
            return picture;
        }
    }
}