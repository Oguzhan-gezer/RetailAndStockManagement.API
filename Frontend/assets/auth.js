const API_BASE = 'https://localhost:7232';

const Auth = {
    getToken: () => localStorage.getItem('token'),
    
    getUser: () => {
        const token = localStorage.getItem('token');
        if (!token) return null;
        try {
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const binaryStr = atob(base64);
            const bytes = Uint8Array.from(binaryStr, c => c.charCodeAt(0));
            const payload = JSON.parse(new TextDecoder('utf-8').decode(bytes));
            return {
                id: payload.nameid || payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
                username: payload.unique_name || payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
                role: payload.role || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
                fullName: payload.fullName,
                storeId: payload.storeId,
                storeName: payload.storeName
            };
        } catch (e) {
            console.error("Token decode error:", e);
            return null;
        }
    },
    
    isLoggedIn: () => !!localStorage.getItem('token'),
    
    logout: () => {
        localStorage.removeItem('token');
        window.location.href = 'login.html';
    },

    setToken: (token) => {
        localStorage.setItem('token', token);
    },

    checkAuth: (requiredRole = null) => {
        if (!Auth.isLoggedIn()) {
            if (!window.location.pathname.includes('login.html')) {
                window.location.href = 'login.html';
            }
            return null;
        }
        
        const user = Auth.getUser();
        if (requiredRole && user && user.role !== requiredRole) {
            // Redirect based on role if unauthorized
            if (user.role === 'Admin') {
                if (!window.location.pathname.includes('admin.html')) window.location.href = 'admin.html';
            } else {
                if (!window.location.pathname.includes('anasayfa.html')) window.location.href = 'anasayfa.html';
            }
            return null;
        }
        return user;
    }
};

async function fetchWithAuth(url, options = {}) {
    const token = Auth.getToken();
    
    const headers = {
        'Content-Type': 'application/json',
        ...options.headers
    };

    if (token) {
        headers['Authorization'] = `Bearer ${token}`;
    }

    // Don't set Content-Type for FormData
    if (options.body instanceof FormData) {
        delete headers['Content-Type'];
    }

    try {
        const response = await fetch(`${API_BASE}${url}`, {
            ...options,
            headers
        });

        if (response.status === 401 || response.status === 403) {
            Auth.logout();
            return null;
        }

        const contentType = response.headers.get("content-type");
        let data;
        if (contentType && contentType.includes("application/json")) {
            data = await response.json();
        } else {
            data = await response.text();
        }

        if (!response.ok) {
            throw new Error((data && data.message) ? data.message : 'API Request Failed');
        }
        
        return data;
    } catch (error) {
        console.error('API request failed:', error);
        throw error;
    }
}
