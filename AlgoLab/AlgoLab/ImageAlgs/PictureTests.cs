using NUnit.Framework;

namespace ImageAlgs
{
    [TestFixture]
    class PictureTests
    {
        [Test]
        public void CutPicture()
        {
            PictureFactory factory = new PictureFactory();
            Picture bigPicture = factory.Create(width: 5, height: 5);
            bigPicture.ToConsole();
            Picture cutPicture = bigPicture.Cut(1, 1, 2, 3);
            cutPicture.ToConsole();
            Assert.That(cutPicture.Data, Is.EquivalentTo(new byte[] { 6, 7, 11, 12, 16, 17 }));
        }
    }
}
