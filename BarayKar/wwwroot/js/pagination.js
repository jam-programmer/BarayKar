const paginationElement = document.getElementById('paginationSection');

const currentPage = paginationSection.getAttribute('data-current-page');
const totalPages = paginationSection.getAttribute('data-total-pages');
const pageSize = paginationSection.getAttribute('data-pageSize');
const keyword = paginationSection.getAttribute('data-keyword');

const parent = paginationSection.getAttribute('data-parent');


const pagination = createPagination(currentPage, totalPages,  pageSize, keyword, parent);


paginationElement.appendChild(pagination);




function createPagination(currentPage, totalPages, pageSize, keyword, parent) {
    var current = parseInt(currentPage);
    var total = parseInt(totalPages);
    var startPage = 1;
    var pagesPerGroup = 10;
    let path = "";
    var pagination = document.createElement("ul");
    pagination.className = "pagination justify-content-center justify-content-md-start";

    if (current > pagesPerGroup) {
        startPage = Math.floor((current - 1) / pagesPerGroup) * pagesPerGroup + 1;
    }

    const firstPageItem = document.createElement('li');
    firstPageItem.classList.add('page-item');
    const firstPageLink = document.createElement('a');
    firstPageLink.classList.add('page-link');

    var firstPage = startPage - pagesPerGroup;
    if (firstPage > 0) {
        var params = { Page: 1, PageSize: pageSize };
        var queryString = Object.keys(params).map(key => key + '=' + params[key]).join('&');
        var fullUrl = path + '?' + queryString;
        if (keyword) {
            fullUrl = fullUrl + '&keyword=' + keyword;
        }
        if (parent) {
            fullUrl = fullUrl + '&ParentId=' + parent;
        }
        firstPageLink.href = fullUrl;

        const iconChevronsLeft = document.createElement('i');
        iconChevronsLeft.className = 'fas fa-long-arrow-alt-right';
        firstPageLink.appendChild(iconChevronsLeft);
        firstPageItem.appendChild(firstPageLink);
        pagination.appendChild(firstPageItem);
    }

    for (let i = startPage; i <= startPage + pagesPerGroup && i <= total; i++) {
        const pageItem = document.createElement('li');

        if (i === current) {
            pageItem.className = 'page-item active';
        } else {
            pageItem.className = 'page-item';
        }

        const pageLink = document.createElement('a');
        pageLink.classList.add('page-link');

        var params = { Page: i, PageSize: pageSize };
        var queryString = Object.keys(params).map(key => key + '=' + params[key]).join('&');
        var fullUrl = path + '?' + queryString;
        if (keyword) {
            fullUrl = fullUrl + '&keyword=' + keyword;
        }
        if (parent) {
            fullUrl = fullUrl + '&ParentId=' + parent;
        }

        pageLink.href = fullUrl;
        pageLink.innerText = i;
        pageItem.appendChild(pageLink);
        pagination.appendChild(pageItem);
    }

    const lastPageItem = document.createElement('li');
    lastPageItem.classList.add('page-item');
    const lastPageLink = document.createElement('a');
    lastPageLink.classList.add('page-link');

    var lastPage = startPage + pagesPerGroup;
    if (lastPage <= total) {
        var params = { Page: total, PageSize: pageSize };
        var queryString = Object.keys(params).map(key => key + '=' + params[key]).join('&');
        var fullUrl = path + '?' + queryString;
        if (keyword) {
            fullUrl = fullUrl + '&keyword=' + keyword;
        }
        if (parent) {
            fullUrl = fullUrl + '&ParentId=' + parent;
        }
        lastPageLink.href = fullUrl;

        const iconChevronsRight = document.createElement('i');
        iconChevronsRight.className = 'fas fa-long-arrow-alt-left';
        lastPageLink.appendChild(iconChevronsRight);
        lastPageItem.appendChild(lastPageLink);
        pagination.appendChild(lastPageItem);
    }

    return pagination;
}







