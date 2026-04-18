// ==============================
// API 基本設定
// 之後如果 API 網址要改，只要改這裡
// ==============================
const API_BASE_URL = "https://localhost:7172";

// API 路徑
const API_PATHS = {
    summary: "/api/dashboard/summary",
    equipmentStatus: "/api/dashboard/equipment-status",
    recentRecords: "/api/dashboard/recent-records",
    recentAlerts: "/api/dashboard/recent-alerts"
};

// DOM 元素
const todayTotalEl = document.getElementById("todayTotal");
const todayNormalEl = document.getElementById("todayNormal");
const todayAbnormalEl = document.getElementById("todayAbnormal");

const equipmentStatusListEl = document.getElementById("equipmentStatusList");
const recentRecordsTableBodyEl = document.getElementById("recentRecordsTableBody");
const recentAlertsTableBodyEl = document.getElementById("recentAlertsTableBody");

// 共用 fetch function
async function fetchData(apiPath) {
    const response = await fetch(`${API_BASE_URL}${apiPath}`);

    if (!response.ok) {
        throw new Error(`API 呼叫失敗: ${response.status}`);
    }

    return await response.json();
}

// 日期格式化
function formatDateTime(dateString) {
    if (!dateString) return "-";

    const date = new Date(dateString);

    if (isNaN(date.getTime())) {
        return dateString;
    }

    return date.toLocaleString("zh-TW", {
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit"
    });
}

// 狀態樣式
function getStatusClass(status) {
    if (!status) return "status-unknown";

    const value = status.toLowerCase();

    if (value.includes("normal") || value.includes("running")) {
        return "status-normal";
    }

    if (value.includes("warning")) {
        return "status-warning";
    }

    if (value.includes("abnormal") || value.includes("error") || value.includes("stop")) {
        return "status-error";
    }

    return "status-unknown";
}

// 載入摘要卡片
async function loadSummary() {
    try {
        const data = await fetchData(API_PATHS.summary);

        todayTotalEl.textContent = data.todayTotal ?? 0;
        todayNormalEl.textContent = data.todayNormal ?? 0;
        todayAbnormalEl.textContent = data.todayAbnormal ?? 0;
    } catch (error) {
        console.error("loadSummary error:", error);
        todayTotalEl.textContent = "錯誤";
        todayNormalEl.textContent = "錯誤";
        todayAbnormalEl.textContent = "錯誤";
    }
}

// 載入設備狀態
async function loadEquipmentStatus() {
    try {
        const data = await fetchData(API_PATHS.equipmentStatus);

        if (!Array.isArray(data) || data.length === 0) {
            equipmentStatusListEl.innerHTML = `<div class="empty-text">目前沒有設備資料</div>`;
            return;
        }

        equipmentStatusListEl.innerHTML = data.map(item => {
            const statusClass = getStatusClass(item.currentStatus);

            return `
                <div class="equipment-card">
                    <h3>${item.equipmentCode ?? "-"}</h3>
                    <p><strong>設備名稱：</strong>${item.equipmentName ?? "-"}</p>
                    <p>
                        <strong>目前狀態：</strong>
                        <span class="status-badge ${statusClass}">
                            ${item.currentStatus ?? "-"}
                        </span>
                    </p>
                    <p><strong>最後更新：</strong>${formatDateTime(item.lastUpdateTime)}</p>
                </div>
            `;
        }).join("");
    } catch (error) {
        console.error("loadEquipmentStatus error:", error);
        equipmentStatusListEl.innerHTML = `<div class="empty-text">設備狀態載入失敗</div>`;
    }
}

// 載入最近生產紀錄
async function loadRecentRecords() {
    try {
        const data = await fetchData(API_PATHS.recentRecords);

        if (!Array.isArray(data) || data.length === 0) {
            recentRecordsTableBodyEl.innerHTML = `
                <tr>
                    <td colspan="7" class="empty-text">目前沒有生產紀錄</td>
                </tr>
            `;
            return;
        }

        recentRecordsTableBodyEl.innerHTML = data.map(item => {
            const resultClass = String(item.resultStatus).toLowerCase() === "normal"
                ? "result-normal"
                : "result-abnormal";

            return `
                <tr>
                    <td>${formatDateTime(item.recordTime)}</td>
                    <td>${item.equipmentCode ?? "-"}</td>
                    <td>${item.workOrderNo ?? "-"}</td>
                    <td>${item.serialNo ?? "-"}</td>
                    <td>${item.voltage ?? "-"}</td>
                    <td>${item.temperature ?? "-"}</td>
                    <td class="${resultClass}">${item.resultStatus ?? "-"}</td>
                </tr>
            `;
        }).join("");
    } catch (error) {
        console.error("loadRecentRecords error:", error);
        recentRecordsTableBodyEl.innerHTML = `
            <tr>
                <td colspan="7" class="empty-text">生產紀錄載入失敗</td>
            </tr>
        `;
    }
}

// 載入最近異常紀錄
async function loadRecentAlerts() {
    try {
        const data = await fetchData(API_PATHS.recentAlerts);

        if (!Array.isArray(data) || data.length === 0) {
            recentAlertsTableBodyEl.innerHTML = `
                <tr>
                    <td colspan="5" class="empty-text">目前沒有異常紀錄</td>
                </tr>
            `;
            return;
        }

        recentAlertsTableBodyEl.innerHTML = data.map(item => `
            <tr>
                <td>${formatDateTime(item.recordTime)}</td>
                <td>${item.equipmentCode ?? "-"}</td>
                <td>${item.workOrderNo ?? "-"}</td>
                <td>${item.serialNo ?? "-"}</td>
                <td>${item.alertMessage ?? "-"}</td>
            </tr>
        `).join("");
    } catch (error) {
        console.error("loadRecentAlerts error:", error);
        recentAlertsTableBodyEl.innerHTML = `
            <tr>
                <td colspan="5" class="empty-text">異常紀錄載入失敗</td>
            </tr>
        `;
    }
}

// 初始載入
async function initDashboard() {
    await Promise.all([
        loadSummary(),
        loadEquipmentStatus(),
        loadRecentRecords(),
        loadRecentAlerts()
    ]);

    updateRefreshTime();
}

function updateRefreshTime() {
    const el = document.getElementById("lastRefreshTime");
    if (el) {
        el.textContent = "最後更新：" + new Date().toLocaleString("zh-TW");
    }
}

initDashboard();
setInterval(initDashboard, 5000);