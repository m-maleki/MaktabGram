// === Mobile Sidebar Toggle Logic ===
document.addEventListener('DOMContentLoaded', function() {
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

// === Table Search/Filter Logic ===
function filterTable() {
    const input = document.getElementById("user-search");
    if (!input) return;
    
    const filter = input.value.toUpperCase();
    const tableBody = document.getElementById("user-table-body");
    if (!tableBody) return;
    
    const rows = tableBody.getElementsByTagName("tr");

    // Define a function to convert Farsi/Arabic digits to English for consistent search
    function normalizeDigits(str) {
        if (typeof str !== 'string') return str;
        return str.replace(/[۰-۹]/g, d => '۰۱۲۳۴۵۶۷۸۹'.indexOf(d).toString())
                 .replace(/[٠-٩]/g, d => '٠١٢٣٤٥٦٧٨٩'.indexOf(d).toString());
    }

    for (let i = 0; i < rows.length; i++) {
        const cells = rows[i].getElementsByTagName("td");
        let match = false;

        // Loop through all cells
        for (let j = 0; j < cells.length; j++) {
            const cell = cells[j];
            if (cell) {
                // Normalize both cell text and filter input for better search matching
                const cellText = normalizeDigits(cell.textContent || cell.innerText);
                const normalizedFilter = normalizeDigits(filter);
                
                // Check if the cell content contains the filter text
                if (cellText.toUpperCase().indexOf(normalizedFilter) > -1) {
                    match = true;
                    break; // Found a match in this row
                }
            }
        }

        // Show or hide the row based on the match result
        if (match) {
            rows[i].style.display = "";
        } else {
            rows[i].style.display = "none";
        }
    }
}

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
            (currentPage.startsWith('settings') && href === 'settings.html')) {
            link.classList.add('active');
        }
    });
}

