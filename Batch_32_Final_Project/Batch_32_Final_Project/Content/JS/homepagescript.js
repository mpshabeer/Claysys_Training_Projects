// Initialize the index and display the initial slide
index = 1;
showslide(index);

// Function to display a specific slide
function slide(n)
{
showslide(index=n)
}
// Function to display the next or previous slide
function pluslide(n)
{
 showslide(index+=n)
}

// Function to control the display of slides and dots
function showslide(n) {
    let i;
    const slides = document.getElementsByClassName("imageslideshow");
    const dots = document.getElementsByClassName("dot");
    if (n > slides.length) {
        index = 1;
    }
    if (n < 1) {
        index = slides.length;
    }

    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < slides.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "")
    }
    slides[index - 1].style.display = "block";
    dots[index - 1].className += " active";
}   

