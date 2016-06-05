// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLListPage.cs" project="Gmtl.HandyLib" date="2016-06-05 15:27">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
namespace Gmtl.HandyLib
{
    public class HLListPage<T>
    {
        private readonly List<T> itemsOnPage;

        public HLListPage(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "pageNumber cannot be less than 1..");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "pageSize cannot be less than 1.");

            if (items == null)
                throw new ArgumentNullException("items", "superset cannot be null.");

            TotalCount = totalCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            itemsOnPage = new List<T>(items);
        }

        public int TotalCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public int PageCount
        {
            get { return this.itemsOnPage.Count; }
        }

        public T this[int index]
        {
            get { return this.itemsOnPage[index]; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.itemsOnPage.GetEnumerator();
        }
    }
}