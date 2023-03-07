$(document).ready(function () {


    $('#personal_form').validate({
        rules: {
            name:
            {
                required: true,
                minlength: 2,
                maxlength: 20
            },
            sname:
            {
                required: true,
                minlength: 2,
                maxlength: 30
            },
            age:
            {
                required: true,
                range: [1, 100]
            }
        },
        messages: {
            name: {
                required: 'This field is required',
                minlength: 'Small name, change',
                maxlength: 'The name is too big, change it'
            },
            sname: {
                required: 'This field is required',
                minlength: 'Small surname, change',
                maxlength: 'The surname is too big, change it'
            },
            age: {
                required: 'This field is required',
                range: 'The age should be from 1 to 100'
                }
        },
        submitHandler: function (form) {
            $.blockUI({
                message: '<div class="loader"></div>',
                css: {
                    padding: 0,
                    margin: 0,
                    width: 60,
                    left: '45%',
                    top: '45%',
                    textAlign: 'center',
                    border: 'none',
                    background:'inherit'

                }
            }

            );

            $.ajax({
                url: '/Profile/SavePersonal',         
                method: 'post',
                success: function () {
                    $.unblockUI();           
                }
            });
        }
    });
});