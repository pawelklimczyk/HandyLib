﻿// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLListPage.cs" project="Gmtl.HandyLib" date="2016-06-05 15:27">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Provides base class for 'pagination'
    /// </summary>
    /// <typeparam name="T">List item type</typeparam>
    public class HLListPage<T> : IEnumerable<T>
    {
        private readonly List<T> itemsOnPage;

        public static int DefaultPageSize = 20;

        /// <summary>
        /// Creates one page based on input
        /// </summary>
        /// <param name="items"></param>
        public HLListPage(IEnumerable<T> items) : this(items, items.Count(), 1, items.Count()) { }

        public HLListPage(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = DefaultPageSize;

            if (items == null)
                throw new ArgumentNullException("items", "superset cannot be null.");

            TotalCount = totalCount;
            TotalPages = (totalCount / pageSize) + (((totalCount % pageSize) > 0) ? 1 : 0);

            PageSize = pageSize;
            PageNumber = pageNumber;

            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < TotalPages;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= TotalPages;

            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            int num = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = num > TotalCount ? TotalCount : num;

            itemsOnPage = new List<T>(items);
        }

        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public bool IsFirstPage { get; private set; }
        public bool IsLastPage { get; private set; }
        public int FirstItemOnPage { get; private set; }
        public int LastItemOnPage { get; private set; }

        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        /// <summary>
        /// Actual number of items on current page
        /// </summary>
        public int Count
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

        public bool IsEmpty
        {
            get { return itemsOnPage.Count == 0; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}