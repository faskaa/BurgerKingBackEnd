const locationElements = document.querySelectorAll('.location-locations .bottom .location');

locationElements.forEach(function(location) {
    location.addEventListener('click', function() {
        this.classList.toggle('active');

        locationElements.forEach(function(element) {
            if (element !== this && element.classList.contains('active')) {
                element.classList.remove('active');
            }
        }, this);
    });
});

locationElements.forEach(function(location) {
    const heartButton = location.querySelector('.location-top .right button');

    heartButton.addEventListener('click', function(e) {
        e.stopPropagation(); 

        this.classList.toggle('active');

        const heartImgs = this.querySelectorAll('img');
        heartImgs.forEach(function(img) {
            img.classList.toggle('active');
        });
    });
});



        function showNearby() {
            document.querySelector('.nearby-content').style.display = 'block';
            document.querySelector('.favorites-content').style.display = 'none';
            document.querySelector('.recents-content').style.display = 'none';
        }

        function showFavorites() {
            document.querySelector('.favorites-content').style.display = 'block';
            document.querySelector('.nearby-content').style.display = 'none';
            document.querySelector('.recents-content').style.display = 'none';
        }

        function showRecents() {
            document.querySelector('.recents-content').style.display = 'block';
            document.querySelector('.nearby-content').style.display = 'none';
            document.querySelector('.favorites-content').style.display = 'none';
        }




const menuBtns = document.querySelectorAll('.location-locations .locations-menu button');
menuBtns.forEach(function(button){
    button.addEventListener('click', e=>{
        menuBtns.forEach(function(btn){
            btn.classList.remove("active")
    
        })
        console.log("a");
        button.classList.toggle("active")
        
        
    })
})


document.getElementById('searchForm').addEventListener('submit', function (event) {
    console.log('Form submit event triggered');
    event.preventDefault();
    submitForm();
});

document.getElementById('searchInput').addEventListener('keyup', function(event) {
    console.log('Keyup event triggered');
    if (event.key === 'Enter') {
        submitForm();
    }
});

function submitForm() {
    console.log('Submitting form');
    document.getElementById('searchForm').submit();
}



document.querySelectorAll('.location-top .left').forEach(function(leftElement) {
    leftElement.addEventListener('click', function() {
      var locationElement = leftElement.closest('.location');

      var locationBottomElement = locationElement.querySelector('.location-bottom');

      locationBottomElement.classList.toggle('clicked');
    });
  });


  document.addEventListener('DOMContentLoaded', function() {
    var locationButtons = document.querySelectorAll('.location button');
  
    locationButtons.forEach(function(button) {
      button.addEventListener('click', function() {
        var images = button.querySelectorAll('img');
  
        images.forEach(function(img) {
          img.classList.toggle('active');
        });
      });
    });
  });