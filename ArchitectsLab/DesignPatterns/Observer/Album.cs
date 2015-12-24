using System;

namespace DesignPatterns.Observer
{
    public class Album: Subject
    {
        private readonly String m_name;

        public Album(string name)
        {
            m_name = name;
        }

        public void Play()
        {
            Notify();

            // code to play the album
        }

        public String Name
        {
            get { return m_name; }
        }
    }
}