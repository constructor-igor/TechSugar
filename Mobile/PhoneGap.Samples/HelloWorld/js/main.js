var app = {

/*
    findByName: function() {
        console.log('findByName');
        this.store.findByName($('.search-key').val(), function(employees) {
            var l = employees.length;
            var e;
            $('.employee-list').empty();
            for (var i=0; i<l; i++) {
                e = employees[i];
                $('.employee-list').append('<li><a href="#employees/' + e.id + '">' + e.firstName + ' ' + e.lastName + '</a></li>');
            }
        });
    },
*/
/*	
	findByName: function() {
		var self = this;
		this.store.findByName($('.search-key').val(), function(employees) {
			$('.employee-list').html(self.employeeLiTpl(employees));
		});
	},
*/
    initialize: function() {
    	var self = this;
    	this.detailsURL = /^#employees\/(\d{1,})/;
    	this.registerEvents();
    	this.store = new MemoryStore(function() {
        	self.route();
    	});


/*    	
		var self = this;
        this.store = new MemoryStore(function() {
			//self.showAlert('Store Initialized', 'Info');
			self.renderHomeView();
		});
		
		this.homeTpl = Handlebars.compile($("#home-tpl").html());
		this.employeeLiTpl = Handlebars.compile($("#employee-li-tpl").html());

        $('.search-key').on('keyup', $.proxy(this.findByName, this));
		this.findByName();
*/		
    },

	showAlert: function (message, title) {
		if (navigator.notification) {
			navigator.notification.alert(message, null, title, 'OK');
		} else {
			alert(title ? (title + ": " + message) : message);
		}
	},

	registerEvents: function() {
    	var self = this;
    	$(window).on('hashchange', $.proxy(this.route, this));
    	// Check of browser supports touch events...
    	if (document.documentElement.hasOwnProperty('ontouchstart')) {
        	// ... if yes: register touch event listener to change the "selected" state of the item
        	$('body').on('touchstart', 'a', function(event) {
            	$(event.target).addClass('tappable-active');
        	});
        	$('body').on('touchend', 'a', function(event) {
            	$(event.target).removeClass('tappable-active');
        	});
    	} else {
        	// ... if not: register mouse events instead
        	$('body').on('mousedown', 'a', function(event) {
            	$(event.target).addClass('tappable-active');
        	});
        	$('body').on('mouseup', 'a', function(event) {
            	$(event.target).removeClass('tappable-active');
        	});
    	}
	},

	route: function() {
    	var hash = window.location.hash;
    	if (!hash) {
        	$('body').html(new HomeView(this.store).render().el);
        	return;
    	}
    	var match = hash.match(app.detailsURL);
    	if (match) {
        	this.store.findById(Number(match[1]), function(employee) {
            	$('body').html(new EmployeeView(employee).render().el);
        	});
    	}
	}


/*	
	renderHomeView: function() {
		$('body').html(this.homeTpl());
		$('.search-key').on('keyup', $.proxy(this.findByName, this));
	}
*/
};

app.initialize();