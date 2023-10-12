(function ($) {
 "use strict";
 
  /*=ALL DOCUMENT READY FUNCTION=*/
  $(document).ready(function(){
  	/* jQuery MOBILEMenu */
	   jQuery('nav#dropdown').meanmenu();
	/*---------------------------------
	 1. owl-carousel for all sp slides
	-----------------------------------*/
	$(".all-sp-slides").owlCarousel({
		autoPlay: false,
		center: true,
		items : 1, //1 items above 1000px browser width
		itemsDesktop : [1000,1], //5 items between 1000px and 901px
		itemsDesktopSmall : [900,1], // betweem 900px and 601px
		itemsTablet: [767,2],
		itemsMobile : [450,1], // itemsMobile disabled - inherit from itemsTablet option
		pagination:false,
		navigation:false,
		navigationText:["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"]
	});
	/*---------------------------------
	 2. owl-carousel for all sp slides
	-----------------------------------*/
	$(".home-2-all-sp-slides").owlCarousel({
		autoPlay: false,
		center: true,
		items : 1, //1 items above 1000px browser width
		itemsDesktop : [1000,1], //5 items between 1000px and 901px
		itemsDesktopSmall : [900,1], // betweem 900px and 601px
		itemsTablet: [767,2],
		itemsMobile : [450,1], // itemsMobile disabled - inherit from itemsTablet option
		pagination:false,
		navigation:true,
		navigationText:["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"]
	});
	/*-------------------------------
	 3. owl-carousel for new single
	--------------------------------*/
	$(".all-new-single").owlCarousel({
		autoPlay: false,
		center: true,
		items : 5, //1 items above 1000px browser width
		itemsDesktop : [1000,3], //5 items between 1000px and 901px
		itemsDesktopSmall : [900,3], // betweem 900px and 601px
		itemsTablet: [767,2],
		itemsMobile : [450,1], // itemsMobile disabled - inherit from itemsTablet option
		pagination:false,
		navigation:true,
		navigationText:["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"]
	});
	/*-------------------------------
	 4. owl-carousel for h2-new-single
	--------------------------------*/
	$(".h2-new-single").owlCarousel({
		autoPlay: false,
		center: true,
		items : 4, 
		itemsDesktop : [1199,3],
		itemsDesktopSmall : [979,3],
		itemsTablet: [767,2],
		itemsMobile : [450,1],		
		pagination:false,
		navigation:true,
		navigationText:["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"]
	});
	
	/*-------------------------------
	 5. owl-carousel for new single
	--------------------------------*/
	$(".mobile-p-slides").owlCarousel({
		autoPlay: false,
		center: true,
		items : 4, //1 items above 1000px browser width
		itemsDesktop : [1000,2], //5 items between 1000px and 901px
		itemsDesktopSmall : [900,2], // betweem 900px and 601px
		itemsTablet: [767,2],
		itemsMobile : [450,1], // itemsMobile disabled - inherit from itemsTablet option
		pagination:false,
		navigation:true,
		navigationText:["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"]
	});
	/*-------------------------------
	 6. owl-carousel for new single
	--------------------------------*/
	$(".all-brand").owlCarousel({
		autoPlay: false,
		center: true,
		items : 5, //1 items above 1000px browser width
		itemsDesktop : [1000,3], //5 items between 1000px and 901px
		itemsDesktopSmall : [900,3], // betweem 900px and 601px
		itemsTablet: [600,2], //1 items between 600 and 0
		itemsMobile : [600,1], // itemsMobile disabled - inherit from itemsTablet option
		pagination:false,
		navigation:true,
		navigationText:["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"]
	});
	/*---------------------------------------
	 7. owl-carousel for lattest-blog-bottom
	-----------------------------------------*/
	$(".lattest-blog-bottom").owlCarousel({
		autoPlay: false,
		center: true,
		items : 1, //1 items above 1000px browser width
		itemsDesktop : [1000,1], //5 items between 1000px and 901px
		itemsDesktopSmall : [900,1], // betweem 900px and 601px
		itemsTablet: [767,2],
		itemsMobile : [450,1], // itemsMobile disabled - inherit from itemsTablet option
		pagination:false,
		navigation:true,
		navigationText:["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"]
	});
	/*---------------------------------------
	 8. owl-carousel for lattest-blog-bottom
	-----------------------------------------*/
	$(".b-slide-all").owlCarousel({
		autoPlay: false,
		center: true,
		items : 1, //1 items above 1000px browser width
		itemsDesktop : [1000,1], //5 items between 1000px and 901px
		itemsDesktopSmall : [900,1], // betweem 900px and 601px
		itemsTablet: [767,2],
		itemsMobile : [450,1], // itemsMobile disabled - inherit from itemsTablet option
		pagination:false,
		navigation:true,
		navigationText:["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"]
	});
	/*---------------------------------------
	 9. owl-carousel for Testimonial
	-----------------------------------------*/
	$(".all-testimonil").owlCarousel({
		autoPlay: true,
		center: true,
		items : 1, //1 items above 1000px browser width
		itemsDesktop : [1000,1], //5 items between 1000px and 901px
		itemsDesktopSmall : [900,1], // betweem 900px and 601px
		itemsTablet: [767,2],
		itemsMobile : [450,1], // itemsMobile disabled - inherit from itemsTablet option
		transitionStyle : "backSlide",
		pagination:true,
		navigation:true,
		navigationText:["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"]
	});
	/*---------------------
	   scrollUp
	--------------------- */	
	$.scrollUp({
        scrollText: '<i class="fa fa-angle-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });
	/*---------------------
	   Countdown
	--------------------- */	
	// To change date, simply edit: var endDate = August 20, 2017 12:40:00";
	$(function() {
	  var endDate = "August 20, 2017 12:40:00";
	  $('.nrb-countdown .row').countdown({
		date: endDate,
		render: function(data) {
		  $(this.el).html('<div><div class="days"><span>' + this.leadingZeros(data.days, 2) + '</span><span>days</span></div><div class="hours"><span>' + this.leadingZeros(data.hours, 2) + '</span><span>hours</span></div></div><div class="nrb-countdown-ms"><div class="minutes"><span>' + this.leadingZeros(data.min, 2) + '</span><span>minutes</span></div><div class="seconds"><span>' + this.leadingZeros(data.sec, 2) + '</span><span>seconds</span></div></div>');
		}
	  });
	});	
	/*---------------------
	 For  mixItUp
	--------------------- */	
   $('#protfolios').mixItUp({
	animation: {
			effects: 'rotateY(-180deg)',
			duration: 1000,
		}
	});
	/*-------------------------
	Category Menu toggle function
	--------------------------*/
	$( '.nk-module-title' ).on('click', function() {
        $( '.nk-all-items' ).slideToggle(900);
     });
	//Category Menu
		$("#vina-treeview-virtuemart93 ul").treeview({
			animated: "normal",
			persist: "location",
			collapsed: true,
			unique: false,
		});
	/* For tooltip  */
	$('[data-toggle="tooltip"]').tooltip(); 
	/*---------------------------------
	 price slider
	-----------------------------------*/
  // $(function() {
		//$("#slider-range" ).slider({
		//	range: true,
		//	min: 0,
		//	max: 50000,
		//	values: [ 0, 45800 ],
		//	slide: function( event, ui ) {
		//		$( "#amount" ).val( "" + ui.values[ 0 ] + " - " + ui.values[ 1 ] );
		//	}
		//});
		//$( "#amount" ).val( "" + $( "#slider-range" ).slider( "values", 0 ) +
		//	" - " + $( "#slider-range" ).slider( "values", 1 ) );
	 //   });

	  $(function () {
		  $("#slider-range").slider({
			  range: true,
			  min: 0,
			  max: 50000,
			  values: [0, 45800],
			  slide: function (event, ui) {
				  $("#amount").val(ui.values[0] + " - " + ui.values[1]);
			  }
		  });
		  $("#amount").val($("#slider-range").slider("values", 0) +
			  " - " + $("#slider-range").slider("values", 1));
	  });
    /*---------------------
	Single  product Zoom
	--------------------- */
   $('.zoom_01').elevateZoom({
   	easing : true,
	cursor: "crosshair",
	zoomWindowFadeIn: 300,
	zoomWindowFadeOut: 350
   }); 
   /*-------------------------
	Active for fancybox
	--------------------------*/
	$('.fancybox').fancybox();
	/*---------------------
	 Input Number Incrementer
	--------------------- */
	$(function() {
	  $(".numbers-row").append('<div class="inc button">+</div><div class="dec button">-</div>');
	  $(".button").on("click", function() {
		var $button = $(this);
		var oldValue = $button.parent().find("input").val();
		if ($button.text() == "+") {
		  var newVal = parseFloat(oldValue) + 1;
		} else {
		   // Don't allow decrementing below zero
		  if (oldValue > 0) {
			var newVal = parseFloat(oldValue) - 1;
			} else {
			newVal = 0;
		  }
		  }
		$button.parent().find("input").val(newVal);

	  });
	});
	/*  Featured Products slider */
	$('.all-f-p').bxSlider({
	  mode: 'vertical',
	  slideWidth: 370,	
	  pager: false,  
      minSlides: 4,
	  maxSlides: 4, 	  
	  nextSelector: '#featured-next',
      prevSelector: '#featured-prev',
	  nextText: '<i class="fa fa-chevron-right"></i></i>',
	  prevText: '<i class="fa fa-chevron-left"></i>'
	});
	/* Best sellers  slider */
	$('.all-b-p').bxSlider({
	  mode: 'vertical',
	  slideWidth: 370,	
	  pager: false,  
      minSlides: 4,
	  maxSlides: 4, 	  
	  nextSelector: '#best-sellers-next',
      prevSelector: '#best-sellers-prev',
	  nextText: '<i class="fa fa-chevron-right"></i></i>',
	  prevText: '<i class="fa fa-chevron-left"></i>'
	}); 
    
});
/*---------------------
 PRELOADER
--------------------- */
$(window).load(function() { // makes sure the whole site is loaded
	$('#status').fadeOut(); // will first fade out the loading animation
	$('#loader-wrapper').delay(200).fadeOut('slow'); // will fade out the white DIV that covers the website.
	$('body').delay(100).css({'overflow-x':'hidden'});
})

})(jQuery);