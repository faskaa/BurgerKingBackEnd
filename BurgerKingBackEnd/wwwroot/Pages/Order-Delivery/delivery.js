function updateNumber(operation, order) {
    var numberElement = order.querySelector('.display');
    var decrementBtn = order.querySelector('.decrement');
    var currentNumber = parseInt(numberElement.innerHTML);

    if (operation === '+') {
        currentNumber++;
    }

    if (operation === '-') {
        currentNumber = Math.max(1, currentNumber - 1);
    }

    numberElement.innerHTML = currentNumber;
    decrementBtn.disabled = (currentNumber === 1);
}

var orders = document.querySelectorAll('.orders .order');
orders.forEach(function (order) {
    var incrementBtn = order.querySelector('.increment');
    var decrementBtn = order.querySelector('.decrement');

    incrementBtn.addEventListener('click', function () {
        updateNumber('+', order);
    });

    decrementBtn.addEventListener('click', function () {
        updateNumber('-', order);
    });

    var removeBtn = order.querySelector('.remove');
    removeBtn.addEventListener('click', e=>{
        Swal.fire({
            title: 'Are you sure?',
            text: 'You won\'t be able to revert this!',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, remove it!'
        }).then((result)=>{
            if(result.isConfirmed){
                order.style.display = 'none';
                console.log("remove clicked");
            }
        })
    })
});



