$(document).ready(function () {
    var sortByPrice = false;
    var isAscending = true;
    var dropdownMenuButton = $('#dropdownMenuButton');
    var template = function (data) {

        return '<div class="margin-t20 col col-lg-6 col-sm-6 margin-bottom-15" > ' +
            '<div class="row">' +
            '<div class="col col-lg-4"><img class="image-100x100" src="../Content/cube-512.png" /></div>' +
            '<div class="col col-lg-8" > ' +
            '<label>Product Title</label>' +
            '<p>' + data.Title + ' (' + data.Id + ') description</p>' +
            '<button type="button"   data-id="' + data.Id + '" class="moreDetailsButton btn btn-primary">more details</button><br>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="margin-t20 col col-lg-6 col-sm-6  margin-bottom-15 border-line padding-top10">' +
            '<p>Price: ' + data.Price + ' <p>' +
            '<p>Availability: ' + data.Availability + '</p>' +
            '</div>';
    }

  

    var loadProductList = function () {

        $.ajax({
            url: 'Product/GetProducts?sortByPrice=' + sortByPrice + '&isAscending=' + isAscending,
            cache: true,
            contentType: false,
            processData: false,
            data: null,
            type: 'get',
            success: function (data) {

                var productContent = $('#productContent');
                productContent.html('');
                data.forEach(f => {
                    productContent.append(template(f));
                });


            }
        });
    }

    loadProductList();
  
    $('#ProductListContent').on('click', '#priceSelected', function () {
        sortByPrice = true;
        dropdownMenuButton.html("Price");
        loadProductList();
    }).on('click', '#popularitySelected', function () {
        sortByPrice = false;
        dropdownMenuButton.html("Popularity");
        loadProductList();
    }).on('click', '#ascendingLabel', function () {
        isAscending = true;
        loadProductList();
    }).on('click', '#descendingLabel', function () {
        isAscending = false;
        loadProductList();
    });

}).on('click', '.moreDetailsButton', function () {
    var $this = $(this).data('id');

    var $productDetailTemplate = $('#productDetailContent');

    $.ajax({
        url: 'Product/GetDetails?Id=' + $this,
        cache: true,
        contentType: false,
        processData: false,
        data: null,
        type: 'get',
        success: function (data) {

            $('#ProductListContent').addClass('hide');

            $productDetailTemplate.removeClass('hide')
            $productDetailTemplate.find('.productName').html('Name: ' + data.Title);
            $productDetailTemplate.find('.productDesc').html('Description: ' + data.Description);
            $productDetailTemplate.find('.productPrice').html('Price: ' + data.Price);

            var $ul = $productDetailTemplate.find('.productSpec').find('ul');


            data.Specs.forEach(f => {

                $ul.append('<li>' + f + '</li>');
            });

        }
    });

}).on('click', '.backButton', function () {

    
    var $productDetailTemplate = $('#productDetailContent');

    $productDetailTemplate.addClass('hide')
    $productDetailTemplate.find('.productName').html('');
    $productDetailTemplate.find('.productDesc').html('');
    $productDetailTemplate.find('.productPrice').html('');
    $productDetailTemplate.find('.productSpec').find('ul').html('');
    $('#ProductListContent').removeClass('hide');

});

