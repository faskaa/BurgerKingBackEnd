var buttons = document.querySelectorAll('.bottom .pick-up-type .left button');
const removeButton = document.querySelector('.order-edit .left .removebtn');
const timeButtons = document.querySelectorAll('.times .time button');
const driveThruBtn = document.querySelector('.bottom .pick-up-type .left #drive-thru') 
const pickUpBtn = document.querySelector('.bottom .pick-up-type .left #pick-up')  
const dineInBtn = document.querySelector('.bottom .pick-up-type .left #dine-in')  
const pickUpTime = document.querySelector('.pick-up-time')
const addOrders = document.querySelectorAll('.card-add-order .add-order .order .order-right button')
const divOrders = document.querySelectorAll('.card-add-order .add-order .order')
const addOrderDiv = document.querySelector('.card-add-order')



// divOrders.forEach(function(order){
//     addOrders.forEach(function(button) {
//         button.addEventListener('click', e=>{
//             order.classList.add("clicked")
//             addOrderDiv.classList.add("clicked")
//         })
//     })
    
// })




// driveThruBtn.addEventListener('click',e=>{
//     console.log("a");
//     pickUpTime.classList.add("selected-pick-up")
// })

// pickUpBtn.addEventListener('click',e=>{
//     console.log("pick");
//     pickUpTime.classList.remove("selected-pick-up")
// })

// dineInBtn.addEventListener('click',e=>{
//     console.log("dine");
//     pickUpTime.classList.remove("selected-pick-up")
// })

// buttons.forEach(function(button){

//     button.addEventListener('click',e=>{
//         console.log("Clicked");
        
//         buttons.forEach(function(btn){
//             btn.classList.remove("selected")
//         })

//         button.classList.toggle("selected")

//     })
// })













// function updateNumber(operation) {
//     var numberElement = document.getElementById('numberDisplay');
//     var decrementBtn = document.getElementById('decrementBtn');
//     var currentNumber = parseInt(numberElement.innerText);

//     if (operation === '+') {
//         currentNumber++;
//     } else if (operation === '-') {
//         currentNumber = Math.max(1, currentNumber - 1);
//     }

//     numberElement.innerText = currentNumber;

//     decrementBtn.disabled = (currentNumber === 1);
// }

// document.getElementById('incrementBtn').addEventListener('click', function() {
//     updateNumber('+');
// });

// document.getElementById('decrementBtn').addEventListener('click', function() {
//     updateNumber('-');
// });




// removeButton.addEventListener('click', e => {
//     Swal.fire({
//         title: 'Are you sure?',
//         text: 'You won\'t be able to revert this!',
//         icon: 'warning',
//         showCancelButton: true,
//         confirmButtonColor: '#3085d6',
//         cancelButtonColor: '#d33',
//         confirmButtonText: 'Yes, remove it!'
//     }).then((result) => {
//         if (result.isConfirmed) {
//             console.log('Remove Clicked');
//         }
//     });

// });

// timeButtons.forEach(function(button){
//     button.addEventListener('click', e=>{
//         console.log("Time button clicked");
//         timeButtons.forEach(function(btn){
//             btn.classList.remove("selected")
//         })
//         button.classList.toggle("selected")
//     })
// })


// function selectButton(selectedButton) {
//     var buttons = document.querySelectorAll('.times input[type="button"]');
//     buttons.forEach(function(button) {
//         button.classList.remove('selected');
//     });

//     selectedButton.classList.add('selected');
// }




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
                order.classList.add("removed");
                console.log("remove clicked");
            }
        })
    })
});


