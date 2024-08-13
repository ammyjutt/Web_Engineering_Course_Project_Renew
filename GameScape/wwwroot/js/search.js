$(document).ready(function () {
    const $searchBar = $('#searchInput');
    const $searchForm = $('#searchForm');
    const $searchResults = $('#searchResults');

   

    $searchBar.on('input', function () {
        const query = $searchBar.val();
        if (query.length >= 3) { // Start searching after 3 characters
            $.ajax({
                url: $searchForm.data('url'),
                
                dataType: 'json',
                success: function (data) {
                    //console.log(data);
                    $searchResults.empty();
                    if (data.length > 0) {
                        $.each(data, function (index, item) {
                            let anchor = '<a class="dropdown-item" href="/Game/ShowSpecificProduct/' + item.id + '">' + item.name + '</a>';
                            $searchResults.append(anchor);
                        });
                        $searchResults.addClass('show');
                    } else {
                        $searchResults.removeClass('show');
                    }
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        } else {
            $searchResults.removeClass('show');
        }
    });
});