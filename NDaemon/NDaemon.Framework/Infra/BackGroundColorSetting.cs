using System;

namespace NDaemon.Framework.Infra
{
    public class BackGroundColorSetting: IDisposable
    {
        private readonly ConsoleColor m_originalColor;

        public BackGroundColorSetting(ConsoleColor newColor)
        {
            m_originalColor = Console.BackgroundColor;
            Console.BackgroundColor = newColor;
        }
        #region IDisposable
        public void Dispose()
        {
            Console.BackgroundColor = m_originalColor;
        }
        #endregion
    }
}