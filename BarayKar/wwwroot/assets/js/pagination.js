const paginationElement = document.getElementById('paginationSection');

const currentPage = paginationSection.getAttribute('data-current-page');
const totalPages = paginationSection.getAttribute('data-total-pages');
const controller = paginationSection.getAttribute('data-controller-name');
const action = paginationSection.getAttribute('data-action-name');
const pageSize = paginationSection.getAttribute('data-pageSize');
const keyword = paginationSection.getAttribute('data-keyword');

const parent = paginationSection.getAttribute('data-parent');

    
const pagination = createPagination(currentPage, totalPages, controller, action, pageSize, keyword, parent);


paginationElement.appendChild(pagination);
function createPagination(currentPage, totalPages, controller, action, pageSize, keyword, parent) {

    var current = currentPage;
    var total = totalPages;
    var flag = 5;
    var start = 1;
    var pageSize = pageSize;
    var path = "/Admin/" + controller + "/" + action;
    var keyword = keyword;


    var pagination = document.createElement("li");
    pagination.className = "pagination justify-content-center justify-content-md-start";

    const firstPageItem = document.createElement('li');
    firstPageItem.classList.add('page-item');
    const firstPageLink = document.createElement('a');
    firstPageLink.classList.add('page-link');

    var params = { CurentPage: 1, PageSize: pageSize };
    var queryString = Object.keys(params).map(key => key + '=' + params[key]).join('&');
    var fullUrl = path + '?' + queryString;

    if (keyword) {
        fullUrl = fullUrl + '&keyword=' + keyword;
    }
    if (parent) {
        fullUrl = fullUrl + '&ParentId=' + parent;
    }
    firstPageLink.href = fullUrl;


    const iconChevronsLeft = document.createElement('em');
    iconChevronsLeft.className = "tf-icon bx bx-chevrons-right";
    firstPageLink.appendChild(iconChevronsLeft);
    firstPageItem.appendChild(firstPageLink);
    pagination.appendChild(firstPageItem);




    if (total <= 10) {
        for (start; start <= total; start++) {

            const pageItem = document.createElement('li');

            if (start == current) {
                pageItem.className = 'page-item active';
            } else {
                pageItem.className = 'page-item';
            }

            pageItem.className = 'page-item';
            const pageLink = document.createElement('a');
            pageLink.classList.add('page-link');

            var params = { CurentPage: start, PageSize: pageSize};
            var queryString = Object.keys(params).map(key => key + '=' + params[key]).join('&');
            var fullUrl = path + '?' + queryString;
            if (keyword) {
                fullUrl = fullUrl + '&keyword=' + keyword;
            }
            if (parent) {
                fullUrl = fullUrl + '&ParentId=' + parent;
            }

            pageLink.href = fullUrl;

            pageLink.innerText = start;
            pageItem.appendChild(pageLink);
            pagination.appendChild(pageItem);

        }
    } else {
        if (current == flag) {
            start = flag;
            if ((flag + 5) > total) {
                flag = total;
            } else {
                flag = flag + 5;
            }
        }
        for (start; start <= total; start++) {
            const pageItem = document.createElement('li');
            if (start == current) {
                pageItem.className = 'page-item active';
            } else {
                pageItem.className = 'page-item';
            }

            const pageLink = document.createElement('a');
            pageLink.classList.add('page-link');
            var params = { CurentPage: start, PageSize: pageSize };
            var queryString = Object.keys(params).map(key => key + '=' + params[key]).join('&');
            var fullUrl = path + '?' + queryString;
            if (keyword) {
                fullUrl = fullUrl + '&keyword=' + keyword;
            }
            if (parent) {
                fullUrl = fullUrl + '&ParentId=' + parent;
            }
            pageLink.href = fullUrl;
            pageLink.innerText = start;
            pageItem.appendChild(pageLink);
            pagination.appendChild(pageItem);
        }
    }

    const lastPageItem = document.createElement('li');
    lastPageItem.classList.add('page-item');
    const lastPageLink = document.createElement('a');
    lastPageLink.classList.add('page-link');
    var params = { CurentPage: start, PageSize: pageSize };
    var queryString = Object.keys(params).map(key => key + '=' + params[key]).join('&');
    var fullUrl = path + '?' + queryString;
    if (keyword) {
        fullUrl = fullUrl + '&keyword=' + keyword;
    }
    if (parent) {
        fullUrl = fullUrl + '&ParentId=' + parent;
    }
    lastPageLink.href = fullUrl;
    const iconChevronsRight = document.createElement('em');
    iconChevronsRight.className = "tf-icon bx bx-chevrons-left";
    lastPageLink.appendChild(iconChevronsRight);
    lastPageItem.appendChild(lastPageLink);
    pagination.appendChild(lastPageItem);

    return pagination;
}