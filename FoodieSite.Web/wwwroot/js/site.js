
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


const notyf = new Notyf({
    duration: 3000,
    position: {
        x: 'right',
        y: 'top',
    },
    types: [
        {
            type: 'success',
            dismissible: true
        },
        {
            type: 'warning',
            dismissible: true
        },
        {
            type: 'error',
            dismissible: true
        }
    ]
});
