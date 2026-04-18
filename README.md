\# Mini MES｜智慧產線數據即時監控模擬專案



\## 1. 專案簡介



Mini MES 是一個以 \*\*智慧製造場景\*\* 為核心所設計的模擬專案，目的是展示從 \*\*設備資料產生、資料接收、異常判斷、資料入庫，到前端即時監控\*\* 的完整流程。



本專案模擬產線設備持續回傳生產數據，後端 API 負責接收與分析資料，並將結果寫入 PostgreSQL；前端 Dashboard 則即時顯示設備狀態、最新生產紀錄與異常警報。透過這個專案，可以快速呈現我對 \*\*智慧製造、MES 基本概念、設備資料整合、即時監控系統設計\*\* 的理解與實作能力。



\---



\## 2. 專案目標



本專案的目標如下：



\- 模擬智慧工廠中設備資料上傳流程

\- 建立一套簡化版的 MES 資料接收與監控機制

\- 實作設備狀態更新與異常警報邏輯

\- 建立可視化 Dashboard，呈現即時生產資訊

\- 作為智慧製造／MES／自動化系統相關職務的作品集展示專案



本專案雖為模擬系統，但設計思路貼近實際工廠常見情境，例如：

\- 設備週期性回傳生產資料

\- 系統判斷數值是否異常

\- 異常時建立警報紀錄

\- 更新設備當前狀態

\- 提供管理者即時查看產線概況



\---



\## 3. 使用技術



\### Backend

\- C#

\- ASP.NET Core Web API

\- Entity Framework Core



\### Database

\- PostgreSQL



\### Simulator

\- Python

\- requests



\### Frontend

\- HTML

\- CSS

\- JavaScript

\- Fetch API



\### Development Tools

\- Visual Studio / VS Code

\- pgAdmin

\- Swagger（API 測試）



\---



\## 4. 系統架構說明



本系統由 4 個主要模組組成：



\### 1. Python Simulator

模擬產線設備，固定時間送出一筆生產資料至後端 API。  

資料內容包含：

\- 設備代號

\- 工單號

\- 序號

\- 電壓

\- 溫度

\- 紀錄時間



\### 2. ASP.NET Core Web API

負責接收設備資料並執行商業邏輯，包括：

\- 寫入生產紀錄

\- 判斷資料是否異常

\- 建立異常警報

\- 更新設備最新狀態

\- 提供 Dashboard 查詢 API



\### 3. PostgreSQL Database

用於儲存以下核心資料：

\- 設備主檔

\- 生產紀錄

\- 異常警報紀錄



\### 4. Dashboard

以前端頁面呈現即時資訊，讓使用者快速掌握：

\- 今日總生產筆數

\- 今日正常 / 異常數量

\- 各設備目前狀態

\- 最近生產紀錄

\- 最近異常警報



\---



\## 5. 主要功能



\### 5.1 生產資料接收

設備模擬器會定時呼叫 API，將設備數據送入系統。



\### 5.2 生產紀錄保存

每筆設備上傳資料都會寫入 `production\_record`，作為生產歷程追蹤依據。



\### 5.3 異常判斷

系統會依據電壓與溫度等欄位進行判斷，若超出預設範圍，則視為異常資料。



\### 5.4 異常警報建立

當資料異常時，系統會新增一筆 `alert\_record`，保留異常類型與警報時間。



\### 5.5 設備狀態更新

系統會同步更新 `equipment` 資料，反映設備目前狀態，例如：

\- Running

\- Warning

\- Abnormal



\### 5.6 Dashboard 即時監控

前端頁面可定時刷新，顯示最新的設備狀態與生產資訊，模擬現場管理看板的使用情境。



\---



\## 6. 資料流程說明



本專案資料流程如下：



1\. Python Simulator 定時產生模擬設備資料  

2\. Simulator 呼叫 `POST /api/production-record`  

3\. Web API 接收資料並進行欄位驗證  

4\. 系統寫入 `production\_record`  

5\. 系統依據規則進行異常判斷  

6\. 若異常，新增 `alert\_record`  

7\. 同步更新 `equipment.current\_status` 與 `last\_update\_time`  

8\. Dashboard 呼叫查詢 API 顯示最新資訊  



可簡化理解為：



\*\*設備資料產生 → API 接收 → 資料分析 → 寫入資料庫 → 前端監控顯示\*\*



\---



\## 7. 異常判斷規則



本專案以簡化方式模擬智慧製造中的設備異常判斷，主要依據設備回傳的數值資料進行分析。



\### 判斷邏輯範例

\- 電壓超出安全範圍 → 判定為異常

\- 溫度高於警戒值 → 判定為異常

\- 若任一條件成立，該筆紀錄標記為異常

\- 異常資料會寫入 `alert\_record`

\- 設備狀態同步更新為 `Warning` 或 `Abnormal`



\### 實務意義

這種設計對應到工廠常見需求，例如：

\- 監控設備運作穩定性

\- 提前發現機台異常

\- 作為後續維修、保養與稽核依據

\- 建立簡易版 Andon / Alert 機制



> 註：實務上異常規則通常會由製程工程師、設備工程師或 MES 規格共同定義；本專案使用固定閾值邏輯進行模擬。



\---



\## 8. 專案資料夾結構



```bash

mini-mes/

│

├─ backend/

│  └─ MiniMES/

│     ├─ Controllers/

│     │  ├─ ProductionRecordController.cs

│     │  └─ DashboardController.cs

│     ├─ Models/

│     │  ├─ Equipment.cs

│     │  ├─ ProductionRecord.cs

│     │  └─ AlertRecord.cs

│     ├─ DTOs/

│     │  ├─ ProductionRecordCreateDto.cs

│     │  ├─ DashboardSummaryDto.cs

│     │  ├─ EquipmentStatusDto.cs

│     │  ├─ RecentRecordDto.cs

│     │  └─ RecentAlertDto.cs

│     ├─ Data/

│     │  └─ AppDbContext.cs

│     ├─ Program.cs

│     ├─ appsettings.json

│     └─ MiniMES.csproj

│

├─ simulator/

│  └─ simulator.py

│

├─ frontend/

│  ├─ index.html

│  ├─ app.js

│  └─ style.css

│

└─ README.md

