using System;

namespace DesignPatterns.Observer
{
    public class Album
    {
        private readonly String m_name;

        public delegate void PlayHandler(object sender);
        public event PlayHandler PlayEvent;

        public Album(String name)
        {
            m_name = name;
            Console.WriteLine("Album()");
        }

        public void Play()
        {
            Notify();

            // code to play the album
        }

        private void Notify()
        {
            if (PlayEvent != null)
                PlayEvent(this);
        }

        public String Name
        {
            get { return m_name; }
        }

        ~Album()
        {
            Console.WriteLine("~Album()");
        }
    }
}