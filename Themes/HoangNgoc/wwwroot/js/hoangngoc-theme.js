/**
 * HoangNgoc Theme JavaScript
 * Modern interactive features for OrchardCore CMS
 */

(function() {
    'use strict';

    // Theme namespace
    window.HoangNgoc = window.HoangNgoc || {};

    // Configuration
    const config = {
        breakpoints: {
            mobile: 768,
            tablet: 1024,
            desktop: 1200
        },
        animations: {
            duration: 300,
            easing: 'ease-in-out'
        }
    };

    // Utility functions
    const utils = {
        // Debounce function
        debounce: function(func, wait, immediate) {
            let timeout;
            return function executedFunction() {
                const context = this;
                const args = arguments;
                const later = function() {
                    timeout = null;
                    if (!immediate) func.apply(context, args);
                };
                const callNow = immediate && !timeout;
                clearTimeout(timeout);
                timeout = setTimeout(later, wait);
                if (callNow) func.apply(context, args);
            };
        },

        // Throttle function
        throttle: function(func, limit) {
            let inThrottle;
            return function() {
                const args = arguments;
                const context = this;
                if (!inThrottle) {
                    func.apply(context, args);
                    inThrottle = true;
                    setTimeout(() => inThrottle = false, limit);
                }
            };
        },

        // Get viewport width
        getViewportWidth: function() {
            return Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0);
        },

        // Check if element is in viewport
        isInViewport: function(element) {
            const rect = element.getBoundingClientRect();
            return (
                rect.top >= 0 &&
                rect.left >= 0 &&
                rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
                rect.right <= (window.innerWidth || document.documentElement.clientWidth)
            );
        },

        // Smooth scroll to element
        scrollTo: function(element, offset = 0) {
            const elementPosition = element.getBoundingClientRect().top;
            const offsetPosition = elementPosition + window.pageYOffset - offset;

            window.scrollTo({
                top: offsetPosition,
                behavior: 'smooth'
            });
        }
    };

    // Mobile Navigation
    const mobileNav = {
        init: function() {
            const toggle = document.querySelector('.mobile-menu-toggle');
            const nav = document.querySelector('.main-navigation');
            
            if (!toggle || !nav) return;

            toggle.addEventListener('click', function() {
                nav.classList.toggle('active');
                toggle.classList.toggle('active');
                document.body.classList.toggle('nav-open');
            });

            // Close nav when clicking outside
            document.addEventListener('click', function(e) {
                if (!nav.contains(e.target) && !toggle.contains(e.target)) {
                    nav.classList.remove('active');
                    toggle.classList.remove('active');
                    document.body.classList.remove('nav-open');
                }
            });

            // Close nav on window resize
            window.addEventListener('resize', utils.debounce(function() {
                if (utils.getViewportWidth() > config.breakpoints.mobile) {
                    nav.classList.remove('active');
                    toggle.classList.remove('active');
                    document.body.classList.remove('nav-open');
                }
            }, 250));
        }
    };

    // User Dropdown
    const userDropdown = {
        init: function() {
            const dropdowns = document.querySelectorAll('.user-dropdown');
            
            dropdowns.forEach(dropdown => {
                const toggle = dropdown.querySelector('.user-toggle');
                const menu = dropdown.querySelector('.user-dropdown-menu');
                
                if (!toggle || !menu) return;

                // Toggle dropdown on click
                toggle.addEventListener('click', function(e) {
                    e.preventDefault();
                    dropdown.classList.toggle('active');
                });

                // Close dropdown when clicking outside
                document.addEventListener('click', function(e) {
                    if (!dropdown.contains(e.target)) {
                        dropdown.classList.remove('active');
                    }
                });

                // Close dropdown on escape key
                document.addEventListener('keydown', function(e) {
                    if (e.key === 'Escape') {
                        dropdown.classList.remove('active');
                    }
                });
            });
        }
    };

    // Smooth Scrolling for Anchor Links
    const smoothScroll = {
        init: function() {
            const links = document.querySelectorAll('a[href^="#"]');
            
            links.forEach(link => {
                link.addEventListener('click', function(e) {
                    const href = this.getAttribute('href');
                    if (href === '#') return;
                    
                    const target = document.querySelector(href);
                    if (target) {
                        e.preventDefault();
                        utils.scrollTo(target, 80); // Account for sticky header
                    }
                });
            });
        }
    };

    // Sticky Header
    const stickyHeader = {
        init: function() {
            const header = document.querySelector('.site-header');
            if (!header) return;

            let lastScrollTop = 0;
            const headerHeight = header.offsetHeight;

            const handleScroll = utils.throttle(function() {
                const scrollTop = window.pageYOffset || document.documentElement.scrollTop;
                
                if (scrollTop > headerHeight) {
                    header.classList.add('scrolled');
                    
                    // Hide header on scroll down, show on scroll up
                    if (scrollTop > lastScrollTop && scrollTop > headerHeight * 2) {
                        header.classList.add('hidden');
                    } else {
                        header.classList.remove('hidden');
                    }
                } else {
                    header.classList.remove('scrolled', 'hidden');
                }
                
                lastScrollTop = scrollTop;
            }, 10);

            window.addEventListener('scroll', handleScroll);
        }
    };

    // Form Enhancements
    const formEnhancements = {
        init: function() {
            this.initFloatingLabels();
            this.initFormValidation();
            this.initFileUploads();
        },

        initFloatingLabels: function() {
            const formGroups = document.querySelectorAll('.form-group');
            
            formGroups.forEach(group => {
                const input = group.querySelector('.form-control');
                const label = group.querySelector('.form-label');
                
                if (!input || !label) return;

                // Add floating label class
                group.classList.add('floating-label');

                // Handle focus/blur events
                input.addEventListener('focus', () => {
                    group.classList.add('focused');
                });

                input.addEventListener('blur', () => {
                    if (!input.value) {
                        group.classList.remove('focused');
                    }
                });

                // Check initial value
                if (input.value) {
                    group.classList.add('focused');
                }
            });
        },

        initFormValidation: function() {
            const forms = document.querySelectorAll('form[data-validate]');
            
            forms.forEach(form => {
                form.addEventListener('submit', function(e) {
                    if (!this.checkValidity()) {
                        e.preventDefault();
                        e.stopPropagation();
                        
                        // Show validation errors
                        const invalidInputs = this.querySelectorAll(':invalid');
                        invalidInputs.forEach(input => {
                            input.classList.add('is-invalid');
                            
                            // Remove invalid class on input
                            input.addEventListener('input', function() {
                                this.classList.remove('is-invalid');
                            }, { once: true });
                        });
                    }
                    
                    this.classList.add('was-validated');
                });
            });
        },

        initFileUploads: function() {
            const fileInputs = document.querySelectorAll('input[type="file"]');
            
            fileInputs.forEach(input => {
                const wrapper = document.createElement('div');
                wrapper.className = 'file-upload-wrapper';
                
                const button = document.createElement('button');
                button.type = 'button';
                button.className = 'btn btn-outline file-upload-btn';
                button.innerHTML = '<i class="fas fa-upload"></i> Chọn file';
                
                const info = document.createElement('span');
                info.className = 'file-upload-info';
                info.textContent = 'Chưa chọn file nào';
                
                input.parentNode.insertBefore(wrapper, input);
                wrapper.appendChild(button);
                wrapper.appendChild(info);
                wrapper.appendChild(input);
                
                input.style.display = 'none';
                
                button.addEventListener('click', () => input.click());
                
                input.addEventListener('change', function() {
                    if (this.files.length > 0) {
                        const fileName = this.files[0].name;
                        info.textContent = fileName;
                        wrapper.classList.add('has-file');
                    } else {
                        info.textContent = 'Chưa chọn file nào';
                        wrapper.classList.remove('has-file');
                    }
                });
            });
        }
    };

    // Loading States
    const loadingStates = {
        init: function() {
            this.initButtonLoading();
            this.initPageLoading();
        },

        initButtonLoading: function() {
            const buttons = document.querySelectorAll('[data-loading]');
            
            buttons.forEach(button => {
                button.addEventListener('click', function() {
                    if (this.classList.contains('loading')) return;
                    
                    this.classList.add('loading');
                    const originalText = this.innerHTML;
                    this.innerHTML = '<span class="spinner"></span> Đang xử lý...';
                    
                    // Remove loading state after form submission or timeout
                    setTimeout(() => {
                        this.classList.remove('loading');
                        this.innerHTML = originalText;
                    }, 3000);
                });
            });
        },

        initPageLoading: function() {
            // Show loading on page navigation
            const links = document.querySelectorAll('a[href]:not([href^="#"]):not([target="_blank"])');
            
            links.forEach(link => {
                link.addEventListener('click', function(e) {
                    if (this.hostname !== window.location.hostname) return;
                    
                    const loader = document.createElement('div');
                    loader.className = 'page-loading';
                    loader.innerHTML = '<div class="spinner"></div>';
                    document.body.appendChild(loader);
                });
            });
        }
    };

    // Animations
    const animations = {
        init: function() {
            this.initScrollAnimations();
            this.initHoverEffects();
        },

        initScrollAnimations: function() {
            const elements = document.querySelectorAll('[data-animate]');
            
            const observer = new IntersectionObserver((entries) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        const element = entry.target;
                        const animation = element.dataset.animate || 'fade-in';
                        element.classList.add(animation);
                        observer.unobserve(element);
                    }
                });
            }, {
                threshold: 0.1,
                rootMargin: '0px 0px -50px 0px'
            });

            elements.forEach(element => {
                observer.observe(element);
            });
        },

        initHoverEffects: function() {
            const cards = document.querySelectorAll('.card, .btn');
            
            cards.forEach(card => {
                card.addEventListener('mouseenter', function() {
                    this.style.transform = 'translateY(-2px)';
                });
                
                card.addEventListener('mouseleave', function() {
                    this.style.transform = 'translateY(0)';
                });
            });
        }
    };

    // Search Enhancement
    const searchEnhancement = {
        init: function() {
            const searchInputs = document.querySelectorAll('input[type="search"], .search-input');
            
            searchInputs.forEach(input => {
                let searchTimeout;
                
                input.addEventListener('input', function() {
                    clearTimeout(searchTimeout);
                    const query = this.value.trim();
                    
                    if (query.length >= 2) {
                        searchTimeout = setTimeout(() => {
                            this.performSearch(query);
                        }, 300);
                    }
                });
            });
        },

        performSearch: function(query) {
            // Implement search functionality
            console.log('Searching for:', query);
        }
    };

    // Accessibility Enhancements
    const accessibility = {
        init: function() {
            this.initKeyboardNavigation();
            this.initAriaLabels();
            this.initFocusManagement();
        },

        initKeyboardNavigation: function() {
            // Handle escape key for modals and dropdowns
            document.addEventListener('keydown', function(e) {
                if (e.key === 'Escape') {
                    // Close any open dropdowns
                    document.querySelectorAll('.user-dropdown.active').forEach(dropdown => {
                        dropdown.classList.remove('active');
                    });
                    
                    // Close mobile menu
                    const nav = document.querySelector('.main-navigation.active');
                    if (nav) {
                        nav.classList.remove('active');
                        document.querySelector('.mobile-menu-toggle').classList.remove('active');
                        document.body.classList.remove('nav-open');
                    }
                }
            });
        },

        initAriaLabels: function() {
            // Add aria-labels to buttons without text
            const iconButtons = document.querySelectorAll('button:not([aria-label])');
            iconButtons.forEach(button => {
                if (button.querySelector('i') && !button.textContent.trim()) {
                    const icon = button.querySelector('i');
                    const classes = icon.className;
                    
                    if (classes.includes('fa-search')) {
                        button.setAttribute('aria-label', 'Tìm kiếm');
                    } else if (classes.includes('fa-menu')) {
                        button.setAttribute('aria-label', 'Menu');
                    } else if (classes.includes('fa-user')) {
                        button.setAttribute('aria-label', 'Tài khoản người dùng');
                    }
                }
            });
        },

        initFocusManagement: function() {
            // Trap focus in modals
            const modals = document.querySelectorAll('.modal');
            modals.forEach(modal => {
                const focusableElements = modal.querySelectorAll(
                    'button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])'
                );
                
                if (focusableElements.length === 0) return;
                
                const firstElement = focusableElements[0];
                const lastElement = focusableElements[focusableElements.length - 1];
                
                modal.addEventListener('keydown', function(e) {
                    if (e.key === 'Tab') {
                        if (e.shiftKey) {
                            if (document.activeElement === firstElement) {
                                lastElement.focus();
                                e.preventDefault();
                            }
                        } else {
                            if (document.activeElement === lastElement) {
                                firstElement.focus();
                                e.preventDefault();
                            }
                        }
                    }
                });
            });
        }
    };

    // Performance Optimizations
    const performance = {
        init: function() {
            this.lazyLoadImages();
            this.preloadCriticalResources();
        },

        lazyLoadImages: function() {
            const images = document.querySelectorAll('img[data-src]');
            
            const imageObserver = new IntersectionObserver((entries) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        const img = entry.target;
                        img.src = img.dataset.src;
                        img.classList.remove('lazy');
                        imageObserver.unobserve(img);
                    }
                });
            });

            images.forEach(img => {
                img.classList.add('lazy');
                imageObserver.observe(img);
            });
        },

        preloadCriticalResources: function() {
            // Preload critical CSS and fonts
            const criticalResources = [
                '/css/hoangngoc-theme.css',
                '/css/hoangngoc-responsive.css'
            ];

            criticalResources.forEach(resource => {
                const link = document.createElement('link');
                link.rel = 'preload';
                link.as = 'style';
                link.href = resource;
                document.head.appendChild(link);
            });
        }
    };

    // Initialize all modules
    const init = function() {
        // Wait for DOM to be ready
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', init);
            return;
        }

        // Initialize all modules
        mobileNav.init();
        userDropdown.init();
        smoothScroll.init();
        stickyHeader.init();
        formEnhancements.init();
        loadingStates.init();
        animations.init();
        searchEnhancement.init();
        accessibility.init();
        performance.init();

        // Add loaded class to body
        document.body.classList.add('theme-loaded');
        
        console.log('HoangNgoc Theme initialized successfully');
    };

    // Expose public API
    window.HoangNgoc.theme = {
        init: init,
        utils: utils,
        config: config
    };

    // Auto-initialize
    init();

})();