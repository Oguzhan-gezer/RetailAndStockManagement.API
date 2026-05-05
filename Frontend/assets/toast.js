/* ══════════════════════════════════════════════
   RetailStock — Toast & Confirm JS
   Drop-in replacement for alert() & confirm()
   ══════════════════════════════════════════════ */

(function () {
    // Create toast container
    if (!document.getElementById('rs-toast-container')) {
        const c = document.createElement('div');
        c.id = 'rs-toast-container';
        document.body.appendChild(c);
    }

    // ── Show Toast ──
    function showToast(message, duration = 4000) {
        const container = document.getElementById('rs-toast-container');
        if (!container) return;

        // Detect type from message
        let type = 'info';
        let title = 'Bilgi';
        let icon = 'ℹ️';
        const msg = String(message);

        if (msg.includes('✅') || msg.toLowerCase().includes('başarılı') || msg.toLowerCase().includes('eklendi') || msg.toLowerCase().includes('güncellendi')) {
            type = 'success'; title = 'Başarılı'; icon = '✅';
        } else if (msg.includes('❌') || msg.toLowerCase().includes('hata') || msg.toLowerCase().includes('başarısız') || msg.toLowerCase().includes('yetersiz') || msg.toLowerCase().includes('eklenemez')) {
            type = 'error'; title = 'Hata'; icon = '❌';
        } else if (msg.includes('⚠') || msg.toLowerCase().includes('uyarı') || msg.toLowerCase().includes('lütfen')) {
            type = 'warning'; title = 'Uyarı'; icon = '⚠️';
        }

        // Clean emojis from message for display
        const cleanMsg = msg.replace(/[✅❌⚠️🛒📌]/g, '').trim();

        const toast = document.createElement('div');
        toast.className = `rs-toast ${type}`;
        toast.innerHTML = `
            <div class="rs-toast-icon">${icon}</div>
            <div class="rs-toast-body">
                <div class="rs-toast-title">${title}</div>
                <div class="rs-toast-message">${cleanMsg}</div>
            </div>
            <button class="rs-toast-close" onclick="this.parentElement.classList.add('leaving');setTimeout(()=>this.parentElement.remove(),350);">✕</button>
            <div class="rs-toast-progress" style="animation-duration:${duration}ms;"></div>
        `;

        toast.addEventListener('click', function (e) {
            if (e.target.classList.contains('rs-toast-close')) return;
            toast.classList.add('leaving');
            setTimeout(() => toast.remove(), 350);
        });

        container.appendChild(toast);

        // Auto remove
        setTimeout(() => {
            if (toast.parentElement) {
                toast.classList.add('leaving');
                setTimeout(() => toast.remove(), 350);
            }
        }, duration);
    }

    // ── Custom Confirm ──
    function showConfirm(message) {
        return new Promise((resolve) => {
            const overlay = document.createElement('div');
            overlay.className = 'rs-confirm-overlay';

            const cleanMsg = String(message).replace(/[✅❌⚠️🛒📌]/g, '').trim();

            overlay.innerHTML = `
                <div class="rs-confirm-box">
                    <div class="rs-confirm-icon">❓</div>
                    <div class="rs-confirm-title">Onay Gerekli</div>
                    <div class="rs-confirm-message">${cleanMsg}</div>
                    <div class="rs-confirm-actions">
                        <button class="rs-confirm-btn rs-confirm-cancel" id="rsConfirmNo">İptal</button>
                        <button class="rs-confirm-btn rs-confirm-ok" id="rsConfirmYes">Evet</button>
                    </div>
                </div>
            `;

            document.body.appendChild(overlay);

            overlay.querySelector('#rsConfirmYes').addEventListener('click', () => {
                overlay.remove();
                resolve(true);
            });

            overlay.querySelector('#rsConfirmNo').addEventListener('click', () => {
                overlay.remove();
                resolve(false);
            });

            // Close on overlay click
            overlay.addEventListener('click', (e) => {
                if (e.target === overlay) {
                    overlay.remove();
                    resolve(false);
                }
            });
        });
    }

    // ── Override native alert ──
    window._nativeAlert = window.alert;
    window.alert = function (msg) {
        showToast(msg);
    };

    // ── Override native confirm ──
    // Note: Since confirm is synchronous but our replacement is async,
    // we expose rsConfirm for async usage. For synchronous confirm() calls
    // we fall back to native to avoid breaking existing code flow.
    window.rsConfirm = showConfirm;
    window.rsToast = showToast;
})();
