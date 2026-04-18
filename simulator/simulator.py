import random
import time
import uuid
from datetime import datetime
import requests
import urllib3

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)# =========================================================
# API 設定
# 如果你的 ASP.NET Core API 不是跑在 5000，請改這裡
# 例如：
# API_URL = "http://localhost:5062/api/production-record"
# =========================================================
API_URL = "http://localhost:5078/api/production-record"
    
# 模擬設備清單
EQUIPMENT_CODES = ["EQ-001", "EQ-002", "EQ-003"]

# 模擬工單清單
WORK_ORDER_LIST = ["WO-20260417-001", "WO-20260417-002", "WO-20260417-003"]


def generate_serial_no() -> str:
    """產生產品序號"""
    return f"SN-{datetime.now().strftime('%Y%m%d')}-{uuid.uuid4().hex[:6].upper()}"


def generate_voltage() -> float:
    """
    電壓正常值與異常值
    正常：210 ~ 230
    異常：180 ~ 209 或 231 ~ 250
    異常機率約 20%
    """
    abnormal_chance = 0.2

    if random.random() < abnormal_chance:
        if random.choice([True, False]):
            return round(random.uniform(180, 209), 2)
        return round(random.uniform(231, 250), 2)

    return round(random.uniform(210, 230), 2)


def generate_temperature() -> float:
    """
    溫度正常值與異常值
    正常：20 ~ 80
    異常：5 ~ 19 或 81 ~ 100
    異常機率約 20%
    """
    abnormal_chance = 0.2

    if random.random() < abnormal_chance:
        if random.choice([True, False]):
            return round(random.uniform(5, 19), 2)
        return round(random.uniform(81, 100), 2)

    return round(random.uniform(20, 80), 2)


def build_payload() -> dict:
    """建立要送出的 JSON 資料"""
    payload = {
        "equipmentCode": random.choice(EQUIPMENT_CODES),
        "workOrderNo": random.choice(WORK_ORDER_LIST),
        "serialNo": generate_serial_no(),
        "voltage": generate_voltage(),
        "temperature": generate_temperature(),
        "recordTime": datetime.now().isoformat()
    }
    return payload


def send_data():
    payload = build_payload()

    try:
        response = requests.post(API_URL, json=payload, timeout=10, verify=False)

        print("=" * 60)
        print(f"送出時間: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}")
        print(f"送出資料: {payload}")
        print(f"HTTP 狀態碼: {response.status_code}")

        try:
            print(f"API 回應: {response.json()}")
        except ValueError:
            print(f"API 回應: {response.text}")

    except requests.exceptions.RequestException as e:
        print("=" * 60)
        print(f"送出失敗: {e}")
        print("請確認：")
        print("1. backend API 是否已啟動")
        print("2. API_URL port 是否正確")
        print("3. 路由 /api/production-record 是否正確")
    """送出一筆資料到 API"""
    payload = build_payload()

    try:
        response = requests.post(API_URL, json=payload, timeout=10, verify=False)

        print("=" * 60)
        print(f"送出時間: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}")
        print(f"送出資料: {payload}")
        print(f"HTTP 狀態碼: {response.status_code}")

        try:
            print(f"API 回應: {response.json()}")
        except ValueError:
            print(f"API 回應: {response.text}")

    except requests.exceptions.RequestException as e:
        print("=" * 60)
        print(f"送出失敗: {e}")
        print("請確認：")
        print("1. backend API 是否已啟動")
        print("2. API_URL port 是否正確")
        print("3. 路由 /api/production-record 是否正確")


def main():
    print("設備模擬器啟動中...")
    print(f"目標 API: {API_URL}")
    print("每 3 秒送出 1 筆 production record")
    print("按 Ctrl + C 可停止程式")
    print()

    while True:
        send_data()
        time.sleep(3)


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("\n模擬器已停止")