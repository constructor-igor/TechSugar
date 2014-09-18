using System;
using NUnit.Framework;

/*
 * The test project created for oz-code review (include checking different ideas for Assert visualization)
 * Part of the ideas can be found in http://ozcode.userecho.com/
 * */

namespace AssertSamples
{
    [TestFixture]
    public class AssertSamplesOfObjects
    {
        [Test]
        [ExpectedException]
        public void AssertOfObjects()
        {
            var expectedDataItem = new DataItem("title", "content");
            DataItem actualDataItem = GetExpectedDataItem("title2", "content");

            Assert.AreEqual(expectedDataItem.Title, actualDataItem.Title);
            Assert.AreEqual(expectedDataItem.Content, actualDataItem.Content);
        }

        #region private methods
        private DataItem GetExpectedDataItem(string title, string content)
        {
            return new DataItem(title, content);
        }
        #endregion
    }

    public class DataItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public DataItem(string title, string content)
        {
            Title = title;
            Content = content;
            Date = DateTime.Now;
        }
    }
}