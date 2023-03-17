using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BackEnd2.CustomClass
{
    public class PageRangeDocumentPaginator : DocumentPaginator
    {
        private readonly int _endIndex;
        private readonly DocumentPaginator _paginator;
        private readonly int _startIndex;

        public PageRangeDocumentPaginator(
            DocumentPaginator paginator,
            PageRange pageRange)
        {
            _startIndex = pageRange.PageFrom - 1;
            _endIndex = pageRange.PageTo - 1;
            _paginator = paginator;

            // Adjust the _endIndex
            _endIndex = Math.Min(_endIndex, _paginator.PageCount - 1);
        }

        public override bool IsPageCountValid => true;

        public override int PageCount
        {
            get
            {
                if (_startIndex > _paginator.PageCount - 1)
                    return 0;
                if (_startIndex > _endIndex)
                    return 0;

                return _endIndex - _startIndex + 1;
            }
        }

        public override Size PageSize
        {
            get => _paginator.PageSize;
            set => _paginator.PageSize = value;
        }

        public override IDocumentPaginatorSource Source => _paginator.Source;

        public override DocumentPage GetPage(int pageNumber)
        {
            // Just return the page from the original
            // paginator by using the "startIndex"
            return _paginator.GetPage(pageNumber + _startIndex);
        }
    }
}