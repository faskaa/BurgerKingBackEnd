document.addEventListener('DOMContentLoaded', function() {
    var buttons = document.querySelectorAll('.size .right button');
    const orderDiv = document.querySelector('.section-detail-add-order .add-order .order')
    const addButtons = document.querySelectorAll('.order .order-right button');
    const addOrderDiv = document.querySelector('.section-detail-add-order')


    buttons.forEach(function(button) {
        button.addEventListener('click', function() {
            buttons.forEach(function(btn) {
                btn.classList.remove('clicked');
            });

            button.classList.toggle('clicked');
        });

        addButtons.forEach(function(button){
            button.addEventListener('click', function(){
                orderDiv.classList.add('active')
                addOrderDiv.classList.add("clicked")
            })
        })

    });
    
});


function updateNumber(operation) {
    var numberElement = document.getElementById('numberDisplay');
    var decrementBtn = document.getElementById('decrementBtn');
    var currentNumber = parseInt(numberElement.innerText);

    if (operation === '+') {
        currentNumber++;
    } else if (operation === '-') {
        currentNumber = Math.max(1, currentNumber - 1);
    }

    numberElement.innerText = currentNumber;

    decrementBtn.disabled = (currentNumber === 1);
}

document.getElementById('incrementBtn').addEventListener('click', function() {
    updateNumber('+');
});

document.getElementById('decrementBtn').addEventListener('click', function() {
    updateNumber('-');
});



