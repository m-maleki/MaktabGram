// UI Components و توابع مشترک برای مکتب‌گرام

// تابع برای نمایش الرت سفارشی
function showAlert(message, type = 'info', duration = 3000) {
    const alertTypes = {
        success: 'alert-success',
        error: 'alert-danger',
        warning: 'alert-warning',
        info: 'alert-info'
    };

    const alertDiv = document.createElement('div');
    alertDiv.className = `alert ${alertTypes[type]} fade-in`;
    alertDiv.style.cssText = 'position: fixed; top: 20px; left: 50%; transform: translateX(-50%); z-index: 9999; min-width: 300px;';
    alertDiv.textContent = message;
    
    document.body.appendChild(alertDiv);
    
    setTimeout(() => {
        alertDiv.style.opacity = '0';
        alertDiv.style.transform = 'translateX(-50%) translateY(-20px)';
        setTimeout(() => alertDiv.remove(), 300);
    }, duration);
}

// تابع برای تبدیل اعداد به فارسی
function toPersianNumber(num) {
    const persianDigits = '۰۱۲۳۴۵۶۷۸۹';
    return num.toString().replace(/\d/g, digit => persianDigits[digit]);
}

// تابع برای فرمت کردن اعداد با جداکننده
function formatNumber(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

// تابع برای نمایش/مخفی کردن منوی کاربری
function toggleUserMenu() {
    const menu = document.getElementById('user-menu');
    if (menu) {
        menu.classList.toggle('hidden');
    }
}

// بستن منو با کلیک خارج از آن
document.addEventListener('click', function(event) {
    const menu = document.getElementById('user-menu');
    const button = document.getElementById('user-menu-button');
    
    if (menu && button && !button.contains(event.target) && !menu.contains(event.target)) {
        menu.classList.add('hidden');
    }
});

// تابع برای نمایش/مخفی کردن سایدبار در موبایل
function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    if (sidebar) {
        sidebar.classList.toggle('-translate-x-full');
    }
}

// Event listeners برای سایدبار موبایل
document.addEventListener('DOMContentLoaded', function() {
    const openSidebar = document.getElementById('open-sidebar');
    const closeSidebar = document.getElementById('close-sidebar');
    const sidebar = document.getElementById('sidebar');
    
    if (openSidebar) {
        openSidebar.addEventListener('click', function() {
            if (sidebar) {
                sidebar.classList.remove('translate-x-full');
            }
        });
    }
    
    if (closeSidebar) {
        closeSidebar.addEventListener('click', function() {
            if (sidebar) {
                sidebar.classList.add('translate-x-full');
            }
        });
    }
    
    // بستن سایدبار با کلیک خارج از آن در موبایل
    document.addEventListener('click', function(event) {
        if (sidebar && window.innerWidth < 768) {
            if (!sidebar.contains(event.target) && !openSidebar?.contains(event.target)) {
                sidebar.classList.add('translate-x-full');
            }
        }
    });
    
    // اجرای lucide icons
    if (typeof lucide !== 'undefined') {
        lucide.createIcons();
    }
});

// تابع برای فیلتر کردن جدول
function filterTable() {
    const input = document.getElementById('user-search');
    const filter = input ? input.value.toLowerCase() : '';
    const table = document.getElementById('user-table');
    const tbody = document.getElementById('user-table-body');
    
    if (!tbody) return;
    
    const rows = tbody.getElementsByTagName('tr');
    
    for (let i = 0; i < rows.length; i++) {
        const row = rows[i];
        const cells = row.getElementsByTagName('td');
        let found = false;
        
        for (let j = 0; j < cells.length; j++) {
            const cell = cells[j];
            if (cell) {
                const textValue = cell.textContent || cell.innerText;
                if (textValue.toLowerCase().indexOf(filter) > -1) {
                    found = true;
                    break;
                }
            }
        }
        
        row.style.display = found ? '' : 'none';
    }
}

// تابع برای تایید حذف
function confirmDelete(message = 'آیا از حذف این مورد اطمینان دارید؟') {
    return confirm(message);
}

