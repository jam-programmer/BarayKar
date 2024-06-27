$(document).ready(function () {
    var url = window.location.href;
    let provinces = new URL(url).searchParams.getAll("province");
    let categories = new URL(url).searchParams.getAll("category");
    let cities = new URL(url).searchParams.getAll("city");
    let businesses = new URL(url).searchParams.getAll("business");
    provinces.forEach((item, index) => {
        var province = document.getElementById(item);
        province.checked = "checked";
    });
    cities.forEach((item, index) => {
        var city = document.getElementById(item);
        city.checked = "checked";
    });
    if (categories) {
        categories.forEach((item, index) => {
            var category = document.getElementById(item);
            category.checked = "checked";
        });
    }
    if (businesses) {
        businesses.forEach((item, index) => {
            var business = document.getElementById(item);
            business.checked = "checked";
        });
    }
});

function OptionQuery() {
    let businesses = document.getElementsByName("business");
    var categories = document.getElementsByName("category");
    var provinces = document.getElementsByName("province");
    var cities = document.getElementsByName("city");
    var url = window.location.href.split('?')[0];
    url = url + '?';

    cities.forEach((item) => {
        if (item.checked) {
            if (url.slice(-1) === '?') {
                url += "city=" + item.getAttribute('Id');
            } else {
                url += "&city=" + item.getAttribute('Id');
            }

        }
    });
    provinces.forEach((item) => {
        if (item.checked) {
            if (url.slice(-1) === '?') {
                url += "province=" + item.getAttribute('Id');
            } else {
                url += "&province=" + item.getAttribute('Id');
            }

        }
    });

    if (categories) {
        categories.forEach((item) => {
            if (item.checked) {
                if (url.slice(-1) === '?') {
                    url += "category=" + item.getAttribute('Id'); 
                } else {
                    url += "&category=" + item.getAttribute('Id');
                }

            }
        });
    }
    if (businesses) {
        businesses.forEach((item) => {
            if (item.checked) {
                if (url.slice(-1) === '?') {
                    url += "business=" + item.getAttribute('Id'); 
                } else {
                    url += "&business=" + item.getAttribute('Id');
                }

            }
        });
    }


  

    window.history.pushState({ path: url }, '', url);
    location.reload();
}
function SearchQuery() {
    const search = document.getElementById("search").value;

    if (search === '') {
        Swal.fire({
            title: "جستجو ناموفق",
            text: "برای جستجو یک عبارت بنویسید",
            icon: "info",
            confirmButtonText: "متوجه شدم"
        });
    } else {
        const url = new URL(window.location.href);

        if (url.searchParams.has('search')) {
            url.searchParams.set('search', search);
        } else {
            url.searchParams.append('search', search);
        }

        history.pushState({ path: url.href }, '', url.href);
        location.reload();
    }
}

function ClearQuery() {


  
    let currentUrl = window.location.href;


    let url = new URL(currentUrl);


    url.searchParams.delete('search');
    url.searchParams.delete('province');
    url.searchParams.delete('category');
    url.searchParams.delete('city');
    url.searchParams.delete('business');

    history.pushState(null, '', url.toString());


    location.reload();
}