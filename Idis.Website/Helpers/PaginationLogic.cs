using System;
using System.Collections.Generic;
using System.Linq;

namespace Idis.Website
{
    public class PaginationLogic
    {
        public PaginationLogic(int totalItems, int currentPage, int pageSize, int maxPages = 5)
        {
            // ensure page size isn't out of range
            if (pageSize < 7)
            {
                pageSize = 7;
            }
            else if (pageSize > 36)
            {
                pageSize = 36;
            }

            // calculate total pages
            var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);

            // ensure current page isn't out of range
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            int startPageStep, endPageStep;
            if (totalPages <= maxPages)
            {
                // total pages less than max so show all pages
                startPageStep = 1;
                endPageStep = totalPages;
            }
            else
            {
                // total pages more than max so calculate start and end pages
                var maxPagesBeforeCurrentPage = (int)Math.Floor(maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling(maxPages / (decimal)2) - 1;
                if (currentPage <= maxPagesBeforeCurrentPage)
                {
                    // current page near the start
                    startPageStep = 1;
                    endPageStep = maxPages;
                }
                else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
                {
                    // current page near the end
                    startPageStep = totalPages - maxPages + 1;
                    endPageStep = totalPages;
                }
                else
                {
                    // current page somewhere in the middle
                    startPageStep = currentPage - maxPagesBeforeCurrentPage;
                    endPageStep = currentPage + maxPagesAfterCurrentPage;
                }
            }

            int startPage, endPage;
            startPage = 1;
            endPage = (int)Math.Ceiling(totalItems / (decimal)pageSize);


            // calculate start and end item indexes
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);

            // create an array of pages that can be looped over
            var pages = Enumerable.Range(startPageStep, (endPageStep + 1) - startPageStep);

            // update object instance with all pager properties required by the view
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPageStep = startPageStep;
            EndPageStep = endPageStep;
            StartPage = startPage;
            EndPage = endPage;
            StartIndex = startIndex;
            EndIndex = endIndex;
            Pages = pages;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartPageStep { get; private set; }
        public int EndPageStep { get; private set; }
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }
        public IEnumerable<int> Pages { get; private set; }
    }
}
