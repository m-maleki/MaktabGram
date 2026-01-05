// UI Components for modern sidebar and header
document.addEventListener('DOMContentLoaded', function() {
    // Sidebar toggle for mobile
    const sidebar = document.getElementById('sidebar');
    const openBtn = document.getElementById('open-sidebar');
    const closeBtn = document.getElementById('close-sidebar');

    if (openBtn) {
        openBtn.addEventListener('click', () => {
            sidebar.classList.add('open');
        });
    }

    if (closeBtn) {
        closeBtn.addEventListener('click', () => {
            sidebar.classList.remove('open');
        });
    }

    // Close sidebar when clicking outside on mobile
    const main = document.querySelector('main');
    if (main) {
        main.addEventListener('click', (e) => {
            if (sidebar && sidebar.classList.contains('open') && window.innerWidth < 768) {
                if (!sidebar.contains(e.target) && e.target !== openBtn) {
                    sidebar.classList.remove('open');
                }
            }
        });
    }

    // Set active navigation link
    setActiveNavLink();
});

// Set active navigation link based on current page
function setActiveNavLink() {
    const currentPage = window.location.pathname.split('/').pop() || 'index.html';
    const navLinks = document.querySelectorAll('nav a');
    
    navLinks.forEach(link => {
        const href = link.getAttribute('href');
        if (href === currentPage || 
            (currentPage === 'index.html' && href === 'dashboard.html') ||
            (currentPage.startsWith('dashboard') && href === 'dashboard.html') ||
            (currentPage.startsWith('forms') && href === 'forms.html') ||
            (currentPage.startsWith('tables') && href === 'tables.html') ||
            (currentPage.startsWith('charts') && href === 'charts.html') ||
            (currentPage.startsWith('components') && href === 'components.html') ||
            (currentPage.startsWith('settings') && href === 'settings.html')) {
            link.classList.add('active-nav');
        }
    });
}

// User dropdown menu
function toggleUserMenu() {
    const menu = document.getElementById('user-menu');
    if (menu) {
        menu.classList.toggle('hidden');
        if (!menu.classList.contains('hidden')) {
            menu.classList.add('animate-fadeIn');
        }
    }
}

// Close user menu when clicking outside
document.addEventListener('click', function(event) {
    const userButton = document.getElementById('user-menu-button');
    const userMenu = document.getElementById('user-menu');
    
    if (userButton && userMenu && !userButton.contains(event.target) && !userMenu.contains(event.target)) {
        userMenu.classList.add('hidden');
    }
});

