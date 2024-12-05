//Header__fixed
$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop()) {
            $('header').addClass('sticky')
        }
    });
});

//Chi tiết sản phẩm
let bigImg = document.querySelector('.details__big-img img');
function showImg(pic) {
    bigImg.src = pic;
}

//Menu
// Lấy tất cả các menu trên trang
const optionMenus = document.querySelectorAll(".select-menu");

// Duyệt qua từng menu và thực hiện các thao tác tương ứng
optionMenus.forEach(optionMenu => {
    const selectBtn = optionMenu.querySelector(".select-btn");
    const options = optionMenu.querySelectorAll(".option");
    const sBtn_text = optionMenu.querySelector(".sBtn-text");

    selectBtn.addEventListener("click", () => optionMenu.classList.toggle("active"));

    options.forEach(option => {
        option.addEventListener("click", () => {
            let selectedOption = option.querySelector(".option-text").innerText;
            sBtn_text.innerText = selectedOption;
            optionMenu.classList.remove("active");
        });
    });
});


//Back top
$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop()) {
            $('#backtop').fadeIn();
        } else {
            $('#backtop').fadeOut();
        }
    });
    $("#backtop").click(function () {
        $('html, body').animate({
            scrollTop: 0
        }, 1000);
    });
});