using System;

namespace DesignPatterns.Observer
{
    public class Album
    {
        private String name;
        private readonly ISubject m_playSubject = new SubjectHelper();

        public Album(String name)
        { this.name = name; }

        public void Play()
        {
            m_playSubject.Notify(this);

            // code to play the album
        }

        public String Name
        {
            get { return name; }
        }

        public ISubject PlaySubject
        {
            get { return m_playSubject; }
        }
    }
}