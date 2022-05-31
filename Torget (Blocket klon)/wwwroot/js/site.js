let slideIndex = 0;

const arrowRight = document.getElementById("arrow-right")
const arrowLeft = document.getElementById("arrow-left")
const heroImageDiv = document.getElementById("hero-img")

let activeImg = document.getElementById(`small-pic-${slideIndex}`); 
activeImg.setAttribute("class", "img-active");

heroImageDiv.src = `${activeImg.src}`;
let numberOfPics = document.getElementById("small-pics").childElementCount -2; //För att det är två divar



arrowLeft.addEventListener("click", () => {

    if (slideIndex != 0) {
        activeImg.classList.remove('img-active');
        slideIndex -= 1;
        activeImg = document.getElementById(`small-pic-${slideIndex}`);
        activeImg.setAttribute("class", "img-active");
        heroImageDiv.src = `${activeImg.src}`;
    }
   
   })

arrowRight.addEventListener("click", () => {

    if (slideIndex < numberOfPics-1) { 

        activeImg.classList.remove('img-active');
        slideIndex += 1;
        activeImg = document.getElementById(`small-pic-${slideIndex}`);
        activeImg.setAttribute("class", "img-active");
        heroImageDiv.src = `${activeImg.src}`;
    }
})