// تابع برای کپی کردن متن
function copyToClipboard(text) {
    const textarea = document.createElement('textarea');
    textarea.value = text;
    textarea.style.position = 'fixed';
    textarea.style.opacity = '0';
    document.body.appendChild(textarea);
    textarea.select();
    
    try {
        document.execCommand('copy');
        showAlert('متن کپی شد', 'success');
    } catch (err) {
        showAlert('خطا در کپی کردن', 'error');
    }
    
    document.body.removeChild(textarea);
}

// تابع برای اعتبارسنجی موبایل ایران
function validateIranianMobile(mobile) {
    const pattern = /^09[0-9]{9}$/;
    return pattern.test(mobile);
}

// تابع برای اعتبارسنجی ایمیل
function validateEmail(email) {
    const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return pattern.test(email);
}

// تابع برای نمایش/مخفی کردن رمز عبور
function togglePasswordVisibility(inputId, buttonId) {
    const input = document.getElementById(inputId);
    const button = document.getElementById(buttonId);
    
    if (input && button) {
        if (input.type === 'password') {
            input.type = 'text';
            button.innerHTML = '<svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.88 9.88l-3.29-3.29m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21"></path></svg>';
        } else {
            input.type = 'password';
            button.innerHTML = '<svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"></path><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"></path></svg>';
        }
    }
}

// تابع برای لودینگ
function showLoading(element) {
    if (element) {
        const originalContent = element.innerHTML;
        element.dataset.originalContent = originalContent;
        element.disabled = true;
        element.innerHTML = '<span class="spinner inline-block"></span> در حال پردازش...';
    }
}

function hideLoading(element) {
    if (element && element.dataset.originalContent) {
        element.disabled = false;
        element.innerHTML = element.dataset.originalContent;
    }
}

// تابع برای دوبل کلیک لایک (مخصوص پست‌ها)
function initDoubleTapLike() {
    const postMedias = document.querySelectorAll('.post-media-area');
    
    postMedias.forEach(media => {
        let tapCount = 0;
        let tapTimer;
        
        media.addEventListener('click', function(e) {
            tapCount++;
            
            if (tapCount === 1) {
                tapTimer = setTimeout(() => {
                    tapCount = 0;
                }, 300);
            } else if (tapCount === 2) {
                clearTimeout(tapTimer);
                tapCount = 0;
                
                // نمایش انیمیشن قلب
                const overlay = media.querySelector('.like-overlay');
                if (overlay) {
                    overlay.classList.add('show');
                    setTimeout(() => {
                        overlay.classList.remove('show');
                    }, 800);
                }
                
                // لایک کردن پست (اینجا باید API فراخوانی شود)
                const likeBtn = media.closest('article').querySelector('.like-btn');
                if (likeBtn) {
                    likeBtn.click();
                }
            }
        });
    });
}

// اجرای توابع در هنگام لود صفحه
document.addEventListener('DOMContentLoaded', function() {
    // اجرای دوبل کلیک لایک
    initDoubleTapLike();
    
    // اجرای Lucide Icons
    if (typeof lucide !== 'undefined') {
        lucide.createIcons();
    }
});

// تابع برای درخواست‌های AJAX
async function makeRequest(url, method = 'GET', data = null) {
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json',
        }
    };
    
    if (data && method !== 'GET') {
        options.body = JSON.stringify(data);
    }
    
    try {
        const response = await fetch(url, options);
        const result = await response.json();
        return result;
    } catch (error) {
        console.error('خطا در درخواست:', error);
        showAlert('خطا در برقراری ارتباط با سرور', 'error');
        return null;
    }
}

// Export توابع برای استفاده در سایر فایل‌ها
window.MaktabGram = {
    showAlert,
    toPersianNumber,
    formatNumber,
    toggleUserMenu,
    toggleSidebar,
    filterTable,
    confirmDelete,
    copyToClipboard,
    validateIranianMobile,
    validateEmail,
    togglePasswordVisibility,
    showLoading,
    hideLoading,
    makeRequest
};
