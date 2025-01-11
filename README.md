# todo

[視窗程式設計期末專題](https://github.com/HellHBBD/todo)

## 目錄

- [todo](#todo)
  - [目錄](##目錄)
  - [登入介面](##登入介面)
    - [編輯使用者](###編輯使用者)
  - [主畫面](##主畫面)
    - [編輯任務](###)
  - [排版](##排版)
    - [依新增順序排序(預設)](###依新增順序排序(預設))
    - [以月曆檢視](###以月曆檢視)
    - [四象限圖](###四象限圖)
    - [依先後順序排序](###依先後順序排序)
    - [依緊急重要程度排序](###依緊急重要程度排序)

## 登入介面

![login](https://hackmd.io/_uploads/HJ73Zt1vyl.png)

- 按鈕有快捷鍵提示
- 使用者名稱重複會提示

### 編輯使用者

![input_add](https://hackmd.io/_uploads/Skz6ZFyP1l.png)
![input_rename](https://hackmd.io/_uploads/ry4pWYkP1x.png)

- Enter確認，Esc取消
  
  ## 主畫面
  
  ![home](https://hackmd.io/_uploads/rkMxGt1Pye.png)

- 在空白處點兩下滑鼠左鍵可以新增任務
- 對checkBox點右鍵可以編輯任務
- 任務名稱重複會提示

### 編輯任務

![edit_add](https://hackmd.io/_uploads/HkJEGt1wyg.png)
![edit_modify](https://hackmd.io/_uploads/Hy_4ft1D1l.png)

可編輯項目：

- 名稱
- 期限
- 重要程度
- 先後順序
- 詳細敘述

也可刪除任務。

#### 先後順序

![image](https://hackmd.io/_uploads/rJPbu0kwJg.png)

- 用按鈕控制任務的前後關係
- 有循環的先後關係會提示

#### 詳述

![image](https://hackmd.io/_uploads/ry_HuAkwJx.png)

- 可以編輯字體和顏色

## 排版

### 依新增順序排序(預設)

### 以月曆檢視

![image](https://hackmd.io/_uploads/r1cafKyDye.png)

- 點擊日期檢視當天是否有任務
- 在任務前方以顏色標示緊急程度
- 任務完成的會以灰色顯示，並顯示刪除線

### 四象限圖

![image](https://hackmd.io/_uploads/HJjZmYyvyg.png)

- X軸由右到左日期越近(越緊急)
- Y軸由下到上重要度越高(越重要)
- 游標移到任務上會顯示詳情

### 依先後順序排序

![image](https://hackmd.io/_uploads/HJpQ7YJPyl.png)

### 依緊急重要程度排序

![image](https://hackmd.io/_uploads/SybImtyPyl.png)

- 依照緊急度和重要度計算出的優先度值進行排序
- 在任務方塊旁呈現紅到綠的標籤（越緊急重要->越不緊急重要）
