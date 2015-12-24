using System;

namespace DesignPatterns.Observer
{
    public class Album
    {
        private BillingService billing;
        private String name;

        public Album(BillingService billing, string name)
        {
            this.billing = billing;
            this.name = name;
        }

        public void Play()
        {
            billing.GenerateCharge(this);

            // code to play the album
        }

        public String Name
        {
            get { return name; }
        }
    }
}