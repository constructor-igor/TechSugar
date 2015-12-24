namespace DesignPatterns.Observer
{
    public class BillingService : IObserver
    {
        public void Update(object subject)
        {
            Album album = subject as Album;
            if (album != null)
                GenerateCharge(album);
        }

        private void GenerateCharge(Album album)
        {
            string name = album.Name;

            //code to generate charge for correct album
        }
    }
}