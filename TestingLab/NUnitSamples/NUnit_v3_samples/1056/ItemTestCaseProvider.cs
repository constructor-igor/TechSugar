using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit_v3_samples._1056
{
    public class ItemTestCaseProvider : IEnumerable<ITestCaseData>
    {
        public IEnumerator<ITestCaseData> GetEnumerator()
        {
            yield return ItemTestCaseData.UsingAnyItem().WithInitialQuality(1).ToBeSoldIn(1).ShouldByTomorrowHaveQualityOf(0);
            yield return ItemTestCaseData.UsingAnyItem().WithInitialQuality(2).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(0);
            yield return ItemTestCaseData.UsingAnyItem().WithInitialQuality(0).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(0);
            yield return ItemTestCaseData.UsingAgedBrie().WithInitialQuality(0).ToBeSoldIn(1).ShouldByTomorrowHaveQualityOf(1);
            yield return ItemTestCaseData.UsingAgedBrie().WithInitialQuality(0).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(2);
            yield return ItemTestCaseData.UsingAgedBrie().WithInitialQuality(50).ToBeSoldIn(1).ShouldByTomorrowHaveQualityOf(50);
            yield return ItemTestCaseData.UsingAgedBrie().WithInitialQuality(50).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(50);
            yield return ItemTestCaseData.UsingSulfuras().ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(80);
            yield return ItemTestCaseData.UsingBackstagePass().ToBeSoldIn(11).ShouldByTomorrowHaveQualityOf(1);
            yield return ItemTestCaseData.UsingBackstagePass().ToBeSoldIn(10).ShouldByTomorrowHaveQualityOf(2);
            yield return ItemTestCaseData.UsingBackstagePass().ToBeSoldIn(5).ShouldByTomorrowHaveQualityOf(3);
            yield return ItemTestCaseData.UsingBackstagePass().WithInitialQuality(50).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(0);
            yield return ItemTestCaseData.UsingConjuredItem().WithInitialQuality(2).ToBeSoldIn(1).ShouldByTomorrowHaveQualityOf(0);
            yield return ItemTestCaseData.UsingConjuredItem().WithInitialQuality(4).ToBeSoldIn(0).ShouldByTomorrowHaveQualityOf(0);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class ItemTestCaseData : TestCaseData
    {
        #region ITestCaseData
        public RunState RunState { get; private set; }
        public object[] Arguments { get; private set; }
        public IPropertyBag Properties { get; private set; }
        public string TestName
        {
            get
            {
                return string.Format("{0} with quality {1} to be sold in {2} days should have quality of {3} tomorrow",
                    _item.Name ?? "Any Item",
                    _item.Quality,
                    _item.SellIn,
                    Result);
            }
        }
        #endregion

        public string Description { get; private set; }
        public object Result { get; private set; }

        public bool Explicit { get { return false; } }
        public object ExpectedResult { get; private set; }
        public bool HasExpectedResult { get { return true; } }
        public bool Ignored { get { return false; } }

        public Type ExpectedException { get; private set; }
        public string ExpectedExceptionName { get; private set; }
        public string IgnoreReason { get; private set; }

        private readonly Item _item;

        private ItemTestCaseData(Item item)
        {
            _item = item;
            Arguments = new[] { _item };
        }

        internal static ItemTestCaseData UsingAnyItem()
        {
            return new ItemTestCaseData(new Item { Name = "Any Item" });
        }
        internal static ItemTestCaseData UsingAgedBrie()
        {
            return new ItemTestCaseData((new Item { Name = "Aged Brie" }));
        }
        internal static ItemTestCaseData UsingSulfuras()
        {
            return new ItemTestCaseData((new Item { Name = "Sulfuras, Hand of Ragnaros", Quality = 80 }));
        }
        internal static ItemTestCaseData UsingBackstagePass()
        {
            return new ItemTestCaseData((new Item { Name = "Backstage passes to a TAFKAL80ETC concert" }));
        }
        internal static ItemTestCaseData UsingConjuredItem()
        {
            return new ItemTestCaseData((new Item { Name = "Conjured Item" }));
        }

        internal ItemTestCaseData ToBeSoldIn(int days)
        {
            _item.SellIn = days;
            return this;
        }
        internal ItemTestCaseData WithInitialQuality(int quality)
        {
            _item.Quality = quality;
            return this;
        }
        internal ItemTestCaseData ShouldByTomorrowHaveQualityOf(int quality)
        {
            Result = quality;
            return this;
        }
    }
}
