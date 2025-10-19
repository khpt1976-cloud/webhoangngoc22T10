/**
 * HoangNgoc Professional Theme v2.0 JavaScript
 * Bootstrap 5.3 Enhanced Interactive Features for OrchardCore CMS
 * Compatible with Bootstrap 5.3 components and utilities
 */

(function() {
    'use strict';

    // Theme namespace
    window.HoangNgoc = window.HoangNgoc || {};

    // Configuration - Bootstrap 5.3 Breakpoints
    const config = {
        breakpoints: {
            xs: 0,
            sm: 576,
            md: 768,
            lg: 992,
            xl: 1200,
            xxl: 1400
        },
        animations: {
            duration: 300,
            easing: 'cubic-bezier(0.4, 0, 0.2, 1)',
            bounce: 'cubic-bezier(0.68, -0.55, 0.265, 1.55)'
        },
        theme: {
            name: 'HoangNgoc Professional',
            version: '2.0.0',
            bootstrap: '5.3.2'
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

    // Bootstrap 5.3 Enhanced Mobile Navigation
    const mobileNav = {
        init: function() {
            const toggle = document.querySelector('.navbar-toggler');
            const nav = document.querySelector('#navbarNav');
            
            if (!toggle || !nav) return;

            // Use Bootstrap 5.3 Collapse API
            const bsCollapse = new bootstrap.Collapse(nav, {
                toggle: false
            });

            toggle.addEventListener('click', function() {
                bsCollapse.toggle();
                this.classList.toggle('active');
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

    // Advanced Theme Features
    const advancedFeatures = {
        init: function() {
            this.initScrollReveal();
            this.initAdvancedAnimations();
            this.initPerformanceMonitoring();
        },

        // Scroll Reveal Animation
        initScrollReveal: function() {
            const observerOptions = {
                threshold: 0.1,
                rootMargin: '0px 0px -50px 0px'
            };

            const observer = new IntersectionObserver((entries) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        entry.target.classList.add('revealed');
                        observer.unobserve(entry.target);
                    }
                });
            }, observerOptions);

            // Observe scroll reveal elements
            document.querySelectorAll('.scroll-reveal, .scroll-reveal-left, .scroll-reveal-right').forEach(el => {
                observer.observe(el);
            });
        },

        // Advanced Animations
        initAdvancedAnimations: function() {
            // Stagger animations
            const staggerGroups = document.querySelectorAll('[data-stagger]');
            
            staggerGroups.forEach(group => {
                const children = group.children;
                const delay = parseInt(group.dataset.stagger) || 100;
                
                Array.from(children).forEach((child, index) => {
                    child.style.animationDelay = `${index * delay}ms`;
                    child.classList.add('animate-slide-in-left');
                });
            });

            // Mouse follow effects
            const followElements = document.querySelectorAll('[data-mouse-follow]');
            
            followElements.forEach(element => {
                element.addEventListener('mousemove', (e) => {
                    const rect = element.getBoundingClientRect();
                    const x = e.clientX - rect.left;
                    const y = e.clientY - rect.top;
                    
                    const centerX = rect.width / 2;
                    const centerY = rect.height / 2;
                    
                    const rotateX = (y - centerY) / 10;
                    const rotateY = (centerX - x) / 10;
                    
                    element.style.transform = `perspective(1000px) rotateX(${rotateX}deg) rotateY(${rotateY}deg)`;
                });
                
                element.addEventListener('mouseleave', () => {
                    element.style.transform = 'perspective(1000px) rotateX(0deg) rotateY(0deg)';
                });
            });
        },

        // Performance Monitoring
        initPerformanceMonitoring: function() {
            if (!window.performance) return;

            // Core Web Vitals monitoring
            this.monitorCoreWebVitals();
            
            // Resource loading monitoring
            this.monitorResourceLoading();
            
            // Memory usage monitoring
            this.monitorMemoryUsage();

            window.addEventListener('load', () => {
                setTimeout(() => {
                    const perfData = performance.getEntriesByType('navigation')[0];
                    if (perfData) {
                        const metrics = {
                            loadTime: Math.round(perfData.loadEventEnd - perfData.loadEventStart),
                            domContentLoaded: Math.round(perfData.domContentLoadedEventEnd - perfData.domContentLoadedEventStart),
                            firstByte: Math.round(perfData.responseStart - perfData.requestStart),
                            theme: config.theme.version
                        };
                        
                        console.log(`🚀 ${config.theme.name} Performance:`, metrics);
                        
                        // Send to analytics if available
                        if (window.gtag) {
                            window.gtag('event', 'theme_performance', {
                                custom_parameter: metrics.loadTime
                            });
                        }
                    }
                }, 0);
            });
        },

        // Core Web Vitals monitoring
        monitorCoreWebVitals: function() {
            // Largest Contentful Paint (LCP)
            if ('PerformanceObserver' in window) {
                const lcpObserver = new PerformanceObserver((list) => {
                    const entries = list.getEntries();
                    const lastEntry = entries[entries.length - 1];
                    console.log('LCP:', Math.round(lastEntry.startTime), 'ms');
                });
                lcpObserver.observe({ entryTypes: ['largest-contentful-paint'] });

                // First Input Delay (FID)
                const fidObserver = new PerformanceObserver((list) => {
                    const entries = list.getEntries();
                    entries.forEach(entry => {
                        console.log('FID:', Math.round(entry.processingStart - entry.startTime), 'ms');
                    });
                });
                fidObserver.observe({ entryTypes: ['first-input'] });

                // Cumulative Layout Shift (CLS)
                let clsValue = 0;
                const clsObserver = new PerformanceObserver((list) => {
                    const entries = list.getEntries();
                    entries.forEach(entry => {
                        if (!entry.hadRecentInput) {
                            clsValue += entry.value;
                        }
                    });
                    console.log('CLS:', clsValue.toFixed(4));
                });
                clsObserver.observe({ entryTypes: ['layout-shift'] });
            }
        },

        // Resource loading monitoring
        monitorResourceLoading: function() {
            window.addEventListener('load', () => {
                const resources = performance.getEntriesByType('resource');
                const slowResources = resources.filter(resource => resource.duration > 1000);
                
                if (slowResources.length > 0) {
                    console.warn('Slow loading resources:', slowResources.map(r => ({
                        name: r.name,
                        duration: Math.round(r.duration)
                    })));
                }
            });
        },

        // Memory usage monitoring
        monitorMemoryUsage: function() {
            if ('memory' in performance) {
                setInterval(() => {
                    const memory = performance.memory;
                    const memoryInfo = {
                        used: Math.round(memory.usedJSHeapSize / 1048576), // MB
                        total: Math.round(memory.totalJSHeapSize / 1048576), // MB
                        limit: Math.round(memory.jsHeapSizeLimit / 1048576) // MB
                    };
                    
                    // Warn if memory usage is high
                    if (memoryInfo.used > 50) {
                        console.warn('High memory usage:', memoryInfo);
                    }
                }, 30000); // Check every 30 seconds
            }
        }
    };

    // Performance optimization utilities
    const performanceUtils = {
        // Lazy loading implementation
        initLazyLoading: function() {
            const lazyElements = document.querySelectorAll('.lazy-load');
            
            if ('IntersectionObserver' in window) {
                const lazyObserver = new IntersectionObserver((entries) => {
                    entries.forEach(entry => {
                        if (entry.isIntersecting) {
                            const element = entry.target;
                            
                            // Load images
                            if (element.dataset.src) {
                                element.src = element.dataset.src;
                                element.removeAttribute('data-src');
                            }
                            
                            // Load background images
                            if (element.dataset.bg) {
                                element.style.backgroundImage = `url(${element.dataset.bg})`;
                                element.removeAttribute('data-bg');
                            }
                            
                            element.classList.add('loaded');
                            lazyObserver.unobserve(element);
                        }
                    });
                });
                
                lazyElements.forEach(element => lazyObserver.observe(element));
            } else {
                // Fallback for older browsers
                lazyElements.forEach(element => {
                    if (element.dataset.src) {
                        element.src = element.dataset.src;
                    }
                    if (element.dataset.bg) {
                        element.style.backgroundImage = `url(${element.dataset.bg})`;
                    }
                    element.classList.add('loaded');
                });
            }
        },

        // Debounce function for performance
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

        // Throttle function for performance
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

        // Preload critical resources
        preloadResources: function() {
            const criticalResources = [
                { href: 'https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css', as: 'style' },
                { href: 'https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css', as: 'style' }
            ];

            criticalResources.forEach(resource => {
                const link = document.createElement('link');
                link.rel = 'preload';
                link.href = resource.href;
                link.as = resource.as;
                if (resource.as === 'style') {
                    link.onload = function() {
                        this.onload = null;
                        this.rel = 'stylesheet';
                    };
                }
                document.head.appendChild(link);
            });
        },

        // Critical CSS inlining
        inlineCriticalCSS: function() {
            const criticalCSS = `
                .hero-critical { min-height: 50vh; background: var(--hn-primary); color: white; display: flex; align-items: center; justify-content: center; }
                .nav-critical { background: white; box-shadow: 0 2px 4px rgba(0,0,0,0.1); position: sticky; top: 0; z-index: 1000; }
            `;
            
            const style = document.createElement('style');
            style.textContent = criticalCSS;
            document.head.insertBefore(style, document.head.firstChild);
        },

        // Service Worker registration for caching
        registerServiceWorker: function() {
            if ('serviceWorker' in navigator) {
                window.addEventListener('load', () => {
                    navigator.serviceWorker.register('/sw.js')
                        .then(registration => {
                            console.log('SW registered: ', registration);
                        })
                        .catch(registrationError => {
                            console.log('SW registration failed: ', registrationError);
                        });
                });
            }
        },

        // Image optimization
        optimizeImages: function() {
            const images = document.querySelectorAll('img:not([loading])');
            images.forEach(img => {
                img.loading = 'lazy';
                img.decoding = 'async';
            });
        },

        // Font loading optimization
        optimizeFontLoading: function() {
            // Preload critical fonts
            const fontPreloads = [
                'https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap',
                'https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700&display=swap'
            ];

            fontPreloads.forEach(fontUrl => {
                const link = document.createElement('link');
                link.rel = 'preload';
                link.href = fontUrl;
                link.as = 'style';
                link.onload = function() {
                    this.onload = null;
                    this.rel = 'stylesheet';
                };
                document.head.appendChild(link);
            });
        }
    };

    // Resource hints for better performance
    const resourceHints = {
        init: function() {
            this.addDNSPrefetch();
            this.addPreconnect();
        },

        addDNSPrefetch: function() {
            const domains = [
                'fonts.googleapis.com',
                'fonts.gstatic.com',
                'cdn.jsdelivr.net',
                'cdnjs.cloudflare.com'
            ];

            domains.forEach(domain => {
                const link = document.createElement('link');
                link.rel = 'dns-prefetch';
                link.href = `//${domain}`;
                document.head.appendChild(link);
            });
        },

        addPreconnect: function() {
            const origins = [
                'https://fonts.googleapis.com',
                'https://fonts.gstatic.com'
            ];

            origins.forEach(origin => {
                const link = document.createElement('link');
                link.rel = 'preconnect';
                link.href = origin;
                link.crossOrigin = 'anonymous';
                document.head.appendChild(link);
            });
        }
    };

    // Theme API for external use
    window.HoangNgoc.api = {
        // Animation utilities
        animate: function(element, animation, duration = 600) {
            return new Promise((resolve) => {
                element.classList.add(`animate-${animation}`);
                setTimeout(() => {
                    element.classList.remove(`animate-${animation}`);
                    resolve();
                }, duration);
            });
        },

        // Notification system
        notify: function(message, type = 'info', duration = 5000) {
            const notification = document.createElement('div');
            notification.className = `alert alert-${type} position-fixed top-0 end-0 m-3 animate-slide-in-right`;
            notification.style.zIndex = '9999';
            notification.innerHTML = `
                <div class="d-flex align-items-center">
                    <i class="fas fa-info-circle me-2"></i>
                    <span>${message}</span>
                    <button type="button" class="btn-close ms-auto" aria-label="Close"></button>
                </div>
            `;

            document.body.appendChild(notification);

            // Auto remove
            setTimeout(() => {
                notification.classList.add('animate-slide-out-right');
                setTimeout(() => notification.remove(), 300);
            }, duration);

            // Manual close
            notification.querySelector('.btn-close').addEventListener('click', () => {
                notification.classList.add('animate-slide-out-right');
                setTimeout(() => notification.remove(), 300);
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

        console.log(`🚀 ${config.theme.name} v${config.theme.version} initialized`);
        console.log(`📦 Bootstrap ${config.theme.bootstrap} compatible`);

        // Initialize core modules
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
        
        // Initialize advanced features
        advancedFeatures.init();
        
        // Initialize performance optimizations
        performanceUtils.initLazyLoading();
        performanceUtils.optimizeImages();
        performanceUtils.optimizeFontLoading();
        resourceHints.init();

        // Add loaded class to body
        document.body.classList.add('theme-loaded');
        
        console.log('✨ Advanced theme features loaded successfully!');
        console.log('⚡ Performance optimizations enabled!');
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