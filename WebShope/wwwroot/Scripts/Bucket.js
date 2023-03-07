function RemoveItemFromBucket(id) {
    var formData = new FormData();
    formData.append("id", id);

    $.ajax({
        url: '/Bucket/RemoveItemFromBucket',
        method: 'post',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            document.body.innerHTML = data;
        }
    });
}

function PutItemInToBucket(id, name, price, imageUrl) {

    var formData = new FormData();
    formData.append("id", id);
    formData.append("name", name);
    formData.append("price", price);
    formData.append("imageUrl", imageUrl);


    $.ajax({
        url: '/Bucket/PutItemInToBucket',
        method: 'post',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            $("#baсket").text('Корзина ' + data.count);
            alert("Товар добавлен в корзину.");
        }
    });
}