
        index=1;
        showslide(index);


        function slide(n){
            showslide(index=n)
        }
        function pluslide(n){
            showslide(index+=n)
        }

        function showslide(n){
            let i;
            const slides=document.getElementsByClassName("imageslideshow");
            const dots=document.getElementsByClassName("dot");
            if(n>slides.length){
                index=1;
            }
            if(n<1){
                index=slides.length;
            }

            for(i=0;i<slides.length;i++){
                slides[i].style.display="none";
            }
            for(i=0;i<slides.length;i++){
                dots[i].className=dots[i].className.replace(" active","")
            }
            slides[index-1].style.display="block";
            dots[index-1].className+=" active";
        }   

